using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utility
{
	/// <summary>
	/// Provide all LayerMask as property
	/// </summary>
	public static class LayerMaskStorage
	{
		public static LayerMask Ground => LayerMask.GetMask("Ground");
		public static LayerMask Water => LayerMask.GetMask("Water");
		public static LayerMask Platform => LayerMask.GetMask("Platform");
		public static LayerMask UI => LayerMask.GetMask("UI");

	}
}
