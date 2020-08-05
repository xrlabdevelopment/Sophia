using Sophia.Platform;
using UnityEditor;

namespace Sophia.Editor.Extension
{
    /// <summary>
    /// This universal editor makes it possible to add buttons that will execute static methods
    /// to the inspector by adding the InspectorButton attribute to the method.
    /// </summary>
    /// <remarks>You can also add this behaviour to your own editor by extending from BaseEditor, and 
    /// calling DrawInspectorButtons. </remarks>
    [CustomEditor(typeof(BaseMonoBehaviour), true)]
	[CanEditMultipleObjects]
	public class BaseMonoBehaviourEditor : BaseEditor<BaseMonoBehaviour>
	{
        //--------------------------------------------------------------------------------------
        // Constants
        private const int column_count = 3;

        #region Unity Messages

        //--------------------------------------------------------------------------------------
        public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			drawInspectorButtons(column_count);
		}

        #endregion
    }
}
