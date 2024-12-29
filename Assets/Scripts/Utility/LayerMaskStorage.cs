using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
			UI
		}
	}
}
