using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.gray;
			Gizmos.DrawCube(transform.position, Size);
		}

		public bool IsOnGround()
		{
			if (Physics2D.OverlapBox(transform.position, Size, 0, LayerMaskStorage.Ground))
			{
				return true;
			}

			return false;
		}
	}
}
