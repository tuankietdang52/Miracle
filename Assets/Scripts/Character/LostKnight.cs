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
using Assets.Scripts.Character.Animations;
using Assets.Scripts.Components;
using Assets.Scripts.Manager;
using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Player;

namespace Assets.Scripts.Character
{
	public class LostKnight : BaseCharacter
	{
		protected override string AnimatorPath { get; set; } 
			= "Sprites/Lost Knight/Animations/Lost Knight Animator";

		protected override void SetupAnimation()
		{
			animatorController.runtimeAnimatorController = Resources.Load(AnimatorPath) as RuntimeAnimatorController;
			Animator = new LostKnightAnimator(animatorController);
		}

		[CallByAnimation]
		[SuppressMessage("Usage", "IDE0051")]
		private void DoAttackDamage()
		{
			ICanAttack attackObject;
			try
			{
				attackObject = (ICanAttack)Owner;
			}
			catch
			{
				logger.LogError("Owner cannot attack");
				return;
			}

			attackObject.Attack();
		}
	}
}
