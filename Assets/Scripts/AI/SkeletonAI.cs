using Assets.Scripts.Action;
using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Enemy.Minions;
using Assets.Scripts.Utility.Cooldown;
using Assets.Scripts.Utility.InspectorComponent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
	public class SkeletonAI : EnemyAI
	{
		private readonly Skeleton owner;
		private BaseEntity curTarget = null;
		private readonly CooldownTimer cooldownTimer;
		private bool isAttack = false;

		public SkeletonAI(Skeleton owner)
		{
			this.owner = owner;
			cooldownTimer = new(owner);
		}

		public override void Update()
		{
			base.Update();

			FindingEnemy();
		}

		public override void FixedUpdate()
		{
			base.FixedUpdate();

			var enemy = owner.FOV.TargetFound();
			if (enemy == null) return;
			MoveToTarget(enemy);
		}

		private void FindingEnemy()
		{
			var enemy = owner.FOV.TargetFound();

			if (enemy != null)
			{
				curTarget = enemy;
				Attack();
			}
			else if (curTarget != null) cooldownTimer.Start("find", FindTargetAfterLost(curTarget));
		}

		private IEnumerator FindTargetAfterLost(BaseEntity enemy)
		{
			yield return new WaitForSeconds(0.5f);

			float enemyX = enemy.transform.position.x;
			float ownerX = owner.transform.position.x;

			bool isRight = enemyX >= ownerX;
			float directionX = isRight ? 1f : -1f;

			owner.FlipSprite(directionX);
			curTarget = null;

			cooldownTimer.ReleaseCoroutine("find");
		}

		private void MoveToTarget(BaseEntity target)
		{
			if (isAttack) return;
			if (owner.State != EState.Idle) return;

			var movement = owner.MovementComponent;
			float velocityX = target.transform.position.x >= owner.transform.position.x ?
				movement.Speed * Time.deltaTime : -(movement.Speed * Time.deltaTime);

			Vector2 velocity = new()
			{
				x = velocityX,
			};

			owner.Move(velocity);
		}

		private void Attack()
		{
			if (owner.State != EState.Idle) return;
			if (owner.AttackHolder.IsEnemiesOnAttackRange())
			{
				isAttack = true;
				owner.DoAnimationAttack();
			}
			else isAttack = false;
		}
	}
}
