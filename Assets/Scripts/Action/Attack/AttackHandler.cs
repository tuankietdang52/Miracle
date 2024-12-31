using Assets.Scripts.Components;
using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using Assets.Scripts.Utility.Cooldown;
using Assets.Scripts.Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		protected AttackComponent attackStats;

		private CooldownTimer cooldownTimer;

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
				attackStats = ((ICanAttack)Owner).AttackComponent;
			}
			catch (Exception e)
			{
				logger.LogError("Owner cannot attack");
				logger.LogException(e);
			}

			cooldownTimer = new(this);
		}

		public abstract void Attacking();

		private void Update()
		{
			transform.position = transform.parent.position;
		}

		private void OnDrawGizmosSelected()
		{
			var attackObject = (ICanAttack) Owner;
			var comp = attackObject.AttackComponent;

			if (attackObject.Weapon == null) return;

			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, comp.AttackRange[ComboIndex]);
		}

		protected void SetAttackTimeOut()
		{
			cooldownTimer.Start("attack", DoAttackCooldown);
		}

		private IEnumerator DoAttackCooldown()
		{
			InAttackCooldown = true;

			var attackObject = (ICanAttack)Owner;
			yield return new WaitForSeconds(attackObject.Weapon.AttackSpeed);

			InAttackCooldown = false;
			Owner.State = EState.FREE;
			cooldownTimer.ReleaseCoroutine("attack");
		}

		protected void SetComboTimeOut()
		{
			cooldownTimer.Stop("combo");
			NextCombo();
			cooldownTimer.Start("combo", DoComboCountdown);
		}

		private void NextCombo()
		{
			ComboIndex++;
			if (ComboIndex >= ((ICanAttack)Owner).AttackComponent.ComboCount) 
				ComboIndex = 0;
		}

		private IEnumerator DoComboCountdown()
		{
			var attackObject = (ICanAttack)Owner;
			yield return new WaitForSeconds(attackObject.Weapon.TimeCombo);

			ComboIndex = 0;
			cooldownTimer.ReleaseCoroutine("combo");
		}
	}
}
