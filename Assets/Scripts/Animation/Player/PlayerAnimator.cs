using Assets.Scripts.Animation;
using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using Assets.Scripts.Utility;
using System;
using System.ComponentModel;
using UnityEngine;

public class PlayerAnimator : IAnimator
{
	private readonly Animator _animator;
	private readonly BaseEntity _owner;

	public PlayerAnimator(BaseEntity owner, Animator animator)
	{
		_animator = animator;
		_owner = owner;
	}

	public void Update()
	{
		UpdateMagnitude();
		UpdateVelocityY();
	}

	private void UpdateMagnitude()
	{
		var magnitude = _owner.Rb.linearVelocity.magnitude;
		SetFloat("magnitude", magnitude);
	}

	private void UpdateVelocityY()
	{
		var velocityY = _owner.Rb.linearVelocityY;
		SetFloat("velocityY", velocityY);
		SetBool("isOnGround", _owner.IsOnGround());
	}

	public void Play(string animationName)
	{
		_animator.Play(animationName);
	}

	public void Trigger(string paraName)
	{
		_animator.SetTrigger(paraName);
	}

	public void ResetTrigger(string paraName)
	{
		_animator.ResetTrigger(paraName);
	}

	public void SetInt(string paraName, int value)
	{
		_animator.SetInteger(paraName, value);
	}

	public void SetFloat(string paraName, float value)
	{
		_animator.SetFloat(paraName, value);
	}

	public void SetBool(string paraName, bool value)
	{
		_animator.SetBool(paraName, value);
	}
}
