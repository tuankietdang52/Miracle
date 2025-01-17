using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static Assets.Scripts.Utility.LayerMaskStorage;

namespace Assets.Scripts.Action.Attack
{
	public class OnAttackEventArgs
	{
		public BaseEntity Target;
		public int ComboIndex;

		public OnAttackEventArgs(BaseEntity target, int comboIndex)
		{
			Target = target;
			ComboIndex = comboIndex;
		}
	}

	public delegate void OnAttackEventHandler(object sender, OnAttackEventArgs args);

	public class AttackHolder : MonoBehaviour
	{
		private readonly Logger logger = LoggerExtension.CreateLogger();
		public BaseEntity Owner;
		public AttackHandler AttackHandlerPrefab;

		public event OnAttackEventHandler OnAttackSuccess;

		[HideInInspector]
		public AttackHandler AttackHandler;

		public Vector2 SampleAttackRange;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, SampleAttackRange);
		}

		private void Awake()
		{
			if (!Owner.TryConvertTo<ICanAttack>(out var _))
			{
				logger.LogError("Owner can't attack");
			}
		}

		private void Start()
		{
			AttackHandler =
				PrefabManager.ClonePrefab(AttackHandlerPrefab, this, LayerMaskStorage.ELayerMask.Utility);
			AttackHandler.SetupHandler(Owner.GetConvertTo<ICanAttack>(), this);
		}

		public bool IsEnemiesOnAttackRange()
		{
			int comboIndex = AttackHandler.ComboIndex;
			var colliders = GetAttackRangeColliders(comboIndex);
			return colliders.Count != 0;
		}

		public List<BaseEntity> GetAttackRangeColliders(int comboIndex)
		{
			List<ELayerMask> layers = new() { ELayerMask.Entity };
			var masks = LayerMaskStorage.GetMultipleMasks(layers);

			var attackStats = Owner.GetConvertTo<ICanAttack>().AttackComponent;

			var pos = (Vector2)AttackHandler.transform.position;
			pos += Owner.IsFacingRight() ?
				attackStats.AttackHolderPositionModify[comboIndex] : -attackStats.AttackHolderPositionModify[comboIndex];

			var colliders =
				Physics2D.OverlapBoxAll(pos, attackStats.AttackRange[comboIndex], 0, masks);

			var enemyTag = Owner.EnemyTags;

			List<BaseEntity> entities = new();
			foreach (var collider in colliders)
			{
				if (!enemyTag.Contains(collider.tag) || collider.gameObject == Owner.gameObject)
					continue;

				if (!collider.gameObject.TryGetComponent<BaseEntity>(out var entity) || entity.IsDead())
					continue;

				if (!entity.IsAttackable()) continue;

				entities.Add(entity);
			}

			return entities;
		}

		public void InvokeAttackSuccessEvent(BaseEntity enemy)
		{
			OnAttackSuccess?.Invoke(Owner, new OnAttackEventArgs(enemy, GetComboIndex()));
		}

		public void DoAttack()
		{
			AttackHandler.Attacking();
		}

		public bool InAttackCooldown() => AttackHandler.InAttackCooldown;

		public int GetComboIndex() => AttackHandler.ComboIndex;
	}
}