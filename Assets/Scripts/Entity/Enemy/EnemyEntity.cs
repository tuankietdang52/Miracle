using Assets.Scripts.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Entity.Enemy
{
	public abstract class EnemyEntity : BaseEntity
	{
		public EnemyAI AI { get; set; }

		protected override void Awake()
		{
			base.Awake();

			Rb.mass = 10;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			var tag = collision.gameObject.tag;
			float x = collision.collider.transform.position.x;
			float curX = transform.position.x; 

			if (!EnemyTags.Contains(tag)) return;

			float direction = curX < x ? 1f : -1f;
			FlipSprite(direction);
		}
	}
}
