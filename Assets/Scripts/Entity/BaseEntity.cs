using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
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
	public abstract class BaseEntity : MonoBehaviour
	{
		public GroundCheckObject GroundCheck;
		protected Rigidbody2D Rb => GetComponent<Rigidbody2D>();

		protected virtual void Awake()
		{
			if (!TryGetComponent<Rigidbody2D>(out var rb)) return;
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

		protected virtual void Start()
		{

		}

		protected virtual void Update()
		{
			FlipSprite();
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
		protected virtual void FlipSprite()
		{
			var x = transform.localScale.x;
			if (Rb.linearVelocityX > 0) x = math.abs(x);
			else if (Rb.linearVelocityX < 0) x = math.abs(x) * -1;

			transform.localScale = new(x, transform.localScale.y, transform.localScale.z);
		}

		public bool IsOnGround()
		{
			if (GroundCheck == null) throw new Exception("bruh");
			return GroundCheck.IsOnGround();
		}
	}
}
