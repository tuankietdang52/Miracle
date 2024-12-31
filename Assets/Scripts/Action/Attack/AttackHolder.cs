using Assets.Scripts.Entity;
using Assets.Scripts.Log;
using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Action.Attack
{
	public class AttackHolder : MonoBehaviour
	{
		public BaseEntity Owner;
		public AttackHandler HandlerPrefab;

		[HideInInspector]
		public AttackHandler AttackHandler;

		public Vector2 SampleAttackRange;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, SampleAttackRange);
		}

		private void Start()
		{
			AttackHandler =
				PrefabManager.ClonePrefab(HandlerPrefab, this, LayerMaskStorage.ELayerMask.Utility);
			AttackHandler.SetupHandler(Owner, this);
		}

		public void DoAttack()
		{
			AttackHandler.Attacking();
		}

		public int GetComboIndex()
		{
			return AttackHandler.ComboIndex;
		}
	}
}
