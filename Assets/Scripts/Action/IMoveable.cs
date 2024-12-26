using Assets.Scripts.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Action
{
	public interface IMoveable
	{
		/// <summary>
		/// Move the object by velocity
		/// </summary>
		/// <param name="velocity"></param>
		public void Move(Vector3 velocity);
	}
}
