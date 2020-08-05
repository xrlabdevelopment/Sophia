using System;
using System.Collections;
using System.Collections.Generic;
using Sophia.Platform.Attributes;
using UnityEngine;

namespace Sophia.Platform.Utilities
{
	/// <summary>
	/// A component that makes it easy to take screenshots, usually for development purposes.
	/// </summary>
	[AddComponentMenu("Sophia/ScreenshotTaker")]
	[ExecuteInEditMode]
	public sealed class ScreenshotTaker : MonoBehaviourSingleton<ScreenshotTaker>
	{
        //-------------------------------------------------------------------------------------
        // Inspector
        [Tooltip("The key to use for taking a screenshot.")]
		[SerializeField]
		private UnityEngine.KeyCode ScreenshotKey = UnityEngine.KeyCode.Q;

		[Tooltip("The scale at which to take the screen shot.")]
		[NonNegative]
		[SerializeField]
		private int Scale = 1;

		[Tooltip("The fist part of the file name")]
		[SerializeField]
		private string FileNamePrefix = "screen_";

		//[Tooltip("set this to true to have screenshots taken periodically and specify the interval in seconds.")]
		[SerializeField]
		private OptionalFloat AutomaticScreenshotInterval = new OptionalFloat { UseValue = false, Value = 60f};
		
		[Tooltip("Objects to disable when taking a screenshot.")]
		[SerializeField]
		private GameObject[] DirtyObjects = new GameObject[0];

        //-------------------------------------------------------------------------------------
        // Properties
        protected override bool ShouldDestroyOnLoad
        {
            get
            {
                return true;
            }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private Dictionary<GameObject, bool> state_of_dirty_objects;

        //-------------------------------------------------------------------------------------
        [InspectorButton]
		public static void take()
		{
			Instance.takeImpl();
		}
        //-------------------------------------------------------------------------------------
        [InspectorButton]
		public static void takeClean()
		{
			Instance.takeCleanImpl();
		}

        #region Unity Messages

        //-------------------------------------------------------------------------------------
        protected override void onAwake()
        {
            // Nothing to implement
        }

        //-------------------------------------------------------------------------------------
        protected override void onStart()
        {
            if (Application.isPlaying && AutomaticScreenshotInterval.UseValue)
            {
                if (DirtyObjects.Length > 0)
                {
                    InvokeRepeating(takeCleanImpl, AutomaticScreenshotInterval.Value, AutomaticScreenshotInterval.Value);
                }
                else
                {
                    InvokeRepeating(takeImpl, AutomaticScreenshotInterval.Value, AutomaticScreenshotInterval.Value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        protected override void onUpdate(float dTime)
        {
            if (UnityEngine.Input.GetKeyDown(ScreenshotKey))
            {
                if (DirtyObjects.Length > 0)
                {
                    takeClean();
                }
                else
                {
                    take();
                }
            }
        }

        //-------------------------------------------------------------------------------------
        protected override void onDestroy()
        {
            // Nothing to implement
        }

        #endregion

        //-------------------------------------------------------------------------------------
        private void takeCleanImpl()
		{
			StartCoroutine(takeCleanEnumerator());
		}

        //-------------------------------------------------------------------------------------
        private IEnumerator takeCleanEnumerator()
		{
			state_of_dirty_objects = new Dictionary<GameObject, bool>();

			foreach (GameObject dirtyObject in DirtyObjects)
			{
				state_of_dirty_objects.Add(dirtyObject, dirtyObject.activeSelf);
				dirtyObject.SetActive(false);
			}

			yield return new WaitForEndOfFrame();

			takeImpl();

			yield return new WaitForEndOfFrame();

			foreach (KeyValuePair<GameObject, bool> stateOfDirtyObject in state_of_dirty_objects)
			{
				stateOfDirtyObject.Key.SetActive(stateOfDirtyObject.Value);
			}
		}

        //-------------------------------------------------------------------------------------
        private void takeImpl()
		{
			string path = FileNamePrefix + DateTime.Now.Ticks + ".png";
			ScreenCapture.CaptureScreenshot(path, Scale);
		}
    }
}
