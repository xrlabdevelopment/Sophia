using UnityEngine;
using UnityEditor;

namespace Sophia.Editor
{
    public class ReloadPlugins : MonoBehaviour
    {
        [MenuItem("Tools/Reload Plugins")]
        public static void reload()
        {
            string sophia_core_dll_path = EditorService.CURRENT_INSTALL_LOCATION + EditorService.SOPHIA_CORE_DLL;
            string sophia_editor_dll_path = EditorService.CURRENT_INSTALL_LOCATION + EditorService.SOPHIA_EDITOR_DLL;
            string sophia_platform_dll_path = EditorService.CURRENT_INSTALL_LOCATION + EditorService.SOPHIA_PLATFORM_DLL;

            bool copied = true;

            copied &= EditorService.forceCopy(sophia_core_dll_path, Application.dataPath + "\\Plugins\\" + EditorService.SOPHIA_CORE_DLL, true);
            copied &= EditorService.forceCopy(sophia_editor_dll_path, Application.dataPath + "\\Plugins\\" + EditorService.SOPHIA_EDITOR_DLL, true);
            copied &= EditorService.forceCopy(sophia_platform_dll_path, Application.dataPath + "\\Plugins\\" + EditorService.SOPHIA_PLATFORM_DLL, true);

            if (copied)
                AssetDatabase.Refresh();
        }
    }

    public class FetchPlugins : MonoBehaviour
    {
        [MenuItem("Tools/Fetch Plugins")]
        public static void fetch()
        {
            Debug.LogWarning("Fetching plugins from the server is not implemented yet.");
        }
    }
}
