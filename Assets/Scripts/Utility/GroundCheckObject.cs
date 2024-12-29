using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using ELayerMask = Assets.Scripts.Utility.LayerMaskStorage.ELayerMask;

namespace Assets.Scripts.Utility
{
	/// <summary>
	/// An object to detect if it overlap with ground
	/// </summary>
	public class GroundCheckObject : MonoBehaviour
	{
		/// <summary>
		/// Size of the collision area
		/// </summary>
		public Vector2 Size;
		public bool IsDropFromPlatform = false;

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.gray;
			Gizmos.DrawCube(transform.position, Size);
		}

		public bool IsOnGround()
		{
			List<ELayerMask> layers = IsDropFromPlatform ?
				new(){ ELayerMask.Ground } : new(){ ELayerMask.Ground, ELayerMask.Platform };

			var masks = LayerMaskStorage.GetMultipleMasks(layers);
			return Physics2D.OverlapBox(transform.position, Size, 0, masks);
		}

		public bool IsOnPlatform()
		{
			if (IsDropFromPlatform) return false;
			return Physics2D.OverlapBox(transform.position, Size, 0, LayerMaskStorage.Platform);
		}
	}
}
