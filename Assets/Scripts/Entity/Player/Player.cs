using Assets.Scripts.Action;
using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Entity.Player
{
	[RequireComponent(typeof(HealthComponent), typeof(MovementComponent))]
	public class Player : BaseEntity, IMoveable
	{
		public static Player Instance;
		public HealthComponent HealthComponent { get; private set; }
		public MovementComponent MovementComponent { get; private set; }


		protected override void Awake()
		{
			base.Awake();

			animator = new PlayerAnimator(this, GetComponent<Animator>());
			Instance = Instance == null ? this : Instance;
			DontDestroyOnLoad(this);
		}

		protected override void SetupStats()
		{
			HealthComponent = GetComponent<HealthComponent>();
			MovementComponent = GetComponent<MovementComponent>();
		}

		public void Move(Vector3 velocity)
		{
			Rb.linearVelocity = new()
			{
				x = velocity.x == 0 ? Rb.linearVelocityX : velocity.x,
				y = velocity.y == 0 ? Rb.linearVelocityY : velocity.y,
			};

			FlipSprite(velocity.x);
		}
	}
}