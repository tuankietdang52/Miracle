using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using ELayerMask = Assets.Scripts.Utility.LayerMaskStorage.ELayerMask;

namespace Assets.Scripts.Utility
{
	public static class PrefabManager
	{
		public static T GetPrefab<T>(string path) where T : MonoBehaviour
		{
			return Resources.Load<T>(path);
		}

		public static T ClonePrefab<T>(string path, MonoBehaviour parent) where T : MonoBehaviour
		{
			var prefab = GetPrefab<T>(path);
			return UnityEngine.Object.Instantiate(prefab, parent.transform);
		}

		public static T ClonePrefab<T>(string path, MonoBehaviour parent, ELayerMask mask) where T : MonoBehaviour
		{
			var prefab = GetPrefab<T>(path);
			T obj = UnityEngine.Object.Instantiate(prefab, parent.transform);
			obj.gameObject.layer = LayerMaskStorage.GetLayer(mask);

			return obj;
		}

		public static T ClonePrefab<T>(T prefab, MonoBehaviour parent) where T : MonoBehaviour
		{
			return UnityEngine.Object.Instantiate(prefab, parent.transform);
		}

		public static T ClonePrefab<T>(T prefab, MonoBehaviour parent, ELayerMask mask) where T : MonoBehaviour
		{
			T obj = UnityEngine.Object.Instantiate(prefab, parent.transform);
			obj.gameObject.layer = LayerMaskStorage.GetLayer(mask);

			return obj;
		}
	}
}