using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components
{
	public class AttackComponent : MonoBehaviour
	{
		public float[] BaseDamage;

		public Vector2[] AttackRange;

		public Vector2[] AttackHolderPositionModify;

		public int ComboCount => BaseDamage.Length;
	}
}
