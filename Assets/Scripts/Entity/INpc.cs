using Assets.Scripts.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Entity
{
	public interface INpc
	{
		public EnemyAI AI { get; set; }
	}
}
