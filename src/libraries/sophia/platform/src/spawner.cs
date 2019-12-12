using Sophia.Core;
using UnityEngine;

namespace Sophia.Platform
{
    /// <summary>
    /// Spawner class
    /// </summary>
    /// <typeparam name="T">Type of the object to spawn</typeparam>
    public class Spawner<T> : BaseMonoBehaviour, ISpawner<T>
    {
        //--------------------------------------------------------------------------------------
        // Inspector
        [SerializeField]
        private GameObject PrefabToSpawn;

        #region Unity Messages

        private void Awake()
        {
            Debug.Assert(PrefabToSpawn.GetComponent<T>() != null, "Prefab does not have a component of type: " + typeof(T).ToString());
        }

        #endregion

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Spawn one specific game object
        /// </summary>
        /// <returns>The spawned game object</returns>
        public T spawn()
        {
            //
            // Instantiate a new prefab
            //
            GameObject game_object = GameObject.Instantiate(PrefabToSpawn);

            //
            // Retrieve the desired component
            // 
            T component = game_object.GetComponent<T>();

            Debug.Assert(component != null, "The spawned gameobject should have a component of type: " + typeof(T).ToString());

            //
            // Return a reference to the component
            //
            return component;
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Spawn a series of game objects
        /// </summary>
        /// <param name="amount">The amount of game objects to spawn</param>
        /// <returns>The spawned game objects</returns>
        public T[] spawn(int amount)
        {
            T[] game_objects = new T[amount];
            
            for(int i = 0; i < amount; ++i)
            {
                T obj = spawn();

                if (obj == null)
                    continue;

                game_objects[i] = obj;
            }

            return game_objects;
        }
    }
}