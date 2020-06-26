using Sophia.Threading;
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
        private static readonly string SOPHIA_INSTALL_LOCATION = "C:\\DAE\\bin\\" + Application.unityVersion + "\\";

        //--------------------------------------------------------------------------------------
        // Properties
        public bool IsRunning
        {
            get;
            private set;
        }

        private readonly PluginHandler plugin_handler;

        //--------------------------------------------------------------------------------------
        public EditorService()
        {
            IsRunning = false;

            plugin_handler = new PluginHandler();
        }

        //--------------------------------------------------------------------------------------
        public void run()
        {
            //
            // Subscribe to update and quit event
            //
            EditorApplication.update += update;
            EditorApplication.quitting += stop;

            //
            // Flag that we are running the application
            //
            IsRunning = plugin_handler.initialize(SOPHIA_INSTALL_LOCATION);
        }

        //--------------------------------------------------------------------------------------
        private void update()
        {
            if (!IsRunning)
                return;

            plugin_handler.update(Time.deltaTime);
        }
        //--------------------------------------------------------------------------------------
        private void stop()
        {
            if (!IsRunning)
                return;

            plugin_handler.stop();

            EditorApplication.quitting -= stop;
            EditorApplication.update -= update;

            IsRunning = false;
        }
    }
}
