using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Effect
{
	public enum EEffectTarget
	{
		Self,
		OnAttackEnemy,
		OnAbilityHitEnemy,
		OnAttacker
	}

	public enum EEffectType
	{
		Buff,
		Debuff
	}

	public interface IEffect
	{
		/// <summary>
		/// Set to -1 if this effect is permanent
		/// </summary>
		public float TimeRemain { get; set; }

		public EEffectType Type { get; }

		public Dictionary<EEffectTarget, IEffectFunction> Effects { get; }

		public void Activate(BaseEntity owner);

		public bool IsEnd();
	}
}
