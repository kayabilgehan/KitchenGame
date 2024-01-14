using KitchenChaos.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
	public event EventHandler OnPlayerGrabbedObject;
	[SerializeField] private KitchenObjectSo kitchenObjectSo;

	public override void Interact(Player player) {
		if (!player.HasKitchenObject()) {
			// Instantiate object if player does not have anything
			Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.Prefab);
			kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
			OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
		}
	}
	
}
