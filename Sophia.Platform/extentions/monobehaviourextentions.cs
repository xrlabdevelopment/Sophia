using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sophia.Platform.Extension
{
    ///<summary>
    /// Provides useful extension methods for MonoBehaviours.
    /// </summary>
    public static class MonoBehaviourExtensions
	{
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Clones an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T clone<T>(this T obj)
            where T : MonoBehaviour
		{
			return BaseMonoBehaviour.Instantiate<T>(obj);
		}
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Clones an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<T> clone<T>(this T obj, int count)
            where T : MonoBehaviour
		{
            List<T> list = new List<T>();

			for (int i = 0; i < count; i++)
			{
				list.Add(obj.clone<T>());
			}

			return list;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Find a child of this component
        /// </summary>
        /// <param name="component"></param>
        /// <param name="childName">Name of the child we are looking for</param>
        /// <returns>The returned child, null if no child was found</returns>
        public static GameObject findChild(this Component component, string childName)
        {
            return component.transform.Find(childName).gameObject;
        }
        //-------------------------------------------------------------------------------------
        /// <summary>
        /// /// Find a child of this component recursively
        /// </summary>
        /// <param name="component"></param>
        /// <param name="childName">Name of the child we are looking for</param>
        /// <returns>The returned child, null if no child was found</returns>
        public static GameObject findChildRecursively(this Component component, string childName)
        {
            Transform target = component.transform;
            if (target.Find(childName) != null)
                return target.gameObject;

            for (int i = 0; i < target.childCount; ++i)
            {
                GameObject result = findChildRecursively(target.GetChild(i), childName);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
		/// Gets an attached component that implements the interface of the type parameter.
		/// </summary>
		/// <typeparam name="TInterface">The type of the t interface.</typeparam>
		/// <param name="thisComponent">The this component.</param>
		/// <returns>TInterface.</returns>
		public static TInterface getInterfaceComponent<TInterface>(this Component thisComponent)
            where TInterface : class
		{
			return thisComponent.GetComponent(typeof(TInterface)) as TInterface;
		}

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Invokes the given action after the given amount of time.
        /// </summary>
        public static Coroutine invoke(this MonoBehaviour monoBehaviour, Action action, float time)
        {
            return monoBehaviour.StartCoroutine(invokeImpl(action, time));
        }

        //-------------------------------------------------------------------------------------
        private static IEnumerator invokeImpl(Action action, float time)
        {
            yield return new WaitForSeconds(time);

            action();
        }

        //-------------------------------------------------------------------------------------
        /// <summary>
        /// Invokes the given action after the given amount of time, and repeats the 
        /// action after every repeatTime seconds.
        /// </summary>
        public static Coroutine invokeRepeating(this MonoBehaviour monoBehaviour, Action action, float time, float repeatTime)
        {
            return monoBehaviour.StartCoroutine(invokeRepeatingImpl(action, time, repeatTime));
        }

        //-------------------------------------------------------------------------------------
        private static IEnumerator invokeRepeatingImpl(Action action, float time, float repeatTime)
        {
            yield return new WaitForSeconds(time);

            while (true)
            {
                action();
                yield return new WaitForSeconds(repeatTime);
            }
        }
    }
}
