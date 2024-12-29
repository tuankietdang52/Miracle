using Assets.Scripts.Animation;
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
		protected IAnimator animator;

		public GroundCheckObject GroundCheck;
		public Rigidbody2D Rb { get; protected set; }
		public Collider2D Collider { get; protected set; }

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
			animator?.Update();
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

		public bool IsOnGround()
		{
			if (GroundCheck == null) logger.LogError("Cant found Ground Check Object");
			return GroundCheck.IsOnGround();
		}

		public bool IsOnPlatform()
		{
			if (GroundCheck == null) logger.LogError("Cant found Ground Check Object");
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
				logger.LogError("Cant found PlatformEffector");
				yield break;
			}

			platformEffector.useColliderMask = false;
			Collider.excludeLayers = LayerMaskStorage.Platform;
			GroundCheck.IsDropFromPlatform = true;

			yield return new WaitForSeconds(0.25f);
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
