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
		public override bool IsEnemiesOnAttackRange()
		{
			var colliders = GetAttackRangeColliders();
			return colliders.Length != 0;
		}

		private Collider2D[] GetAttackRangeColliders()
		{
			List<ELayerMask> layers = new() { ELayerMask.Entity };
			var masks = LayerMaskStorage.GetMultipleMasks(layers);

			var attackStats = AttackBehaviour.AttackComponent;

			var pos = (Vector2)transform.position;
			pos += Owner.IsFacingRight() ?
				attackStats.AttackHolderPositionModify[ComboIndex] : -attackStats.AttackHolderPositionModify[ComboIndex];

			var colliders = 
				Physics2D.OverlapBoxAll(pos, attackStats.AttackRange[ComboIndex], 0, masks);

			var enemyTag = Owner.EnemyTag;
			return colliders
				.Where(collider => 
					enemyTag.Contains(collider.tag) && 
					!collider.gameObject.GetComponent<BaseEntity>().IsDead() &&
					collider.gameObject != Owner.gameObject)
				.ToArray();
		}

		public override void Attacking()
		{
			if (InAttackCooldown) {
				if (!Owner.IsDead()) Owner.State = EState.IDLE;
				return;
			}

			var colliders = GetAttackRangeColliders();
			DecreaseOnHitEnemyHealth(colliders);

			SetAttackTimeOut();
			SetComboTimeOut();
		}

		private void DecreaseOnHitEnemyHealth(Collider2D[] collider)
		{
			var weapon = AttackBehaviour.Weapon;

			foreach (var obj in collider)
			{
				var enemyObj = obj.gameObject;

				if (enemyObj == Owner.gameObject) continue;
				if (!enemyObj.TryGetComponent<IAttackable>(out var enemy)) return;

				DealDamage(enemy);
			}
		}
	}
}
