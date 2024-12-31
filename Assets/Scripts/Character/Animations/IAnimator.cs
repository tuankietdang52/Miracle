using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Character.Animations
{
	public interface IAnimator
	{
		public Animator AnimatorController { get; set; }

		public void Update(BaseEntity owner);

		public void Do(string animationName);

		public void Trigger(string paraName);

		public void ResetTrigger(string paraName);

		public void SetInt(string paraName, int value);

		public void SetFloat(string paraName, float value);

		public void SetBool(string paraName, bool value);
	}
}
