using Assets.Scripts.Action.Attack;
using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Weapon
{
	public abstract class BaseWeapon : MonoBehaviour
	{
		public ICanAttack Owner { get; set; }
		public float TimeCombo { get; set; }
		public float AttackSpeed { get; set; }
		public float Damage { get; set; }

		private float criticalChange;
		public float CriticalChance { 
			get => criticalChange; 
			set {
				if (value > 1) criticalChange = 1;
				else if (value < 0) criticalChange = 0;
				else criticalChange = value;
			} 
		}

		protected abstract void SetupStats();

		protected virtual void Awake()
		{
			SetupStats();
		}
	}
}
