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

        public static readonly string SOPHIA_CORE = "sophia_core";
        public static readonly string SOPHIA_EDITOR = "sophia_editor";
        public static readonly string SOPHIA_PLATFORM = "sophia_platform";

        public static readonly string DEBUG_POSTFIX = "_d";
        public static readonly string DLL_EXTENTION = ".dll";

        public static readonly string CURRENT_INSTALL_LOCATION = PROJECTSOPHIA_INSTALL_LOCATION;

        public static readonly string SOPHIA_CORE_DLL = SOPHIA_CORE + DLL_EXTENTION;
        public static readonly string SOPHIA_EDITOR_DLL = SOPHIA_EDITOR + DLL_EXTENTION;
        public static readonly string SOPHIA_PLATFORM_DLL = SOPHIA_PLATFORM + DLL_EXTENTION;

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
        private static List<Task> copy_tasks;
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
            copy_tasks = new List<Task>();

            //
            // Build sophia dll paths
            //
            string sophia_core_dll_path = CURRENT_INSTALL_LOCATION + SOPHIA_CORE_DLL;
            string sophia_editor_dll_path = CURRENT_INSTALL_LOCATION + SOPHIA_EDITOR_DLL;
            string sophia_platform_dll_path = CURRENT_INSTALL_LOCATION + SOPHIA_PLATFORM_DLL;

            //
            // Store last write time stamp
            //
            time_stamps = new Dictionary<string, DateTime>();

            if (File.Exists(sophia_core_dll_path))      time_stamps.Add(sophia_core_dll_path, File.GetLastWriteTime(sophia_core_dll_path));
            if (File.Exists(sophia_editor_dll_path))    time_stamps.Add(sophia_editor_dll_path, File.GetLastWriteTime(sophia_editor_dll_path));
            if (File.Exists(sophia_platform_dll_path))  time_stamps.Add(sophia_platform_dll_path, File.GetLastWriteTime(sophia_platform_dll_path));

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

            checkSophiaDLLWriteTime(SOPHIA_CORE_DLL);
            checkSophiaDLLWriteTime(SOPHIA_EDITOR_DLL);
            checkSophiaDLLWriteTime(SOPHIA_PLATFORM_DLL);

            if(copy_tasks.Count > 0)
            {
                foreach(Task task in copy_tasks)
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
        private void checkSophiaDLLWriteTime(string pluginName)
        {
            string plugin_path = CURRENT_INSTALL_LOCATION + pluginName;

            if (File.Exists(plugin_path))
            {
                DateTime time = File.GetLastWriteTime(plugin_path);
                if (time_stamps[plugin_path] < time)
                {
                    scheduleCopy(plugin_path, Application.dataPath + "\\Plugins\\" + pluginName, true);
                    Debug.Log(pluginName + " was scheduled to be copied");

                    time_stamps[plugin_path] = time;
                }
            }
        }

        //--------------------------------------------------------------------------------------
        private static void onStartedCopy(Task task)
        {
            // Nothing to implement
        }
        //--------------------------------------------------------------------------------------
        private static void onFinishedCopy(Task task)
        {
            if(copy_tasks.IndexOf(task) != -1)
                copy_tasks.Remove(task);

            is_ready_to_refresh = copy_tasks.Count == 0;
        }
    }
}
