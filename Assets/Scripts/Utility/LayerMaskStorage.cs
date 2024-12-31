using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utility
{
	/// <summary>
	/// Provide more property and method about layermask
	/// </summary>
	public static class LayerMaskStorage
	{
		public static LayerMask Ground => LayerMask.GetMask("Ground");
		public static LayerMask Water => LayerMask.GetMask("Water");
		public static LayerMask Platform => LayerMask.GetMask("Platform");
		public static LayerMask Entity => LayerMask.GetMask("Entity");
		public static LayerMask Utility => LayerMask.GetMask("Utility");
		public static LayerMask UI => LayerMask.GetMask("UI");

		public static LayerMask GetMultipleMasks(List<ELayerMask> layers)
		{
			return LayerMask.GetMask(layers.Select(layer => layer.ToString()).ToArray());
		}

		public enum ELayerMask
		{
			Ground,
			Water,
			Platform,
			Entity,
			Utility,
			UI
		}

		public static int GetLayer(ELayerMask mask)
		{
			return LayerMask.NameToLayer(mask.ToString());
		}
	}
}
