using KitchenChaos.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSo kitchenObjectSo;

	public override void Interact(Player player) {
        if (!HasKitchenObject()) { 
			// There is no kitchen object here
			if (player.HasKitchenObject()) {
				// Player carries something
				player.GetKitchenObject().SetKitchenObjectParent(this);
			}
			else {
				// Player does not carrying anything
			}
		}
		else {
			// There is a kitchen object here
			if (player.HasKitchenObject()) {
				// Player carries something
			}
			else {
				// Player does not carrying anything
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}
}
