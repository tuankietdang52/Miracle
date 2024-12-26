using Assets.Scripts.Components;
using Assets.Scripts.Entity.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Manager
{
	/// <summary>
	/// Manage player input
	/// </summary>
	public class PlayerManager : IManager
	{
		private Player Player => Player.Instance;

		public PlayerManager() { }

		public void Awake()
		{

		}

		public void Start()
		{
			
		}

		public void Update()
		{
			
		}

		public void FixedUpdate()
		{
			HandlePlayerMove();
		}

		public void LateUpdate()
		{
			
		}

		private void HandlePlayerMove()
		{
			PlayerMoving();

			if (Input.GetKey(KeyCode.Space))
			{
				PlayerJumping();
			}
		}

		private void PlayerMoving()
		{
			if (!Player.TryGetComponent<MovementComponent>(out var movement)) return;

			Vector3 velocity = new()
			{
				x = Input.GetAxis("Horizontal") * movement.Speed * Time.deltaTime
			};

			Player.Move(velocity);
		}

		private void PlayerJumping()
		{
			if (!Player.IsOnGround()) return;
			if (!Player.TryGetComponent<MovementComponent>(out var movement)) return;

			Vector3 velocity = new()
			{
				y = movement.JumpForce,
			};

			Player.Move(velocity);
		}
	}
}
