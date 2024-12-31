using Assets.Scripts.Action.Attack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapon.Melee
{
	public class RustySword : IMelee
	{
		public ICanAttack Owner {  get; set; }	

		public float TimeCombo { get; set; } = 2f;
		public float Damage { get; set; } = 100f;
		public float AttackSpeed { get; set; } = 0.5f;

		public int ComboCount { get; set; } = 2;

		public RustySword(ICanAttack owner)
		{
			Owner = owner;
		}

		public void Update()
		{
			
		}
	}
}
