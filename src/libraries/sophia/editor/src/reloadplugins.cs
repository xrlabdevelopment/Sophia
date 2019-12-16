using UnityEngine;
using UnityEditor;

namespace Sophia.Editor
{
    public class ReloadPlugins : MonoBehaviour
    {
        [MenuItem("Tools/Reload Plugins %r")]
        static void reload()
        {
            Debug.Log("Reload Plugins");
        }
    }
}
