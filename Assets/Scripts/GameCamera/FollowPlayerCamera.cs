using Assets.Scripts.Log;
using Assets.Scripts.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.GameCamera
{
	[RequireComponent(typeof(CinemachineCamera))]
	public class FollowPlayerCamera : MonoBehaviour, ICameraMachine
	{
		private CinemachineCamera cinemachineCamera;
		public float DefaultSize = 2f;

		private void Awake()
		{
			cinemachineCamera = GetComponent<CinemachineCamera>();
			cinemachineCamera.Lens.OrthographicSize = DefaultSize;
		}

		private void Start()
		{
			GameManager.Instance.Camera = this;
		}

		public void SetSize(float size)
		{
			cinemachineCamera.Lens.OrthographicSize = DefaultSize;
		}
	}
}
