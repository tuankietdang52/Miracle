﻿using Assets.Scripts.Action;
using Assets.Scripts.Action.Attack;
using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Character.Enemy.Skeleton
{
	public class SkeletonAnimator : BaseAnimator
	{
		private bool isTakeHitPlay = false;
		private bool isAttack = false;
		private bool isDead = false;
		private readonly ICanAttack attackBehaviour;

		public SkeletonAnimator(BaseEntity owner, Animator animator, ICanAttack attackBehaviour)
		{
			Owner = owner;
			AnimatorController = animator;
			this.attackBehaviour = attackBehaviour;
		}

		public override void Update()
		{
			if (Owner.IsDead())
			{
				UpdateOnDead();
				return;
			}

			UpdateMagnitude();
			UpdateVelocityY();
			UpdateAttack();
			UpdateTakeHit();
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

		private void UpdateAttack()
		{
			AttackHolder attackHolder = attackBehaviour.AttackHolder;

			if (Owner.State == EState.ATTACKING && !isAttack)
			{
				var comboIndex = attackHolder.GetComboIndex();
				isAttack = true;
				Trigger($"attack{comboIndex}");
			}
			else if (Owner.State != EState.ATTACKING) isAttack = false;
		}

		private void UpdateTakeHit()
		{
			if (Owner.State == EState.TAKEHIT && !isTakeHitPlay)
			{
				Trigger("takeHit");
				isTakeHitPlay = true;
			}
			else if (Owner.State != EState.TAKEHIT) isTakeHitPlay = false;
		}

		private void UpdateOnDead()
		{
			if (isDead) return;

			Trigger("dead");
			SetBool("isDead", true);
			isDead = true;
		}
	}
}
