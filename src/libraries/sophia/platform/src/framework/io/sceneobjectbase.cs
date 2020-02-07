namespace Sophia.Platform
{
    using Newtonsoft.Json;
    using Sophia.Core;
    using UnityEngine;

    public class SceneObjectBase : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Properties
        public int PrefabId = 0;
        public Vector3 Position = Vector3.zero;
        public Vector3 Rotation = Vector3.zero;
        public Vector3 Scale = Vector3.zero;

        private SceneContainer save_load_container = new SceneContainer();

        #region Unity Messages
        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            ApplicationManager.Instance.SaveSceneObjectsManager.registerObjectToSave(this);
        }
        #endregion

        //--------------------------------------------------------------------------------------
        public void updateData()
        {
            Position = this.gameObject.transform.position;
            Rotation = this.gameObject.transform.eulerAngles;
            Scale = this.gameObject.transform.localScale;
        }

        //--------------------------------------------------------------------------------------
        public SceneContainer GetContainer
        {
            get
            {
                save_load_container.PrefabId = PrefabId;

                save_load_container.PositionX = Position.x;
                save_load_container.PositionY = Position.y;
                save_load_container.PositionZ = Position.z;

                save_load_container.RotationX = Rotation.x;
                save_load_container.RotationY = Rotation.y;
                save_load_container.RotationZ = Rotation.z;


                save_load_container.ScaleX = Scale.x;
                save_load_container.ScaleY = Scale.y;
                save_load_container.ScaleZ = Scale.z;

                return save_load_container;
            }
        }
    }
}
