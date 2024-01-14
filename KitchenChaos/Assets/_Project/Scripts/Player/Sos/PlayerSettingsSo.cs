using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace KitchenChaos.Player {

	[CreateAssetMenu(fileName = "PlayerSettingsSo", menuName = "Settings/PlayerSettings", order = 1)]
	public class PlayerSettingsSo : ScriptableObject {
		[SerializeField] private float moveSpeed = 5f;
		[SerializeField] private float rotateSpeed = 10f;
		[SerializeField] private float playerRadius = 0.7f;
		[SerializeField] private float playerHeight = 2f;
		[SerializeField] private float interactDistance = 2f;

		public float MoveSpeed => moveSpeed;
		public float RotateSpeed => rotateSpeed;
		public float PlayerRadius => playerRadius;
		public float PlayerHeight => playerHeight;
		public float InteractDistance => interactDistance;
	}
}

