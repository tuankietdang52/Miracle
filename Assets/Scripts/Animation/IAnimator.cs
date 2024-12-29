using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Animation
{
	public interface IAnimator
	{
		void Update();
		void Play(string animationName);
		void SetInt(string paraName, int value);
		void SetFloat(string paraName, float value);
		void SetBool(string paraName, bool value);

		void Trigger(string paraName);
		void ResetTrigger(string paraName);
	}
}
