using Assets.Scripts.Action;
using Assets.Scripts.Action.Attack;
using Assets.Scripts.Character.LostKnight;
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
	public class SkeletonCharacter : BaseCharacter
	{
		protected override string AnimatorPath { get; set; } 
			= "Sprites/The Chruch/Skeleton/Animations/Skeleton Animator";

		private ICanAttack attackBehaviour;

		protected override void SetupAnimation()
		{
			try
			{
				attackBehaviour = Owner.IsImplement<ICanAttack>();
			}
			catch
			{
				logger.Log("Missing Component");
				return;
			}
			animatorController.runtimeAnimatorController = Resources.Load(AnimatorPath) as RuntimeAnimatorController;
			Animator = new SkeletonAnimator(Owner, GetComponent<Animator>(), attackBehaviour);
		}

		[CallByAnimation]
		[SuppressMessage("Usage", "IDE0051")]
		private void DoAttackDamage()
		{
			attackBehaviour.Attack();
		}

		[CallByAnimation]
		[SuppressMessage("Usage", "IDE0051")]
		private void EndTakeHitAnimation()
		{
			Animator.Trigger("endTakeHit");
		}

		[CallByAnimation]
		[SuppressMessage("Usage", "IDE0051")]
		private void Decay()
		{
			if (!Owner.IsDead()) return;
			Animator.Trigger("decay");
		}

		[CallByAnimation]
		[SuppressMessage("Usage", "IDE0051")]
		private void Destroy()
		{
			Owner.State = EState.DESTROY;
		}
	}
}
