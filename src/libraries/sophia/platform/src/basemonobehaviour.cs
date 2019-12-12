using UnityEngine;

namespace Sophia.Platform
{
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
                Debug.LogWarning("Could not retrieve component of type: " + typeof(T).ToString() + ". Creating default ...");
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
            Debug.Assert(transform.childCount > 0, "We cannot retrieve any components from our children if there are no children.");

            T component = GetComponentInChildren<T>();

            if (component == null)
                Debug.LogWarning("Could not retrieve component of type: " + typeof(T).ToString() + ".");

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
            Debug.Assert(transform.childCount > 0, "We cannot retrieve any components from our children if there are no children.");

            T[] components = GetComponentsInChildren<T>();

            if (components.Length == 0)
                Debug.LogWarning("Could not retrieve components of type: " + typeof(T).ToString() );

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
            Debug.Assert(transform.parent != null, "We cannot retrieve any components from our parent if we don't have a parent.");

            T component = GetComponentInParent<T>();

            if (component == null)
            {
                Debug.LogWarning("Could not retrieve component of type: " + typeof(T).ToString() + ". Creating default ...");
#if UNITY_EDITOR
                component = transform.parent.gameObject.AddComponent<T>();
#endif
            }

            return component;
        }
    }
}