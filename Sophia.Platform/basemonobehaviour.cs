using System;
using System.Collections.Generic;
using System.Linq;
using Sophia.Platform.Extension;
using UnityEngine;

namespace Sophia.Platform
{
    /// <summary>
    /// Provides some additional functions for MonoBehaviour.
    /// </summary>
    [AddComponentMenu("Sophia/BaseMonobehaviour")]
    public class BaseMonoBehaviour : MonoBehaviour
    {
        //-------------------------------------------------------------------------------------
        public new Transform transform
        {
            get
            {
                return cached_transform != null
                    ? cached_transform
                    : (cached_transform = base.transform);
            }
        }
        //-------------------------------------------------------------------------------------
        public new GameObject gameObject
        {
            get
            {
                return cached_gameObject != null
                    ? cached_gameObject
                    : (cached_gameObject = base.gameObject);
            }
        }

        //-------------------------------------------------------------------------------------
        private Transform cached_transform;
        private GameObject cached_gameObject;

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Change the layer of an object and all it's childeren.
        /// </summary>
        /// <param name="trans">The object that needs to change layer.</param>
        /// <param name="name">Name of the layer we want to change too.</param>
        public static void changeLayersRecursively(Transform trans, string name)
        {
            int layer = LayerMask.NameToLayer(name);
            changeLayersRecursively(trans, layer);
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Change the layer of an object and all it's childeren.
        /// </summary>
        /// <param name="trans">The object that needs to change layer.</param>
        /// <param name="layer">ID of the layer we want to change too.</param>
        public static void changeLayersRecursively(Transform trans, int layer)
        {
            trans.gameObject.layer = layer;
            foreach (Transform child in trans)
                changeLayersRecursively(child, layer);
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Instantiates a prefab and attaches it to the given root. 
        /// </summary>
        public static T instantiate<T>(T prefab, GameObject root)
            where T : Component
        {
            T new_object = Instantiate(prefab);

            new_object.transform.SetParent(root.transform, false);
            new_object.transform.resetLocal();

            return new_object;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Instantiates a prefab, attaches it to the given root, and
        /// sets the local position and rotation.
        /// </summary>
        public static T instantiate<T>(T prefab, GameObject root, Vector3 localPosition, Quaternion localRotation)
            where T : Component
        {
            T new_object = Instantiate<T>(prefab);

            new_object.transform.parent = root.transform;

            new_object.transform.localPosition = localPosition;
            new_object.transform.localRotation = localRotation;
            new_object.transform.resetScale();

            return new_object;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Instantiates a prefab.
        /// </summary>
        /// <param name="prefab">The object.</param>
        /// <returns>GameObject.</returns>
        public static GameObject instantiate(GameObject prefab)
        {
            return UnityEngine.Object.Instantiate(prefab);
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Instantiates the specified prefab.
        /// </summary>
        public static GameObject instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject new_object = UnityEngine.Object.Instantiate(prefab, position, rotation);

            return new_object;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Instantiates a prefab and parents it to the root.
        /// </summary>
        /// <param name="prefab">The prefab.</param>
        /// <param name="root">The root.</param>
        /// <param name="isCanvas">To know if it is instantiate in a canvas or not</param>
        /// <returns>GameObject.</returns>
        public static GameObject instantiate(GameObject prefab, GameObject root)
        {
            GameObject new_object = UnityEngine.Object.Instantiate(prefab);

            new_object.transform.parent = root.transform;

            new_object.transform.resetLocal();

            return new_object;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Similar to FindObjectsOfType, except that it looks for components
        /// that implement a specific interface.
        /// </summary>
        public static List<I> findObjectsOfInterface<I>()
            where I : class
        {
            MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();

            return monoBehaviours.Select(behaviour => behaviour.GetComponent(typeof(I))).OfType<I>().ToList();
        }

        //-------------------------------------------------------------------------------------
        public Coroutine Invoke(Action action, float time)
        {
            return MonoBehaviourExtensions.invoke(this, action, time);
        }

        //-------------------------------------------------------------------------------------
        public Coroutine InvokeRepeating(Action action, float time, float repeatTime)
        {
            return MonoBehaviourExtensions.invokeRepeating(this, action, time, repeatTime);
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Destroys given object using either Object.Destroy, or Object.DestroyImmediate,
        /// depending on whether Application.isPlaying is true or not. This is useful when 
        /// writing methods that is used by both editor tools and the game itself.
        /// </summary>
        /// <param name="obj">The object to destroy.</param>
        public static void DestroyUniversal(UnityEngine.Object obj)
        {
            if (Application.isPlaying)
            {
                Destroy(obj);
            }
            else
            {
                DestroyImmediate(obj);
            }
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve a component attached to this object.
        /// If the component is not present an assert is triggered.
        /// </summary>
        /// <typeparam name="T">Type of the component</typeparam>
        /// <returns>The requested component</returns>
        protected T getRequiredComponent<T>()
            where T : Component
        {
            T component = GetComponent<T>();

            if (component == null)
            {
                UnityEngine.Debug.LogWarning("Could not retrieve component of type: " + typeof(T).ToString() + ". Creating default ...", this);
#if UNITY_EDITOR
                component = gameObject.AddComponent<T>();
#endif
            }

            return component;
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve a component attached to a child of this object.
        /// If the component is not present an assert is triggered.
        /// </summary>
        /// <typeparam name="T">Type of the component</typeparam>
        /// <returns>The requested component</returns>
        protected T getRequiredComponentInChilderen<T>()
            where T : Component
        {
            UnityEngine.Debug.Assert(transform.childCount > 0, "We cannot retrieve any components from our children if there are no children.");

            T component = GetComponentInChildren<T>();

            if (component == null)
                UnityEngine.Debug.LogWarning("Could not retrieve component of type: " + typeof(T).ToString() + ".");

            return component;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve a components attached to a child of this object.
        /// If the components are not present an assert is triggered.
        /// </summary>
        /// <typeparam name="T">Type of the components</typeparam>
        /// <returns>The requested components</returns>
        protected T[] getRequiredComponentsInChilderen<T>()
            where T : Component
        {
            UnityEngine.Debug.Assert(transform.childCount > 0, "We cannot retrieve any components from our children if there are no children.");

            T[] components = GetComponentsInChildren<T>();

            if (components.Length == 0)
                UnityEngine.Debug.LogWarning("Could not retrieve components of type: " + typeof(T).ToString());

            return components;
        }


        /// <summary>
        /// Retrieve a component attached to the parent of this object
        /// If the component is not present an assert is triggered
        /// </summary>
        /// <typeparam name="T">Type of the component</typeparam>
        /// <returns>The requested component</returns>
        protected T getRequiredComponentInParent<T>()
            where T : Component
        {
            UnityEngine.Debug.Assert(transform.parent != null, "We cannot retrieve any components from our parent if we don't have a parent.");

            T component = GetComponentInParent<T>();

            if (component == null)
            {
                UnityEngine.Debug.LogWarning("Could not retrieve component of type: " + typeof(T).ToString() + ". Creating default ...");
#if UNITY_EDITOR
                component = transform.parent.gameObject.AddComponent<T>();
#endif
            }

            return component;
        }
    }
}
