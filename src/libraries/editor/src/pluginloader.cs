using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Sophia.Threading;
using UnityEngine;

namespace Sophia.Editor
{
    public enum PluginType
    {
        DEBUG,
        RELEASE
    }

    public class PluginLoader
    {
        //--------------------------------------------------------------------------------------
        // Delegates
        public delegate void PluginLoaderEventHandler();

        public PluginLoaderEventHandler onFinishedLoading;

        public PluginLoaderEventHandler onFileChanged;
        public PluginLoaderEventHandler onFileCreated;
        public PluginLoaderEventHandler onFileRenamed;
        public PluginLoaderEventHandler onFileDeleted;

        //--------------------------------------------------------------------------------------
        // Properties
        public bool IsDirty
        {
            get { return copy_tasks.Count > 0; }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private string                  source_directory;
        private string                  destination_directory;
        private PluginType              plugin_type;
        private IO.FileSystemWatcher    file_system_watcher;
        private List<Task>              copy_tasks;
        private PluginAssociations      plugin_associations;

        //--------------------------------------------------------------------------------------
        public PluginLoader(string source, string dest, PluginType type)
        {
            source_directory        = source;
            destination_directory   = dest;
            plugin_type             = type;
            copy_tasks              = new List<Task>();
            plugin_associations     = new PluginAssociations();

            try
            {
                file_system_watcher = new IO.FileSystemWatcher();
                file_system_watcher.subscribe(source_directory, IO.FileSystemWatcher.DEFAULT_CREATION_INFO);
                file_system_watcher.onFileChanged += fileChanged;
                file_system_watcher.onFileCreated += fileCreated;
                file_system_watcher.onFileRenamed += fileRenamed;
                file_system_watcher.onFileDeleted += fileDeleted;

                foreach(string file_path in Directory.GetFiles(source))
                {
                    if (plugin_type == PluginType.DEBUG)
                    {
                        if (!file_path.Contains(IO.PostFix.DEBUG_POSTFIX))
                            continue;
                    }

                    scheduleCopy(file_path, Path.GetFileName(file_path));
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        //--------------------------------------------------------------------------------------
        public void flush()
        {
            foreach (Task task in copy_tasks)
            {
                Thread thread = new Thread(new ThreadStart(task.execute));
                thread.Start();
            }
        }

        //-------------------------------------------------------------------------------------
        private void fileChanged(FileSystemWatcher source, FileSystemEventArgs args)
        {
            //
            // schedule a new task to copy.
            //
            if (scheduleCopy(args.FullPath, args.Name))
            {
                if (onFileChanged != null)
                    onFileChanged();
            }
            else
            {
                Debug.Log("Failed to schedule plugin copy task when file was CHANGED.");
            }
        }
        //-------------------------------------------------------------------------------------
        private void fileCreated(FileSystemWatcher source, FileSystemEventArgs args)
        {
            //
            // schedule a new task to copy.
            //
            if (scheduleCopy(args.FullPath, args.Name))
            {
                if (onFileCreated != null)
                    onFileCreated();
            }
            else
            {
                Debug.Log("Failed to schedule plugin copy task when file was CREATED.");
            }
        }
        //-------------------------------------------------------------------------------------
        private void fileRenamed(FileSystemWatcher source, RenamedEventArgs args)
        {
            //
            // Delete the old file before creating a new one.
            //
            if(File.Exists(destination_directory + Path.GetFileName(args.OldFullPath)))
            {
                File.Delete(destination_directory + Path.GetFileName(args.OldFullPath));
            }

            //
            // schedule a new task to copy.
            //
            if (scheduleCopy(args.FullPath, args.Name))
            {
                if (onFileRenamed != null)
                    onFileRenamed();
            }
            else
            {
                Debug.Log("Failed to schedule plugin copy task when file was RENAMED.");
            }
        }
        //-------------------------------------------------------------------------------------
        private void fileDeleted(FileSystemWatcher source, FileSystemEventArgs args)
        {
            //
            // Delete the old file.
            //
            if (File.Exists(destination_directory + Path.GetFileName(args.FullPath)))
            {
                File.Delete(destination_directory + Path.GetFileName(args.FullPath));
            }

            if (onFileDeleted != null)
                onFileDeleted();

            Debug.Log("File Deleted");
        }

        //--------------------------------------------------------------------------------------
        private bool scheduleCopy(string fullPath, string name)
        {
            //
            // We only want to work with supported files.
            //
            IO.Extention extention = plugin_associations.Extentions[plugin_type].Find(ex => fullPath.Contains(IO.Extentions.toString(ex)));
            if (extention == null || extention == IO.Extention.NONE)
                return false;

            //
            // If we specified to copy debug library files
            // We should check if they have the required postfix.
            //
            if (plugin_type == PluginType.DEBUG)
            {
                if (!name.Contains(Sophia.IO.PostFix.DEBUG_POSTFIX))
                    return false;
            }

            return prepareCopy(source_directory + Path.GetFileName(fullPath), destination_directory + Path.GetFileName(fullPath), true);
        }
        //--------------------------------------------------------------------------------------
        private bool prepareCopy(string from, string to, bool overwrite)
        {
            if (File.Exists(from))
            {
                Task task = new CopyTask(from, to, overwrite);

                task.onStarted  += onStartedCopy;
                task.onFinished += onFinishedCopy;

                copy_tasks.Add(task);

                return true;
            }

            return false;
        }

        //--------------------------------------------------------------------------------------
        private void onStartedCopy(Task task)
        {
            // Nothing to implement
        }
        //--------------------------------------------------------------------------------------
        private void onFinishedCopy(Task task)
        {
            if (copy_tasks.IndexOf(task) != -1)
            {
                copy_tasks.Remove(task);

                if (copy_tasks.Count == 0 && onFinishedLoading != null)
                    onFinishedLoading();
            }
        }
    }
}
