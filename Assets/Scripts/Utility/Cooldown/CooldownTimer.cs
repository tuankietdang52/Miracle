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

		/// <summary>
		/// Start a coroutine and store it with key
		/// </summary>
		/// <param name="key"></param>
		/// <param name="callback"></param>
		public void Start(string key, IEnumerator callback)
		{
			coroutines[key] = _owner.StartCoroutine(callback);
		}

		/// <summary>
		/// Stop a coroutine by key and remove it
		/// </summary>
		/// <param name="key"></param>
		public void Stop(string key)
		{
			try
			{
				_owner.StopCoroutine(coroutines[key]);
				coroutines.Remove(key);
			}
			catch (KeyNotFoundException) { }
		}

		/// <summary>
		/// Remove a coroutine
		/// </summary>
		/// <param name="key"></param>
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
