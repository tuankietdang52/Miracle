using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using Assets.Scripts.Utility;
using Assets.Scripts.Utility.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Character.LostKnight
{
	/// <summary>
	/// Base class for sprite object to play animation, stats for object base on character
	/// </summary>
	[RequireComponent(typeof(Animator))]
	public abstract class BaseCharacter : MonoBehaviour
	{
		protected readonly Logger logger = LoggerExtension.CreateLogger();

		public BaseEntity Owner;
		public BaseAnimator Animator;

		protected abstract string AnimatorPath { get; set; }

		protected Animator animatorController;
		protected abstract void SetupAnimation();

		protected virtual void Awake()
		{
			animatorController = GetComponent<Animator>();
		}

		protected virtual void Start()
		{
			SetupAnimation();
		}

		protected virtual void Update()
		{
			Animator?.Update();
		}

		protected virtual void FixedUpdate()
		{

		}

		protected virtual void LateUpdate()
		{

		}

		[CallByAnimation]
		protected void ResetState()
		{
			Owner.ResetState();
		}
	}
}
