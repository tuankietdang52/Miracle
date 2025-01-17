using Assets.Scripts.Action.Attack;
using Assets.Scripts.Effect.Debuff;
using Assets.Scripts.Effect.EffectFunction;
using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace Assets.Scripts.Effect.Buff
{
	public class SharpBlade : IEffect
	{
		private readonly ICanAttack owner;

		public float TimeRemain { get; set; } = 20f;

		public EEffectType Type => EEffectType.Buff;

		public Dictionary<EEffectTarget, IEffectFunction> Effects { get; }

		private List<IEffect> OnHitEffects;

		public SharpBlade(BaseEntity owner)
		{
			this.owner = owner.GetConvertTo<ICanAttack>();
			this.owner.AttackHolder.OnAttackSuccess += ApplyToAttackedEnemy;
		}

		public void Activate(BaseEntity owner)
		{
			TimeRemain--;
		}

		private void ApplyToAttackedEnemy(object sender, OnAttackEventArgs args)
		{
			var enemy = args.Target;

			OnHitEffects = new()
			{
				new Bleeding(20, 10)
			};
			
			foreach (var effect in OnHitEffects)
			{
				enemy.Effects.Add(effect);
			}
		}

		public bool IsEnd()
		{
			if (TimeRemain <= 0)
			{
				this.owner.AttackHolder.OnAttackSuccess -= ApplyToAttackedEnemy;
				return true;
			}

			return false;
		}
	}
}
