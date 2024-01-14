using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KitchenChaos.Player {
	public class PlayerAnimator : MonoBehaviour {
		private const string IS_WALKING = "IsWalking";

		[SerializeField] private Animator animator;
		[SerializeField] private Player player;

		private void Update() {
			animator.SetBool(IS_WALKING, player.IsWalking());
		}
	}
}