using Assets.Scripts.Effect.Buff;
using Assets.Scripts.Effect.EffectFunction;
using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Effect.Buff
{
	public class BasicRegeneration : IEffect
	{
		public float TimeRemain { get; set; }

		public EEffectType Type { get; } = EEffectType.Buff;

		public Dictionary<EEffectTarget, IEffectFunction> Effects { get; } = new();

		public BasicRegeneration(float healthPerSeconds)
		{
			Effects.Add(EEffectTarget.Self, new RegenerationFunction(healthPerSeconds));
		}

		public void Activate(BaseEntity owner)
		{
			foreach (var effect in Effects.Values)
			{
				effect.Activate(owner);
			}
		}

		public bool IsEnd() => false;
	}
}
