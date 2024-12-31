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

using ELayerMask = Assets.Scripts.Utility.LayerMaskStorage.ELayerMask;

namespace Assets.Scripts.Action.Attack
{
	public class MeleeAttackHandler : AttackHandler
	{
		public override void Attacking()
		{
			if (InAttackCooldown) return;

			List<ELayerMask> layers = new() { ELayerMask.Entity };

			var masks = LayerMaskStorage.GetMultipleMasks(layers);

			var pos = (Vector2)transform.position;
			pos += Owner.IsFacingRight() ? 
				attackStats.AttackHolderPositionModify[ComboIndex] : -attackStats.AttackHolderPositionModify[ComboIndex];

			var collider = 
				Physics2D.OverlapBoxAll(pos, attackStats.AttackRange[ComboIndex], 0, masks);
			DecreaseOnHitEnemyHealth(collider);

			SetAttackTimeOut();
			SetComboTimeOut();
		}

		private void DecreaseOnHitEnemyHealth(Collider2D[] collider)
		{
			var weapon = ((ICanAttack) Owner).Weapon;

			float damage = attackStats.BaseDamage[ComboIndex];
			damage += weapon != null ? weapon.Damage : 0;

			foreach (var obj in collider)
			{
				var enemy = obj.gameObject;
				if (enemy == Owner.gameObject) continue;

				logger.Log(damage);
			}
		}
	}
}
