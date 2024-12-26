using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Manager
{
	public interface IManager
	{
		void Awake();
		void Start();
		void Update();
		void FixedUpdate();
		void LateUpdate();
	}
}
