using Assets.Scripts.Components;
using Assets.Scripts.Entity;
using Assets.Scripts.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Action.Attack
{
	public interface ICanAttack
	{
		public BaseWeapon Weapon { get; set; }
		public AttackHolder AttackHolder { get; set; }

		public AttackComponent AttackComponent { get; set; }

		public void DoAnimationAttack();
		public void Attack();
	}
}
