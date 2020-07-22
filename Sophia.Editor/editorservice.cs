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
        // Properties
        public bool IsRunning
        {
            get;
            private set;
        }

        //--------------------------------------------------------------------------------------
        public EditorService()
        {
            IsRunning = false;
        }

        //--------------------------------------------------------------------------------------
        public void run()
        {
            //
            // Subscribe to update and quit event
            //
            EditorApplication.update += update;
            EditorApplication.quitting += stop;
        }

        //--------------------------------------------------------------------------------------
        private void update()
        {
            if (!IsRunning)
                return;
        }
        //--------------------------------------------------------------------------------------
        private void stop()
        {
            if (!IsRunning)
                return;

            EditorApplication.quitting -= stop;
            EditorApplication.update -= update;

            IsRunning = false;
        }
    }
}
