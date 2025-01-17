using Assets.Scripts.Effect.EffectFunction;
using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Effect.Debuff
{
	public class Bleeding : IEffect
	{
		public float TimeRemain { get; set; }

		public EEffectType Type { get; } = EEffectType.Debuff;

		public Dictionary<EEffectTarget, IEffectFunction> Effects { get; } = new();

		public Bleeding(float damage, float timeRemain)
		{
			TimeRemain = timeRemain;
			Effects.Add(EEffectTarget.Self, new BleedingFunction(damage));
		}

		public void Activate(BaseEntity owner)
		{
			foreach (var effect in Effects.Values)
			{
				effect.Activate(owner);
			}

			TimeRemain--;
		}

		public bool IsEnd() => TimeRemain <= 0;
	}
}
