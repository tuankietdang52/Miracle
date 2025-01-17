using Assets.Scripts.Components;
using Assets.Scripts.Entity;
using Assets.Scripts.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.EventSystems.EventTrigger;

namespace Assets.Scripts.Action.Attack
{
	public class DamageDealer
	{
		private readonly AttackComponent attackStats;

		public DamageDealer(AttackComponent AttackStats)
		{
			attackStats = AttackStats;
		}

		private bool IsCritical(float chance)
		{
			var num = UnityEngine.Random.Range(0f, 1f);

			if (num > chance) return false;
			return true;
		}

		/// <summary>
		/// Return damage when valid, -1 if not
		/// </summary>
		/// <param name="target"></param>
		/// <param name="comboIndex"></param>
		/// <param name="weapon"></param>
		/// <returns></returns>
		public float GetDamageDeal(IAttackable target, int comboIndex, BaseWeapon weapon = null)
		{
			try
			{
				var temp = target as BaseEntity;
				if ((temp.IsDead())) return -1;
			}
			catch { return -1; }

			float damage = weapon != null ? weapon.Damage : 0;
			damage += attackStats.BaseDamage[comboIndex];

			float criticalChance = weapon != null ? weapon.CriticalChance : 0;
			if (IsCritical(criticalChance)) damage *= 2;

			return damage;
		}
	}
}
