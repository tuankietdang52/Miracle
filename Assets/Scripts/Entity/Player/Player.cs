using Assets.Scripts.Action;
using Assets.Scripts.Weapon.Melee;
using Assets.Scripts.Components;
using Assets.Scripts.Weapon;
using Assets.Scripts.Log;
using UnityEngine;
using Assets.Scripts.Action.Attack;
using Assets.Scripts.Manager;
using System.Collections;

namespace Assets.Scripts.Entity.Player
{
	public class Player : BaseEntity, IMoveable, ICanAttack, IAttackable
	{
		public static Player Instance;

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
			if (State != EState.IDLE) return;
			State = EState.ATTACKING;
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
			if (State != EState.IDLE) return;

			Rb.linearVelocity = new()
			{
				x = velocity.x == 0 ? Rb.linearVelocityX : velocity.x,
				y = velocity.y == 0 ? Rb.linearVelocityY : velocity.y,
			};

			FlipSprite(velocity.x);
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