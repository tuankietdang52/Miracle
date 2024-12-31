using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Character.Animations
{
	public class LostKnightAnimator : IAnimator
	{
		public Animator AnimatorController { get; set; }

		public LostKnightAnimator(Animator animator)
		{
			AnimatorController = animator;
		}

		public void Update(BaseEntity owner)
		{
			UpdateMagnitude(owner);
			UpdateVelocityY(owner);
		}

		private void UpdateMagnitude(BaseEntity owner)
		{
			// for moving, if magnitude > 0.1 play moving, otherwise play idle
			var magnitude = owner.Rb.linearVelocity.magnitude;
			AnimatorController.SetFloat("magnitude", magnitude);
		}

		private void UpdateVelocityY(BaseEntity owner)
		{
			var velocityY = owner.Rb.linearVelocityY;
			AnimatorController.SetFloat("velocityY", velocityY);
			AnimatorController.SetBool("isOnGround", owner.IsOnGround());
		}

		public void Do(string animationName)
		{
			AnimatorController.Play(animationName);
		}

		public void Trigger(string paraName)
		{
			AnimatorController.SetTrigger(paraName);
		}

		public void ResetTrigger(string paraName)
		{
			AnimatorController.ResetTrigger(paraName);
		}

		public void SetInt(string paraName, int value)
		{
			AnimatorController.SetInteger(paraName, value);
		}

		public void SetFloat(string paraName, float value)
		{
			AnimatorController.SetFloat(paraName, value);
		}

		public void SetBool(string paraName, bool value)
		{
			AnimatorController.SetBool(paraName, value);
		}
	}
}
