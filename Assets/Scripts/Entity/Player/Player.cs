using Assets.Scripts.Action;
using Assets.Scripts.Components;
using UnityEngine;

namespace Assets.Scripts.Entity.Player
{
	public class Player : BaseEntity, IMoveable
	{
		public static Player Instance;

		protected override void Awake()
		{
			base.Awake();
			Instance = Instance == null ? this : Instance;

			DontDestroyOnLoad(this);
		}

		public void Move(Vector3 velocity)
		{
			Rb.linearVelocity = new()
			{
				x = velocity.x == 0 ? Rb.linearVelocityX : velocity.x,
				y = velocity.y == 0 ? Rb.linearVelocityY : velocity.y,
			};
		}
	}
}