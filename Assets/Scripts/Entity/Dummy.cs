using Assets.Scripts.Action;
using Assets.Scripts.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entity
{
	public class Dummy : BaseEntity, IAttackable
	{
		public HealthComponent HealthComponent { get; set; }

		public void Dead()
		{
			
		}

		public void DecreaseHealth(float value)
		{
			
		}

		public void TakingHit(BaseEntity attacker, float damage)
		{

		}

		protected override void SetupStats()
		{
			HealthComponent.Health = 1;
			HealthComponent.MaxHealth = 1;
		}
	}
}
