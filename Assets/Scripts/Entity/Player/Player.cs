using Assets.Scripts.Action;
using Assets.Scripts.Weapon.Melee;
using Assets.Scripts.Components;
using Assets.Scripts.Weapon;
using Assets.Scripts.Log;
using UnityEngine;
using Assets.Scripts.Action.Attack;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Entity.Player
{
	public class Player : BaseEntity, IMoveable, ICanAttack
	{
		public static Player Instance;

		[SerializeField]
		private AttackHolder attackHolder;
		public AttackHolder AttackHolder { get => attackHolder; set => attackHolder = value; }

		public IWeapon Weapon { get; set; }
		
		#region Stats

		public HealthComponent HealthComponent { get; private set; }
		public MovementComponent MovementComponent { get; private set; }

		public AttackComponent AttackComponent { get; set; }

		#endregion

		protected override void SetupStats()
		{
			HealthComponent = GetComponent<HealthComponent>();
			MovementComponent = GetComponent<MovementComponent>();
			AttackComponent = GetComponent<AttackComponent>();
		}

		protected override void Awake()
		{
			base.Awake();

			Weapon = new RustySword(this);

			Instance = Instance == null ? this : Instance;
			DontDestroyOnLoad(this);
		}

		protected override void Start()
		{
			base.Start();
			AttackHolder.Owner = this;
		}

		public void DoAnimationAttack()
		{
			if (State != EState.FREE) return;

			Character.Animator.Trigger($"attack{attackHolder.GetComboIndex()}");
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
			if (State != EState.FREE) return;

			Rb.linearVelocity = new()
			{
				x = velocity.x == 0 ? Rb.linearVelocityX : velocity.x,
				y = velocity.y == 0 ? Rb.linearVelocityY : velocity.y,
			};

			FlipSprite(velocity.x);
		}
	}
}