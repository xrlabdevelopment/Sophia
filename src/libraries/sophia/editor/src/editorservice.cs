using Sophia.Core;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using UnityEditor;
using UnityEngine;

namespace Sophia.Editor
{
    internal class EditorService
    {
        //--------------------------------------------------------------------------------------
        // Constants
        public static readonly string PROJECTSOPHIA_INSTALL_LOCATION = "C:\\DAE\\bin\\" + Application.unityVersion + "\\";
        public static readonly string DEBUG_POSTFIX = "_d";
        public static readonly string DLL_EXTENTION = ".dll";
        public static readonly string CURRENT_INSTALL_LOCATION = PROJECTSOPHIA_INSTALL_LOCATION;

        //--------------------------------------------------------------------------------------
        // Properties
        public bool IsRunning
        {
            get { return is_running; }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private bool is_running = false;
        
        private static Dictionary<string, DateTime> time_stamps;
        private static List<ITask> copy_tasks;
        private static bool is_ready_to_refresh = false;

        //--------------------------------------------------------------------------------------
        public void run()
        {
            if (!Directory.Exists(PROJECTSOPHIA_INSTALL_LOCATION))
            {
                Debug.LogWarning("Install location not found: " + PROJECTSOPHIA_INSTALL_LOCATION);
                return;
            }

            //
            // List that will store the threads who will copy the files
            //
            copy_tasks = new List<ITask>();

            //
            // Store last write time stamp
            //
            time_stamps = new Dictionary<string, DateTime>();
            foreach (string file_path in Directory.GetFiles(CURRENT_INSTALL_LOCATION))
            {
                if (file_path.Contains(DLL_EXTENTION))
                {
#if !_DEBUG
                    if(file_path.Contains(DEBUG_POSTFIX + DLL_EXTENTION))
                        continue;
#else
                    if(!file_path.Contains(DEBUG_POSTFIX + DLL_EXTENTION))
                        continue;
#endif
                    Debug.Log("tracking: " + file_path);
                    time_stamps.Add(file_path, File.GetLastWriteTime(file_path));
                }
                else
                {
                    Debug.Log("file path is no dynamic lib: " + file_path);
                }
            }

            //
            // Subscribe to update and quit event
            //
            EditorApplication.update += update;
            EditorApplication.quitting += stop;

            //
            // Flag that we are running the application
            //
            is_running = true;
        }

        //--------------------------------------------------------------------------------------
        public static bool forceCopy(string from, string to, bool overwrite)
        {
            if (File.Exists(from))
            {
                File.Copy(from, to, overwrite);
                Debug.Log("File was copied to: " + to);

                return true;
            }

            return false;
        }
        //--------------------------------------------------------------------------------------
        public static bool scheduleCopy(string from, string to, bool overwrite)
        {
            if (File.Exists(from))
            {
                Task task = new CopyTask(from, to, overwrite);

                task.onStarted += onStartedCopy;
                task.onFinished += onFinishedCopy;

                copy_tasks.Add(task);

                return true;
            }

            return false;
        }

        //--------------------------------------------------------------------------------------
        private void update()
        {
            // Do not check if files need to be copied when we alread have scheduled tasks.
            if (copy_tasks.Count != 0)
                return;

            if(is_ready_to_refresh)
            {
                AssetDatabase.Refresh();
                is_ready_to_refresh = false;
            }

            foreach (string file_path in Directory.GetFiles(CURRENT_INSTALL_LOCATION))
            {
                if (file_path.Contains(DLL_EXTENTION))
                {
#if !_DEBUG
                    if (file_path.Contains(DEBUG_POSTFIX + DLL_EXTENTION))
                        continue;
#else
                    if(!file_path.Contains(DEBUG_POSTFIX + DLL_EXTENTION))
                        continue;
#endif
                    checkSophiaDLLWriteTime(file_path);
                }
            }

            if(copy_tasks.Count > 0)
            {
                foreach(ITask task in copy_tasks)
                {
                    Thread thread = new Thread(new ThreadStart(task.execute));
                    thread.Start();
                }
            }
        }
        //--------------------------------------------------------------------------------------
        private void stop()
        {
            if (!is_running)
                return;

            copy_tasks.Clear();

            EditorApplication.quitting -= stop;
            EditorApplication.update -= update;

            is_running = false;
        }

        //--------------------------------------------------------------------------------------
        private void checkSophiaDLLWriteTime(string path)
        {
            string plugin_path = path;

            if (File.Exists(plugin_path))
            {
                DateTime time = File.GetLastWriteTime(plugin_path);
                if (time_stamps[plugin_path] < time)
                {
                    scheduleCopy(plugin_path, Application.dataPath + "\\Plugins\\" + Path.GetFileName(plugin_path), true);
                    Debug.Log(plugin_path + " was scheduled to be copied");

                    time_stamps[plugin_path] = time;
                }
            }
        }

        //--------------------------------------------------------------------------------------
        private static void onStartedCopy(ITask task)
        {
            // Nothing to implement
        }
        //--------------------------------------------------------------------------------------
        private static void onFinishedCopy(ITask task)
        {
            if(copy_tasks.IndexOf(task) != -1)
                copy_tasks.Remove(task);

            is_ready_to_refresh = copy_tasks.Count == 0;
        }
    }
}
