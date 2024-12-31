using Assets.Scripts.Action;
using Assets.Scripts.Character;
using Assets.Scripts.Log;
using Assets.Scripts.Utility;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Entity
{
	/// <summary>
	/// An abstract class for all entity in game
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
	public abstract class BaseEntity : MonoBehaviour
	{
		protected Logger logger = LoggerExtension.CreateLogger();
		public BaseCharacter Character;

		public GroundCheckObject GroundCheck;
		public Rigidbody2D Rb { get; protected set; }
		public Collider2D Collider { get; protected set; }

		public EState State = EState.FREE;

		public int Level = 0;

		protected abstract void SetupStats();

		protected virtual void Awake()
		{
			Rb = GetComponent<Rigidbody2D>();
			Collider = GetComponent<Collider2D>();

			Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			Rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

			SetupStats();
		}

		protected virtual void Start()
		{

		}

		protected virtual void Update()
		{

		}

		protected virtual void FixedUpdate()
		{

		}

		protected virtual void LateUpdate()
		{

		}

		/// <summary>
		/// Flip sprite base on direction of entity when move
		/// </summary>
		protected virtual void FlipSprite(float velocityX)
		{
			var scaleX = transform.localScale.x;
			if (velocityX > 0) scaleX = math.abs(scaleX);
			else if (velocityX < 0) scaleX = math.abs(scaleX) * -1;

			transform.localScale = new(scaleX, transform.localScale.y, transform.localScale.z);
		}

		public void ResetState()
		{
			State = EState.FREE;
		}

		public bool IsFacingRight()
		{
			return transform.localScale.x > 0;
		}

		public bool IsOnGround()
		{
			if (GroundCheck == null) logger.LogError("Can't found Ground Check Object");
			return GroundCheck.IsOnGround();
		}

		public bool IsOnPlatform()
		{
			if (GroundCheck == null) logger.LogError("Can't found Ground Check Object");
			return GroundCheck.IsOnPlatform();
		}

		public void DropFromPlatform()
		{
			StartCoroutine(DroppingFromPlatform());
		}

		private IEnumerator DroppingFromPlatform()
		{
			if (!IsOnPlatform()) yield break;

			if (!TryGetComponent<PlatformEffector2D>(out var platformEffector))
			{
				logger.LogError("Can't found PlatformEffector");
				yield break;
			}

			// exclude platform layer for collider, mean that entity will ignore collide
			// with platform layer
			platformEffector.useColliderMask = false;
			Collider.excludeLayers = LayerMaskStorage.Platform;
			GroundCheck.IsDropFromPlatform = true;

			yield return new WaitForSeconds(0.25f);

			// reset collider
			ResetColliderAfterDrop(platformEffector);
		}

		private void ResetColliderAfterDrop(PlatformEffector2D platformEffector)
		{
			platformEffector.useColliderMask = true;
			Collider.excludeLayers = 0;
			GroundCheck.IsDropFromPlatform = false;
		}
	}
}
