using Assets.Scripts.Action;
using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utility
{
	public static class EntityExtension
	{
		public static float GetDirectionTo(this BaseEntity cur, BaseEntity target)
		{
			return cur.transform.position.x < target.transform.position.x ? 1f : -1f;
		}

		public static bool IsAttackable(this BaseEntity entity)
		{
			return entity.TryConvertTo<IAttackable>(out var _) && !entity.IsDead();
		}
	}
}
