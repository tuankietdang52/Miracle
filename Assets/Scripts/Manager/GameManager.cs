using Assets.Scripts.GameCamera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Manager
{
	/// <summary>
	/// Base manager class which call unity event in all manager (without MonoBehaviour)
	/// </summary>
	public class GameManager : MonoBehaviour
	{
		private readonly IEnumerable<IManager> managers = new List<IManager>()
		{
			new PlayerManager(),
		};

		public static GameManager Instance;
		public ICameraMachine Camera { get; set; }

		private void Awake()
		{
			Instance = Instance == null ? this : Instance;
			foreach (var manager in managers)
			{
				manager.Awake();
			}
		}

		private void Start()
		{
			foreach (var manager in managers)
			{
				manager.Start();
			}
		}

		private void Update()
		{
			foreach (var manager in managers)
			{
				manager.Update();
			}
		}

		private void FixedUpdate()
		{
			foreach (var manager in managers)
			{
				manager.FixedUpdate();
			}
		}

		private void LateUpdate()
		{
			foreach (var manager in managers)
			{
				manager.LateUpdate();
			}
		}
	}
}
