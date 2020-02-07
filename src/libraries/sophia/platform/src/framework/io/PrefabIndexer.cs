namespace Sophia.Platform
{
    using System.Collections.Generic;
    using Sophia.Core;
    using UnityEngine;

    public class PrefabIndexer : Singleton<PrefabIndexer>
    {
        //--------------------------------------------------------------------------------------
        // Fields
        [SerializeField] private List<SceneObjectBase> prefab_list;

        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            var objects = this.GetComponentsInChildren<SceneObjectBase>(true);
            prefab_list = new List<SceneObjectBase>(objects);


            for (int i = 0; i < prefab_list.Count; i++)
                prefab_list[i].PrefabId = i;
        }

        //--------------------------------------------------------------------------------------
        public GameObject getPrefab(int index)
        {
            return prefab_list[index].gameObject;
        }

        //--------------------------------------------------------------------------------------
        public GameObject spawnPrefab(int index)
        {
            return spawnPrefab(index, true);
        }

        //--------------------------------------------------------------------------------------
        public GameObject spawnPrefab(int index, bool enable)
        {
            var prefab = getPrefab(index);
            var obj = Instantiate(prefab);
            obj.SetActive(enable);
            obj.name = prefab.name;
            return obj;
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
