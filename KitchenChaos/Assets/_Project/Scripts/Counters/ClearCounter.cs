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
				if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
					// Player is holding a plate
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo())) {
						GetKitchenObject().DestroySelf();
					}
				}
				else {
					// Player is not carrying a plate but something else
					if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
						// Counter is holding a plate
						if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSo())) {
							player.GetKitchenObject().DestroySelf();
						}
					}
				}
			}
			else {
				// Player does not carrying anything
				GetKitchenObject().SetKitchenObjectParent(player);
			}
		}
	}
}
