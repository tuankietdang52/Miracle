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
using Assets.Scripts.Manager;
using Assets.Scripts.Utility.InspectorComponent;
using Assets.Scripts.Weapon;
using Assets.Scripts.Weapon.Melee;
using UnityEngine;

namespace Assets.Scripts.Entity.Enemy.Minions
{
	public class Skeleton : BaseEntity, INpc, IMoveable, ICanAttack, IAttackable
	{
		public EnemyAI AI { get; set; }

		public FieldOfView FOV;

		[SerializeField]
		private AttackHolder attackHolder;
		public AttackHolder AttackHolder { get => attackHolder; set => attackHolder = value; }
		public BaseWeapon Weapon { get; set; }

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
			if (State != EState.IDLE) return;

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
			if (State != EState.IDLE) return;
			State = EState.ATTACKING;
		}

		public void DecreaseHealth(float value)
		{
			if (IsDead()) return;

			HealthComponent.Health -= value;
			State = EState.TAKEHIT;
		}

		public void Dead()
		{
			if (IsDead()) return;
			State = EState.DEAD;
		}
	}
}
