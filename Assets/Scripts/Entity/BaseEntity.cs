using Assets.Scripts.Action;
using Assets.Scripts.Character.LostKnight;
using Assets.Scripts.Log;
using Assets.Scripts.Manager;
using Assets.Scripts.Utility;
using Assets.Scripts.Utility.Cooldown;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using Assets.Scripts.Effect;
using Assets.Scripts.Effect.Debuff;

namespace Assets.Scripts.Entity
{
	public delegate void OnStateChangedHandler(object sender, EState state);

	/// <summary>
	/// An abstract class for all entity in game
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
	public abstract class BaseEntity : MonoBehaviour
	{
		protected Logger logger = LoggerExtension.CreateLogger();
		protected CooldownTimer cooldownTimer;

		public BaseCharacter Character;

		public GroundCheckObject GroundCheck;
		public Rigidbody2D Rb { get; protected set; }
		public Collider2D Collider { get; protected set; }

		public event OnStateChangedHandler OnStateChanged;

		[SerializeField]
		private EState state = EState.Idle;
		public EState State { 
			get => state;
			set
			{
				state = value;
				OnStateChanged.Invoke(this, state);
			}
		}

		public string[] EnemyTags { get; set; } = { };

		public UniqueClassCollection<IEffect> Effects = new();

		public int Level = 0;

		protected abstract void SetupStats();

		private void SetupCollisionAndPhysic()
		{
			Rb = GetComponent<Rigidbody2D>();
			Collider = GetComponent<Collider2D>();

			Rb.mass = 10;
			Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			Rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		}

		protected virtual void Awake()
		{
			SetupCollisionAndPhysic();
			cooldownTimer = new(this);
			SetupStats();
		}

		protected virtual void Start()
		{
			EnemyTags = GameManager.Instance.GetEnemyTag(tag);
			cooldownTimer.Start("doEffect", DoEffects());
		}

		private IEnumerator DoEffects()
		{
			List<IEffect> endEffect = new();

			while (true)
			{
				yield return new WaitForSeconds(1f);

				foreach (var effect in Effects.Values)
				{
					effect.Activate(this);
					if (effect.IsEnd()) endEffect.Add(effect);
				}

				for (int i = endEffect.Count - 1; i >= 0; i--)
				{
					Effects.Remove(endEffect[i]);
					endEffect.RemoveAt(i);
				}
			}
		}

		protected virtual void Update()
		{
			if (IsDead()) return; 
		}

		protected virtual void FixedUpdate()
		{
			if (IsDead()) return;
		}

		protected virtual void LateUpdate()
		{
			if (State == EState.Destroy && !CompareTag("Player"))
			{
				Destroy(gameObject);
			}
		}

		/// <summary>
		/// Convert this to TInterface, null if not valid
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <returns></returns>
		public TInterface GetConvertTo<TInterface>() where TInterface : class
		{
			if (!typeof(TInterface).IsInterface) return null;
			if (this is not TInterface entity) return null;
			
			return entity;
		}

		/// <summary>
		/// Try convert this to TInterface, false if not valid
		/// </summary>
		/// <typeparam name="TInterface"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool TryConvertTo<TInterface>(out TInterface entity) where TInterface : class
		{
			entity = null;

			if (!typeof(TInterface).IsInterface) return false;
			if (this is not TInterface temp) return false;

			entity = temp;

			return true;
		}

		/// <summary>
		/// Flip sprite base on direction of entity when move
		/// </summary>
		public virtual void FlipSprite(float velocityX)
		{
			var scaleX = transform.localScale.x;
			if (velocityX > 0) scaleX = math.abs(scaleX);
			else if (velocityX < 0) scaleX = math.abs(scaleX) * -1;

			transform.localScale = new(scaleX, transform.localScale.y, transform.localScale.z);
		}

		public void FlipHorizontal()
		{
			var scaleX = transform.localScale.x;
			var absX = math.abs(scaleX);
			scaleX = scaleX > 0 ? -absX : absX;

			transform.localScale = new(scaleX, transform.localScale.y, transform.localScale.z);
		}

		public void ResetState()
		{
			State = EState.Idle;
		}

		public bool IsDead() => State == EState.Dead || State == EState.Destroy;

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
			cooldownTimer.Start("drop", DroppingFromPlatform());
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
			cooldownTimer.ReleaseCoroutine("drop");
		}

		private void ResetColliderAfterDrop(PlatformEffector2D platformEffector)
		{
			platformEffector.useColliderMask = true;
			Collider.excludeLayers = 0;
			GroundCheck.IsDropFromPlatform = false;
		}
	}
}
