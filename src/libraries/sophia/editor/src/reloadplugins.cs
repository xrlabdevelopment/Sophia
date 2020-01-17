using UnityEngine;
using UnityEditor;

namespace Sophia.Editor
{
    public class ReloadPlugins : MonoBehaviour
    {
        private static readonly string SOPHIA_CORE = "sophia_core";
        private static readonly string SOPHIA_EDITOR = "sophia_editor";
        private static readonly string SOPHIA_PLATFORM = "sophia_platform";

        [MenuItem("Tools/Reload Plugins ( release )")]
        public static void reloadRelease()
        {
#if !_DEBUG
            string SOPHIA_CORE_DLL = SOPHIA_CORE + EditorService.DLL_EXTENTION;
            string SOPHIA_EDITOR_DLL = SOPHIA_EDITOR + EditorService.DLL_EXTENTION;
            string SOPHIA_PLATFORM_DLL = SOPHIA_PLATFORM + EditorService.DLL_EXTENTION;

            string sophia_core_dll_path = EditorService.CURRENT_INSTALL_LOCATION + SOPHIA_CORE_DLL;
            string sophia_editor_dll_path = EditorService.CURRENT_INSTALL_LOCATION + SOPHIA_EDITOR_DLL;
            string sophia_platform_dll_path = EditorService.CURRENT_INSTALL_LOCATION + SOPHIA_PLATFORM_DLL;

            bool copied = true;

            copied &= EditorService.forceCopy(sophia_core_dll_path, Application.dataPath + "\\Plugins\\" + SOPHIA_CORE_DLL, true);
            copied &= EditorService.forceCopy(sophia_editor_dll_path, Application.dataPath + "\\Plugins\\" + SOPHIA_EDITOR_DLL, true);
            copied &= EditorService.forceCopy(sophia_platform_dll_path, Application.dataPath + "\\Plugins\\" + SOPHIA_PLATFORM_DLL, true);

            if (copied)
                AssetDatabase.Refresh();
#else
            Debug.LogError("Please remove the \"_DEBUG\" preprocessor condition in the player settings before requesting RELEASE binaries.");
#endif
        }

        [MenuItem("Tools/Reload Plugins ( debug )")]
        public static void reloadDebug()
        {
#if _DEBUG
            string SOPHIA_CORE_DLL = SOPHIA_CORE + EditorService.DEBUG_POSTFIX + EditorService.DLL_EXTENTION;
            string SOPHIA_EDITOR_DLL = SOPHIA_EDITOR + EditorService.DEBUG_POSTFIX + EditorService.DLL_EXTENTION;
            string SOPHIA_PLATFORM_DLL = SOPHIA_PLATFORM + EditorService.DEBUG_POSTFIX + EditorService.DLL_EXTENTION;

            string sophia_core_dll_path = EditorService.CURRENT_INSTALL_LOCATION + SOPHIA_CORE_DLL;
            string sophia_editor_dll_path = EditorService.CURRENT_INSTALL_LOCATION + SOPHIA_EDITOR_DLL;
            string sophia_platform_dll_path = EditorService.CURRENT_INSTALL_LOCATION + SOPHIA_PLATFORM_DLL;

            bool copied = true;

            copied &= EditorService.forceCopy(sophia_core_dll_path, Application.dataPath + "\\Plugins\\" + SOPHIA_CORE_DLL, true);
            copied &= EditorService.forceCopy(sophia_editor_dll_path, Application.dataPath + "\\Plugins\\" + SOPHIA_EDITOR_DLL, true);
            copied &= EditorService.forceCopy(sophia_platform_dll_path, Application.dataPath + "\\Plugins\\" + SOPHIA_PLATFORM_DLL, true);

            if (copied)
                AssetDatabase.Refresh();
#else
            Debug.LogError("Please add the \"_DEBUG\" preprocessor condition in the player settings before requesting DEBUG binaries.");
#endif
        }

    }
}
