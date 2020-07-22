using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Sophia.Diagnostics;
using Sophia.IO;

namespace Sophia
{
    namespace IO
    {
        public enum EventFilters
        {
            CHANGED = 1 << 0,
            CREATED = 1 << 1,
            RENAMED = 1 << 2,
            DELETED = 1 << 3,
            ERROR = 1 << 4
        }

        public class FileWatcherCreationInfo
        {
            //-------------------------------------------------------------------------------------
            // Constants
            private static readonly EventFilters DEFAULT_EVENT_FILTERS = EventFilters.CHANGED
                                                               | EventFilters.CREATED
                                                               | EventFilters.RENAMED
                                                               | EventFilters.DELETED;

            private static readonly NotifyFilters DEFAULT_NOTIFICATION_FILTERS = NotifyFilters.LastAccess
                                                                      | NotifyFilters.LastWrite
                                                                      | NotifyFilters.FileName
                                                                      | NotifyFilters.DirectoryName;

            //-------------------------------------------------------------------------------------
            public FileWatcherCreationInfo()
            {
                event_filters = DEFAULT_EVENT_FILTERS;
                notification_filters = DEFAULT_NOTIFICATION_FILTERS;

                watch_on_creation = true;
            }

            /// <summary>
            /// Event filter we want to subscribe on
            /// </summary>
            public EventFilters event_filters;
            /// <summary>
            /// Notifications we want to subscribe on
            /// </summary>
            public NotifyFilters notification_filters;

            /// <summary>
            /// Should we start watching on creation.
            /// </summary>
            public bool watch_on_creation;
        }

        public class FileSystemWatcher
        {
            //-------------------------------------------------------------------------------------
            // Delegates
            public delegate void FileSystemEventHandler(System.IO.FileSystemWatcher sender, FileSystemEventArgs e);
            public delegate void RenamedEventHandler(System.IO.FileSystemWatcher sender, RenamedEventArgs e);
            public delegate void ErrorEventHandler(System.IO.FileSystemWatcher sender, ErrorEventArgs e);

            public FileSystemEventHandler onFileCreated;
            public FileSystemEventHandler onFileChanged;
            public FileSystemEventHandler onFileDeleted;
            public RenamedEventHandler onFileRenamed;
            public ErrorEventHandler onErrorOccured;

            //-------------------------------------------------------------------------------------
            // Constants
            public static FileWatcherCreationInfo DEFAULT_CREATION_INFO = new FileWatcherCreationInfo();

            //-------------------------------------------------------------------------------------
            // Fields
            private readonly Dictionary<string, System.IO.FileSystemWatcher> file_system_watchers;
            private readonly Logger logger;

            //-------------------------------------------------------------------------------------
            public FileSystemWatcher(Logger logger = null)
            {
                this.file_system_watchers = new Dictionary<string, System.IO.FileSystemWatcher>();
                this.logger = logger;
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// a new file system change notifier that will raise events when a directory, or file in a directory, changes.
            /// </summary>
            /// <param name="path">path to the directory we would like to observe.</param>
            /// <param name="info">Some additional properties for the filewatcher we would like to create.</param>
            /// <returns>Will return true if we succeeded, false otherwise.</returns>
            public bool subscribe(string path, FileWatcherCreationInfo info)
            {
                if (file_system_watchers.ContainsKey(path))
                {
                    if (logger != null)
                        logger.log((string.Format("FileSystemWatcher for path {0} already present.", path)));

                    return false;
                }

                try
                {
                    System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher(path);

                    watcher.EnableRaisingEvents = info.watch_on_creation;
                    watcher.NotifyFilter = info.notification_filters;

                    watcher = installFileSystemEvents(watcher, info.event_filters);

                    file_system_watchers.Add(path, watcher);

                    return true;
                }
                catch (Exception exception)
                {
                    if (logger != null)
                        logger.error(exception.Message);

                    return false;
                }
            }

            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Enable a specific file watcher
            /// </summary>
            /// <param name="path">Path towards the file watcher we would like to enable</param>
            /// <returns>returns true if enabled, false otherwise.</returns>
            public bool enableWatcher(string path)
            {
                if (!file_system_watchers.ContainsKey(path))
                {
                    if (logger != null)
                        logger.warning(string.Format("FileSystemWatcher with path {0} not present.", path));
                    return false;
                }

                file_system_watchers[path].EnableRaisingEvents = true;
                return true;
            }
            //-------------------------------------------------------------------------------------
            /// <summary>
            /// Disable a specific file watcher
            /// </summary>
            /// <param name="path">Path towards the file watcher we would like to disable</param>
            /// <returns>returns true if disabled, false otherwise.</returns>
            public bool disableWatcher(string path)
            {
                if (!file_system_watchers.ContainsKey(path))
                {
                    if (logger != null)
                        logger.warning(string.Format("FileSystemWatcher with path {0} not present.", path));
                    return false;
                }

                file_system_watchers[path].EnableRaisingEvents = false;
                return true;
            }

            //-------------------------------------------------------------------------------------
            private void fileChanged(object source, FileSystemEventArgs args)
            {
                if (!(source is System.IO.FileSystemWatcher))
                    return;

                if (onFileChanged != null)
                    onFileChanged(source as System.IO.FileSystemWatcher, args);
            }
            //-------------------------------------------------------------------------------------
            private void fileCreated(object source, FileSystemEventArgs args)
            {
                if (!(source is System.IO.FileSystemWatcher))
                    return;

                if (onFileCreated != null)
                    onFileCreated(source as System.IO.FileSystemWatcher, args);
            }
            ///-------------------------------------------------------------------------------------
            private void fileRenamed(object source, RenamedEventArgs args)
            {
                if (!(source is System.IO.FileSystemWatcher))
                    return;

                if (onFileRenamed != null)
                    onFileRenamed(source as System.IO.FileSystemWatcher, args);
            }
            //-------------------------------------------------------------------------------------
            private void fileDeleted(object source, FileSystemEventArgs args)
            {
                if (!(source is System.IO.FileSystemWatcher))
                    return;

                if (onFileDeleted != null)
                    onFileDeleted(source as System.IO.FileSystemWatcher, args);
            }
            //-------------------------------------------------------------------------------------
            private void errorOccured(object source, ErrorEventArgs args)
            {
                if (!(source is System.IO.FileSystemWatcher))
                    return;

                if (onErrorOccured != null)
                    onErrorOccured(source as System.IO.FileSystemWatcher, args);
            }

            //-------------------------------------------------------------------------------------
            private System.IO.FileSystemWatcher installFileSystemEvents(System.IO.FileSystemWatcher watcher, EventFilters eventFilters)
            {
                //
                // Convert the given event filter to an integer.
                // This makes it possible to execute bitwise operations on it.
                // 
                int incoming_event_filter = (int)eventFilters;
                for (int i = 0; i < Enum.GetNames(typeof(EventFilters)).Length; ++i)
                {
                    //
                    // Setup our current event filter that is being processed.
                    //
                    int current_event_filter = 1 << i;

                    //
                    // Check if the event filter was installed.
                    //
                    bool has_event_filter_installed = (incoming_event_filter & current_event_filter) > 0;
                    if (has_event_filter_installed)
                    {
                        switch ((EventFilters)current_event_filter)
                        {
                            case EventFilters.CHANGED:
                                watcher.Changed += fileChanged;
                                break;
                            case EventFilters.CREATED:
                                watcher.Created += fileCreated;
                                break;
                            case EventFilters.DELETED:
                                watcher.Deleted += fileDeleted;
                                break;
                            case EventFilters.RENAMED:
                                watcher.Renamed += fileRenamed;
                                break;
                            case EventFilters.ERROR:
                                watcher.Error += errorOccured;
                                break;
                        }
                    }
                }

                return watcher;
            }
        }
    }
}
