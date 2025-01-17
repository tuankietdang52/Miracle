using Assets.Scripts.Action;
using Assets.Scripts.Weapon.Melee;
using Assets.Scripts.Components;
using Assets.Scripts.Weapon;
using Assets.Scripts.Log;
using UnityEngine;
using Assets.Scripts.Action.Attack;
using Assets.Scripts.Manager;
using System.Collections;
using Assets.Scripts.Effect.Buff;
using System.Collections.Generic;
using Assets.Scripts.Effect;
using Assets.Scripts.Utility;
using System;

namespace Assets.Scripts.Entity.Player
{
	public class Player : BaseEntity, IMoveable, ICanAttack, IAttackable
	{
		public static Player Instance;

		[SerializeField]
		private AttackHolder attackHolder;
		public AttackHolder AttackHolder { get => attackHolder; set => attackHolder = value; }

		public BaseWeapon Weapon { get; set; }

		public List<IAttackEffect> AttackEffects { get; } = new();

		#region Stats

		public HealthComponent HealthComponent { get; set; }
		public MovementComponent MovementComponent { get; set; }

		public AttackComponent AttackComponent { get; set; }

		#endregion

		protected override void SetupStats()
		{
			HealthComponent = GetComponent<HealthComponent>();
			MovementComponent = GetComponent<MovementComponent>();
			AttackComponent = GetComponent<AttackComponent>();
			Weapon = GetComponent<BaseWeapon>();

			Effects.Add(new BasicRegeneration(HealthComponent.HealthRegeneration));
		}

		protected override void Awake()
		{
			base.Awake();

			Rb.mass = 0;
			Instance = Instance == null ? this : Instance;
			DontDestroyOnLoad(this);
		}

		protected override void Start()
		{
			base.Start();
			AttackHolder.Owner = this;
		}

		protected override void Update()
		{
			if (HealthComponent.Health <= 0)
			{
				Dead();
				return;
			}

			base.Update();
		}

		public void DoAnimationAttack()
		{
			if (State != EState.Idle) return;
			if (!attackHolder.InAttackCooldown()) State = EState.Attacking;		
		}

		public void Attack()
		{
			if (attackHolder == null)
			{
				logger.LogError("Can't found attack point");
				return;
			}

			attackHolder.DoAttack();
		}

		public void Move(Vector3 velocity)
		{
			if (State != EState.Idle) return;

			Rb.linearVelocity = new()
			{
				x = velocity.x == 0 ? Rb.linearVelocityX : velocity.x,
				y = velocity.y == 0 ? Rb.linearVelocityY : velocity.y,
			};

			FlipSprite(velocity.x);
		}

		public void TakingHit(BaseEntity attacker, float value)
		{
			DecreaseHealth(value);
			float direction = this.GetDirectionTo(attacker);
			FlipSprite(direction);
		}

		public void DecreaseHealth(float value)
		{
			if (IsDead()) return;

			HealthComponent.Health -= value;
			State = EState.TakeHit;
		}

		public void Dead()
		{
			if (IsDead()) return;
			State = EState.Dead;
		}
	}
}