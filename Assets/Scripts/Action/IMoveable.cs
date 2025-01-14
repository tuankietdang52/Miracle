﻿using Assets.Scripts.Components;
using Assets.Scripts.Entity;
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
		public MovementComponent MovementComponent { get; set; }

		/// <summary>
		/// Move the object by velocity
		/// </summary>
		/// <param name="velocity"></param>
		public void Move(Vector3 velocity);
	}
}
