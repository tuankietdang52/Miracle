using Assets.Scripts.Entity;
using Assets.Scripts.Utility;
using Assets.Scripts.Weapon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Log;
using UnityEngine;

namespace Assets.Scripts.Action.Attack
{
	public class MeleeAttackHandler : AttackHandler
	{
		public override void Attacking()
		{
			if (InAttackCooldown) {
				return;
			}

			var enemies = attackHolder.GetAttackRangeColliders(ComboIndex);
			DoAttackToEnemies(enemies);

			SetAttackTimeOut();
			SetComboTimeOut();
		}

		private void DoAttackToEnemies(IEnumerable<BaseEntity> enemies)
		{
			foreach (var enemy in enemies)
			{
				if (!enemy.TryConvertTo<IAttackable>(out var attackableEnemy))
					return;

				DealDamage(attackableEnemy);
				attackHolder.InvokeAttackSuccessEvent(enemy);
			}
		}
	}
}
