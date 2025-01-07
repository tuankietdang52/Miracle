using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Character
{
	public abstract class BaseAnimator
	{
		protected readonly Logger logger = LoggerExtension.CreateLogger(); 
		public BaseEntity Owner { get; protected set; }
		public Animator AnimatorController { get; set; }

		public abstract void Update();

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
