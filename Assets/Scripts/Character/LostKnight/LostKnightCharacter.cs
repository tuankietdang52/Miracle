using Assets.Scripts.Action;
using Assets.Scripts.Action.Attack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Log;
using UnityEngine;
using Assets.Scripts.Utility;
using System.Diagnostics.CodeAnalysis;
using Assets.Scripts.Components;
using Assets.Scripts.Manager;
using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Player;

namespace Assets.Scripts.Character.LostKnight
{
	public class LostKnightCharacter : BaseCharacter
	{
		protected override string AnimatorPath { get; set; } 
			= "Sprites/The Chruch/Lost Knight/Animations/Lost Knight Animator";

		private ICanAttack attackBehaviour;

		protected override void SetupAnimation()
		{
			try
			{
				attackBehaviour = Owner.IsImplement<ICanAttack>();
			}
			catch
			{
				logger.LogError("Missing Component");
				return;
			}

			animatorController.runtimeAnimatorController = Resources.Load(AnimatorPath) as RuntimeAnimatorController;
			Animator = new LostKnightAnimator(Owner, GetComponent<Animator>(), attackBehaviour);
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
