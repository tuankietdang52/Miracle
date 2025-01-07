using Assets.Scripts.Action.Attack;
using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Weapon.Melee
{
	public class HeavySword : BaseWeapon
	{
		public HeavySword(ICanAttack owner)
		{
			Owner = owner;
		}

		protected override void SetupStats()
		{
			TimeCombo = 2f;
			Damage = 110f;
			AttackSpeed = 0.6f;
			CriticalChance = 0.1f;
		}
	}
}
