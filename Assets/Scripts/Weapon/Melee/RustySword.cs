﻿using Assets.Scripts.Action.Attack;
using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapon.Melee
{
	public class RustySword : BaseWeapon
	{
		public RustySword(ICanAttack owner)
		{
			Owner = owner;
		}

		protected override void SetupStats()
		{
			TimeCombo = 2f;
			Damage = 50f;
			AttackSpeed = 0.3f;
			CriticalChance = 0.05f;
		}
	}
}
