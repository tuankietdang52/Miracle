using Assets.Scripts.Components;
using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using Assets.Scripts.Utility.Cooldown;
using Assets.Scripts.Weapon;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Action.Attack
{
	public abstract class AttackHandler : MonoBehaviour
	{
		protected readonly Logger logger = LoggerExtension.CreateLogger();

		public BaseEntity Owner;
		public AttackHolder attackHolder;

		public int ComboIndex { get; protected set; }

		protected bool InAttackCooldown = false;
		protected ICanAttack AttackBehaviour;

		private CooldownTimer cooldownTimer;
		private DamageDealer damageDealer;

		public void SetupHandler(BaseEntity owner, AttackHolder attackHolder)
		{
			InAttackCooldown = false;
			Owner = owner;
			this.attackHolder = attackHolder;

			transform.parent = attackHolder.transform;
		}

		public void Start()
		{
			try
			{
				AttackBehaviour = Owner.IsImplement<ICanAttack>();
			}
			catch (Exception e)
			{
				logger.LogError("Owner cannot attack");
				logger.LogException(e);
			}

			damageDealer = new(AttackBehaviour.AttackComponent);
			cooldownTimer = new(this);
		}

		public abstract void Attacking();

		public abstract bool IsEnemiesOnAttackRange();

		private void Update()
		{
			transform.position = transform.parent.position;
		}

		private void OnDrawGizmosSelected()
		{
			var comp = AttackBehaviour.AttackComponent;

			if (AttackBehaviour.Weapon == null) return;

			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(transform.position, comp.AttackRange[ComboIndex]);
		}

		/// <summary>
		/// Decrease health of attacked entity base on attacker damage
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="weapon"></param>
		protected void DealDamage(IAttackable entity)
		{
			damageDealer.DealDamage(entity, ComboIndex, AttackBehaviour.Weapon);
			FlipToAttacker((BaseEntity)entity);
		}

		private void FlipToAttacker(BaseEntity enemy)
		{
			var enemyX = enemy.transform.position.x;
			var attackerX = Owner.transform.position.x;

			float x = attackerX - enemyX;
			enemy.FlipSprite(x);
		}

		protected void SetAttackTimeOut()
		{
			cooldownTimer.Start("attack", DoAttackCooldown());
		}

		private IEnumerator DoAttackCooldown()
		{
			InAttackCooldown = true;

			yield return new WaitForSeconds(AttackBehaviour.Weapon.AttackSpeed);

			InAttackCooldown = false;
			Owner.State = EState.IDLE;
			cooldownTimer.ReleaseCoroutine("attack");
		}

		protected void SetComboTimeOut()
		{
			cooldownTimer.Stop("combo");
			NextCombo();
			cooldownTimer.Start("combo", DoComboCountdown());
		}

		private void NextCombo()
		{
			ComboIndex++;
			if (ComboIndex >= AttackBehaviour.AttackComponent.ComboCount) 
				ComboIndex = 0;
		}

		private IEnumerator DoComboCountdown()
		{
			var attackObject = AttackBehaviour;
			yield return new WaitForSeconds(attackObject.Weapon.TimeCombo);

			ComboIndex = 0;
			cooldownTimer.ReleaseCoroutine("combo");
		}
	}
}
