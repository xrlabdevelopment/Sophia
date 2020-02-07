namespace Sophia.Platform
{
    using System.Collections.Generic;
    using Sophia.Core;
    using UnityEngine;

    public class SaveLoadSceneObjects : Singleton<SaveLoadSceneObjects>
    {
        //--------------------------------------------------------------------------------------
        // Fields
        private List<SceneObjectBase> objects_to_save = new List<SceneObjectBase>();
        private List<SceneObjectBase> objects_to_load = new List<SceneObjectBase>();

        //--------------------------------------------------------------------------------------
        // Properties
        public string SaveLocation = "";
        public string SaveName = "";
        public List<SceneObjectBase> ObjectsToSave { get { return objects_to_save; } }

        //--------------------------------------------------------------------------------------
        public void registerObjectToSave(SceneObjectBase objToSave)
        {
            objects_to_save.Add(objToSave);
        }

        //--------------------------------------------------------------------------------------
        public void saveScene()
        {
            SaveLoadObjectContainer saveFile = new SaveLoadObjectContainer();
            saveFile.SavedObjects = new SceneContainer[objects_to_save.Count];

            for (int i = 0; i < objects_to_save.Count; i++)
            {
                objects_to_save[i].updateData();
                saveFile.SavedObjects[i] = objects_to_save[i].GetContainer;
            }

            JsonIO.save<SaveLoadObjectContainer>(SaveLocation, SaveName, saveFile);
        }

        //--------------------------------------------------------------------------------------
        public void loadScene()
        {
            SaveLoadObjectContainer saveFile = new SaveLoadObjectContainer();
            saveFile = JsonIO.load<SaveLoadObjectContainer>(SaveLocation, SaveName);

            foreach (var savedObject in saveFile.SavedObjects)
            {
                var to_load_object = new SceneObjectBase();
                to_load_object.PrefabId = savedObject.PrefabId;
                to_load_object.Position = new Vector3(savedObject.PositionX, savedObject.PositionY, savedObject.PositionZ);
                to_load_object.Rotation = new Vector3(savedObject.RotationX, savedObject.RotationY, savedObject.RotationZ);
                to_load_object.Scale = new Vector3(savedObject.ScaleX, savedObject.ScaleY, savedObject.ScaleZ);
                objects_to_load.Add(to_load_object);

            }
        }

        //--------------------------------------------------------------------------------------
        public void spawnScene()
        {
            foreach (var loadedObject in objects_to_load)
            {
                var prefab = ApplicationManager.Instance.PrefabIndexManager.spawnPrefab(loadedObject.PrefabId);
                prefab.transform.position = loadedObject.Position;
                prefab.transform.eulerAngles = loadedObject.Rotation;
                prefab.transform.localScale = loadedObject.Scale;
            }
        }

        //--------------------------------------------------------------------------------------
        protected override void onAwake()
        {
            //Nothing to implement
        }

        //--------------------------------------------------------------------------------------
        protected override void onStart()
        {
            //Nothing to implement
        }

        //--------------------------------------------------------------------------------------
        protected override void onUpdate(float dTime)
        {
            //Nothing to implement
        }

        //--------------------------------------------------------------------------------------
        protected override void onDestroy()
        {
            //Nothing to implement
        }
    }
}
