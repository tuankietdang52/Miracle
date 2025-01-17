using Assets.Scripts.Action;
using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Effect.EffectFunction
{
	/// <summary>
	/// Healing owner overtime
	/// </summary>
	public class RegenerationFunction : IEffectFunction
	{
		private readonly float healthPerSeconds;

		public RegenerationFunction(float healthPerSeconds)
		{
			this.healthPerSeconds = healthPerSeconds;
		}

		public void Activate(BaseEntity owner)
		{
			if (owner.IsDead()) return;

			if (!owner.TryConvertTo<IAttackable>(out var isAttackable))
				return;

			var healthComp = isAttackable.HealthComponent;

			healthComp.Health += healthPerSeconds;
			if (healthComp.Health > healthComp.MaxHealth)
			{
				healthComp.Health = healthComp.MaxHealth;
			}
		}
	}
}
