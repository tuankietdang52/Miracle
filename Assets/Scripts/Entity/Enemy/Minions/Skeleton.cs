using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Action;
using Assets.Scripts.Action.Attack;
using Assets.Scripts.AI;
using Assets.Scripts.Character.Enemy.Skeleton;
using Assets.Scripts.Components;
using Assets.Scripts.Effect;
using Assets.Scripts.Effect.Buff;
using Assets.Scripts.Manager;
using Assets.Scripts.Utility;
using Assets.Scripts.Utility.InspectorComponent;
using Assets.Scripts.Weapon;
using Assets.Scripts.Weapon.Melee;
using UnityEngine;

namespace Assets.Scripts.Entity.Enemy.Minions
{
	public class Skeleton : EnemyEntity, INpc, IMoveable, ICanAttack, IAttackable
	{
		public FieldOfView FOV;

		[SerializeField]
		private AttackHolder attackHolder;
		public AttackHolder AttackHolder { get => attackHolder; set => attackHolder = value; }
		public BaseWeapon Weapon { get; set; }

		public List<IAttackEffect> AttackEffects { get; set; }

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

			Effects.Add(new SharpBlade(this));
		}

		protected override void Awake()
		{
			base.Awake();

			AI = new SkeletonAI(this);
			AI.Awake();
		}

		protected override void Start()
		{
			base.Start();
			AI.Start();
		}

		protected override void Update()
		{
			if (HealthComponent.Health <= 0)
			{
				Dead();
				return;
			}

			base.Update();
			AI.Update();

			logger.Log(Effects.Count);
		}

		protected override void FixedUpdate()
		{
			base.FixedUpdate();
			AI.FixedUpdate();
		}

		protected override void LateUpdate()
		{
			base.LateUpdate();
			AI.LateUpdate();
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

		public void Attack()
		{
			attackHolder.DoAttack();
		}

		public void DoAnimationAttack()
		{
			if (State != EState.Idle) return;
			if (!attackHolder.InAttackCooldown()) State = EState.Attacking;
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
