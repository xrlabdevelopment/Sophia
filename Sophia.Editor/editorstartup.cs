
using UnityEditor;

namespace Sophia.Editor
{
    [InitializeOnLoad]
    public class EditorStartup
    {
        //---------------------------------------------------------------------------------------
        static EditorStartup()
        {
            UnityEngine.Debug.Log("Engine up and running!");
        }
    }
}
