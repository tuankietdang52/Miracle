using Assets.Scripts.Action;
using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using Assets.Scripts.Utility.Cooldown;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

using ELayerMask = Assets.Scripts.Utility.LayerMaskStorage.ELayerMask;

namespace Assets.Scripts.Utility.InspectorComponent
{

	public class FieldOfView : MonoBehaviour
	{
		public BaseEntity Owner;

		private Logger logger = LoggerExtension.CreateLogger();

		public float radius;
		[Range(0, 90f)]
		public float angle;

		private LayerMask targetLayer;
		private LayerMask obstacleLayer;

		private BaseEntity target;

		private CooldownTimer cooldownTimer;

		private void Awake()
		{
			cooldownTimer = new(this);

			targetLayer = LayerMaskStorage.Entity;
			obstacleLayer = LayerMaskStorage.GetMultipleMasks(new List<ELayerMask>() {
				ELayerMask.Ground
			});
		}

		private void Start()
		{
			cooldownTimer.Start("find", DetectEnemy());
		}

		/// <summary>
		/// Return first target if found, null if not target be found
		/// </summary>
		/// <returns></returns>
		public BaseEntity TargetFound() => target;

		private IEnumerator DetectEnemy()
		{
			while (true)
			{
				// scan each 2 seconds
				yield return new WaitForSeconds(0.2f);
				Detecting();
			}
		}

		private bool IsValid(Collider2D collider, out BaseEntity enemy)
		{
			BaseEntity temp = null;

			bool isEnemy = Owner.EnemyTags.Length != 0 &&
				Owner.EnemyTags.Contains(collider.gameObject.tag);

			var result = isEnemy
				&& collider.gameObject != Owner.gameObject
				&& collider.TryGetComponent(out temp)
				&& temp.IsAttackable();

			enemy = result ? temp : null;
			return enemy != null;
		}

		private void Detecting()
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
			BaseEntity enemy = null;

			// filt found enemies by tag and BaseEntity component
			var isDetect = 
				colliders.FirstOrDefault(collider => IsValid(collider, out enemy));

			if (!isDetect)
			{
				target = null;
				return;
			}
			CheckEnemy(enemy);
		}

		private void CheckEnemy(BaseEntity enemy)
		{
			var curPos = transform.position;
			var enemyPos = enemy.transform.position;

			var direction = (enemyPos - curPos).normalized;
			Vector2 from = Owner.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

			// outside the cone
			if (Vector2.Angle(from, direction) > angle / 2)
			{
				target = null;
				return;
			}

			float distance = Vector2.Distance(curPos, enemyPos);

			// check if there is obstacles in a way
			if (!Physics2D.Raycast(curPos, direction, distance, obstacleLayer))
			{
				target = enemy;
			}
			else target = null;
		}

		private void OnDrawGizmos()
		{
			if (Owner == null) return;

			// draw circle
			Handles.color = new Color(1f, 1f, 1f, 0.3f);
			Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

			// draw line
			Vector2 angle1 = DirectionFromAngle(-transform.eulerAngles.z, -angle / 2);
			Vector2 angle2 = DirectionFromAngle(-transform.eulerAngles.z, angle / 2);

			Gizmos.color = Color.green;
			Gizmos.DrawLine(transform.position, (Vector2)transform.position + angle1 * radius);
			Gizmos.DrawLine(transform.position, (Vector2)transform.position + angle2 * radius);

			// draw target
			if (TargetFound())
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(transform.position, target.transform.position);
			}
		}

		private Vector2 DirectionFromAngle(float eulerY, float angleInDegree)
		{
			var scaleX = Owner.transform.localScale.x;

			angleInDegree += eulerY;

			var x = math.cos(angleInDegree * Mathf.Deg2Rad);
			var y = math.sin(angleInDegree * Mathf.Deg2Rad);

			x = scaleX < 0 ? -x : x;
			y = scaleX < 0 ? -y : y;
			
			return new Vector2(x, y);
		}
	}
}
