using System;
using System.IO;

using UnityEditor;
using UnityEngine;

namespace Sophia.Editor
{
    [InitializeOnLoad]
    public class EditorStartup
    {
        //---------------------------------------------------------------------------------------
        // Fields
        private static EditorService service;

        //---------------------------------------------------------------------------------------
        static EditorStartup()
        {
            initialize();

            Debug.Log("Engine up and running!");
        }

        //---------------------------------------------------------------------------------------
        private static void initialize()
        {
            if (service == null)
            {
                service = new EditorService();
                service.run();
            }
        }
    }
}