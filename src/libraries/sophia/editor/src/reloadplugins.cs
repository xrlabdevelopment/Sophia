using UnityEngine;
using UnityEditor;

namespace Sophia.Editor
{
    public class ReloadPlugins : MonoBehaviour
    {
        private static readonly string INSTALL_LOCATION = "C:\\DAE\\bin\\";

        private static readonly string SOPHIA_CORE = "sophia_core";
        private static readonly string SOPHIA_EDITOR = "sophia_editor";
        private static readonly string SOPHIA_PLATFORM = "sophia_platform";

        //private static readonly string DEBUG_POSTFIX = "_d";
        private static readonly string DLL_EXTENTION = ".dll";

        [MenuItem("Tools/Reload Plugins")]
        static void reload()
        {
            string current_install_location = INSTALL_LOCATION + Application.unityVersion + "\\";

            string sophia_core_dll = SOPHIA_CORE + DLL_EXTENTION;
            string sophia_editor_dll = SOPHIA_EDITOR + DLL_EXTENTION;
            string sophia_platform_dll = SOPHIA_PLATFORM + DLL_EXTENTION;

            string sophia_core_dll_path = current_install_location + sophia_core_dll;
            string sophia_editor_dll_path = current_install_location + sophia_editor_dll;
            string sophia_platform_dll_path = current_install_location + sophia_platform_dll;

            Debug.Log("Installing from: " + sophia_core_dll_path);
            Debug.Log("Installing from: " + sophia_editor_dll_path);
            Debug.Log("Installing from: " + sophia_platform_dll_path);

            if (System.IO.File.Exists(sophia_core_dll_path))
            {
                System.IO.File.Copy(sophia_core_dll_path, Application.dataPath + "\\Plugins\\" + sophia_core_dll, true);
                Debug.Log(sophia_core_dll + " was copied");
            }
            else Debug.LogWarning("Could not find " + sophia_core_dll);
            if (System.IO.File.Exists(sophia_editor_dll_path))
            {
                System.IO.File.Copy(sophia_editor_dll_path, Application.dataPath + "\\Plugins\\" + sophia_editor_dll, true);
                Debug.Log(sophia_editor_dll + " was copied");
            }
            else Debug.LogWarning("Could not find " + sophia_editor_dll);
            if (System.IO.File.Exists(sophia_platform_dll_path))
            {
                System.IO.File.Copy(sophia_platform_dll_path, Application.dataPath + "\\Plugins\\" + sophia_platform_dll, true);
                Debug.Log(sophia_platform_dll + " was copied");
            }
            else Debug.LogWarning("Could not find " + sophia_platform_dll);
        }
    }

    public class FetchPlugins : MonoBehaviour
    {
        [MenuItem("Tools/Fetch Plugins")]
        static void reload()
        {
            Debug.LogWarning("Fetching plugins from the server is not implemented yet.");
        }
    }
}
