using Assets.Scripts.Action;
using Assets.Scripts.Action.Attack;
using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Character.LostKnight
{
	public class LostKnightAnimator : BaseAnimator
	{
		private readonly ICanAttack attackBehaviour;

		public LostKnightAnimator(BaseEntity owner, Animator animator, ICanAttack attackBehaviour)
		{
			Owner = owner;
			AnimatorController = animator;
			this.attackBehaviour = attackBehaviour;

			owner.OnStateChanged += TrackingState;
		}

		private void TrackingState(object sender, EState state)
		{
			switch (state)
			{
				case EState.Attacking:
					DoAttack();
					break;

				case EState.TakeHit:
					DoTakeHit();
					break;

				case EState.Dead:
					OnDead();
					break;

				default:
					return;
			}
		}

		public override void Update()
		{
			if (Owner.IsDead()) return;

			UpdateMagnitude();
			UpdateVelocityY();
		}

		private void UpdateMagnitude()
		{
			// for moving, if magnitude > 0.1 play moving, otherwise play idle
			var magnitude = Owner.Rb.linearVelocity.magnitude;
			AnimatorController.SetFloat("magnitude", magnitude);
		}

		private void UpdateVelocityY()
		{
			var velocityY = Owner.Rb.linearVelocityY;
			AnimatorController.SetFloat("velocityY", velocityY);
			AnimatorController.SetBool("isOnGround", Owner.IsOnGround());
		}

		private void DoAttack()
		{
			AttackHolder attackHolder = attackBehaviour.AttackHolder;

			var comboIndex = attackHolder.GetComboIndex();
			Trigger($"attack{comboIndex}");
		}

		private void DoTakeHit()
		{
			Trigger("takeHit");
		}

		private void OnDead()
		{
			Trigger("dead");
			SetBool("isDead", true);
		}
	}
}
