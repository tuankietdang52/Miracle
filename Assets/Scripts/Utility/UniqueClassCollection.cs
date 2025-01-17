using Assets.Scripts.Utility.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Utility
{
	/// <summary>
	/// Collection that contain class only and not allow duplicated, if push a class
	/// that already in collection, the new one will replace the old one
	/// </summary>
	public class UniqueClassCollection<T> : Dictionary<string, T> where T : class
	{
		public UniqueClassCollection()
		{
			
		}

		public new T this[string key]
		{
			get => base[key];
			private set => base[key] = value;
		}

		[Obsolete("This function will do nothing and will throw an exception")]
		public new void Add(string key, T item)
		{
			throw new NotSupportedException("This function is not supported in this collection");
		}

		[Obsolete("This function will do nothing and will throw an exception")]
		public new bool TryAdd(string key, T item)
		{
			throw new NotSupportedException("This function is not supported in this collection");
		}

		[Obsolete("This function will do nothing and will throw an exception")]
		public new bool Remove(string key)
		{
			throw new NotSupportedException("This function is not supported in this collection");
		}

		public void Add(T item)
		{
			string key = item.GetType().Name;

			if (ContainsKey(key))
			{
				this[key] = item;
				return;
			}

			base.Add(key, item);
		}

		public bool Remove(T item)
		{
			return base.Remove(item.GetType().Name);
		}
	}
}
