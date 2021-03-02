using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sophia.Platform.Extension
{
	/// <summary>
	/// The base class for the generic InspectorList. This class exists so that 
	/// a single property drawer can be use for all sub classes.
	/// </summary>
	[Serializable]
	public class InspectorList
	{}

	/// <summary>
	/// Exactly the same as generic <c>List</c>, but has a custom property drawer 
	/// that draws a re-orderable list in the inspector.
	/// </summary>
	/// <typeparam name="T">The type of the contents of this list.</typeparam>
	/// <remarks>This class should not be used directly (otherwise, it will not appear in the inspector).
	/// Instead, use either one of the provided sub classes, or a define a new custom non-generic subclass
	/// and use that.</remarks>
	[Serializable]
	public class InspectorList<T> : InspectorList, IList<T>
	{
        //-------------------------------------------------------------------------------------
        // Inspector
        [SerializeField]
		private List<T> Values;

        //-------------------------------------------------------------------------------------
        // Properties
        public int Count
        {
            get { return Values.Count; }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }

        public T this[int index]
        {
            get { return Values[index]; }
            set { Values[index] = value; }
        }

        //-------------------------------------------------------------------------------------
        public InspectorList()
		{
            Values = new List<T>();
		}
        //-------------------------------------------------------------------------------------
        public InspectorList(IEnumerable<T> initialValues)
		{
            Values = initialValues.ToList();
		}

        //-------------------------------------------------------------------------------------
        public IEnumerator<T> GetEnumerator()
		{
			return Values.GetEnumerator();
		}
        //-------------------------------------------------------------------------------------
        IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)Values).GetEnumerator();
		}

        //-------------------------------------------------------------------------------------
        public void Add(T item)
		{
			Values.Add(item);
		}
        //-------------------------------------------------------------------------------------
        public void Clear()
		{
			Values.Clear();
		}
        //-------------------------------------------------------------------------------------
        public bool Contains(T item)
		{
			return Values.Contains(item);
		}
        //-------------------------------------------------------------------------------------
        public void CopyTo(T[] array, int arrayIndex)
		{
			Values.CopyTo(array, arrayIndex);
		}
        //-------------------------------------------------------------------------------------
        public bool Remove(T item)
		{
			return Values.Remove(item);
		}

        //-------------------------------------------------------------------------------------
        public int IndexOf(T item)
		{
			return Values.IndexOf(item);
		}
        //-------------------------------------------------------------------------------------
        public void Insert(int index, T item)
		{
			Values.Insert(index, item);
		}
        //-------------------------------------------------------------------------------------
        public void RemoveAt(int index)
		{
			Values.RemoveAt(index);
		}
	}

	/// <summary>
	/// An <c>InspectorList</c> of type <c>int</c>.
	/// </summary>
	[Serializable]
	public class IntList : InspectorList<int> { }

	/// <summary>
	/// An <c>InspectorList</c> of type <c>float</c>.
	/// </summary>
	[Serializable]
	public class FloatList : InspectorList<float> { }

	/// <summary>
	/// An <c>InspectorList</c> of type <c>string</c>.
	/// </summary>
	[Serializable]
	public class StringList : InspectorList<string> { }

	/// <summary>
	/// An <c>InspectorList</c> of type <c>Object</c>.
	/// </summary>
	[Serializable]
	public class ObjectList : InspectorList<UnityEngine.Object> { }

	/// <summary>
	/// An <c>InspectorList</c> of type <c>MonoBehaviour</c>.
	/// </summary>
	[Serializable]
	public class MonoBehaviourList : InspectorList<MonoBehaviour> { }

    /// <summary>
    /// An <c>InspectorList</c> of type <c>Color</c>.
    /// </summary>
    [Serializable]
    public class ColorList : InspectorList<Color> { }

	/// <summary>
	/// An <c>InspectorList</c> of type <c>Vector2</c>.
	/// </summary>
	[Serializable]
	public class Vector2List : InspectorList<Vector2> { }

	/// <summary>
	/// An <c>InspectorList</c> of type <c>Vector3</c>.
	/// </summary>
	[Serializable]
	public class Vector3List : InspectorList<Vector3> { }


	/// <summary>
	/// An <c>InspectorList</c> of type <c>Vector4</c>.
	/// </summary>
	[Serializable]
	public class Vector4List : InspectorList<Vector4> { }

}
