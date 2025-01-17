using Assets.Scripts.Components;
using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Action
{
	public interface IAttackable
	{
		public HealthComponent HealthComponent { get; set; }

		public void TakingHit(BaseEntity attacker, float damage);

		public void DecreaseHealth(float value);

		public void Dead();
	}
}
