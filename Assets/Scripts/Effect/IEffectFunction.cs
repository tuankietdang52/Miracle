using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Effect
{
	public interface IEffectFunction
	{
		public void Activate(BaseEntity owner);
	}
}
