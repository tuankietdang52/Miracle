using Assets.Scripts.Action.Attack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
	public interface IWeapon
	{
		public ICanAttack Owner { get; set; }
		public float TimeCombo { get; set; }
		public float AttackSpeed { get; set; }
		public float Damage { get; set; }

		public void Update();
	}
}
