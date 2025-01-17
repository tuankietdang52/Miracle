using Assets.Scripts.Components;
using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using Assets.Scripts.Utility.Cooldown;
using Assets.Scripts.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Action.Attack
{
	public abstract class AttackHandler : MonoBehaviour
	{
		protected readonly Logger logger = LoggerExtension.CreateLogger();

		public AttackHolder attackHolder;

		public int ComboIndex { get; protected set; }

		public bool InAttackCooldown { get; protected set; } = false;
		protected ICanAttack Owner;

		private CooldownTimer cooldownTimer;
		private DamageDealer damageDealer;

		public void SetupHandler(ICanAttack owner, AttackHolder attackHolder)
		{
			InAttackCooldown = false;
			Owner = owner;
			this.attackHolder = attackHolder;

			transform.parent = attackHolder.transform;
		}

		public void Start()
		{
			damageDealer = new(Owner.AttackComponent);
			cooldownTimer = new(this);
		}

		public abstract void Attacking();


		private void Update()
		{
			transform.position = transform.parent.position;
		}

		private void OnDrawGizmosSelected()
		{
			var comp = Owner.AttackComponent;

			if (Owner.Weapon == null) return;

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
			float damage = damageDealer.GetDamageDeal(entity, ComboIndex, Owner.Weapon);
			if (damage != -1) entity.TakingHit(attackHolder.Owner, damage);
		}

		protected void SetAttackTimeOut()
		{
			cooldownTimer.Start("attack", DoAttackCooldown());
		}

		private IEnumerator DoAttackCooldown()
		{
			InAttackCooldown = true;
			yield return new WaitForSeconds(Owner.Weapon.AttackSpeed);
			InAttackCooldown = false;

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
			if (ComboIndex >= Owner.AttackComponent.ComboCount) 
				ComboIndex = 0;
		}

		private IEnumerator DoComboCountdown()
		{
			var attackObject = Owner;
			yield return new WaitForSeconds(attackObject.Weapon.TimeCombo);

			ComboIndex = 0;
			cooldownTimer.ReleaseCoroutine("combo");
		}
	}
}
