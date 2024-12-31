using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utility.Cooldown
{
	public class CooldownTimer
	{
		private readonly MonoBehaviour _owner;
		private readonly Dictionary<string, Coroutine> coroutines = new();

		public CooldownTimer(MonoBehaviour owner)
		{
			_owner = owner;
		}

		public void Start(string key, Func<IEnumerator> callback)
		{
			coroutines[key] = _owner.StartCoroutine(callback());
		}

		public void Stop(string key)
		{
			try
			{
				_owner.StopCoroutine(coroutines[key]);
				coroutines.Remove(key);
			}
			catch (KeyNotFoundException) { }
		}

		public void ReleaseCoroutine(string key)
		{
			try
			{
				coroutines.Remove(key);
			}
			catch (KeyNotFoundException) { }
		}
	}
}
