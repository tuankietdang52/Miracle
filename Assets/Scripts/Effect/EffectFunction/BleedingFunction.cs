using Assets.Scripts.Action;
using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Effect.EffectFunction
{
	public class BleedingFunction : IEffectFunction
	{
		private readonly float damage;

		public BleedingFunction(float damage)
		{
			this.damage = damage;
		}

		public void Activate(BaseEntity owner)
		{
			if (owner.IsDead()) return;

			if (!owner.TryConvertTo<IAttackable>(out var isAttackable))
				return;

			var healthComp = isAttackable.HealthComponent;

			healthComp.Health -= damage;
		}
	}
}
