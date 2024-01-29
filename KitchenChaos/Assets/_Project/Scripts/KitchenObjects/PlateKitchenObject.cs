using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateKitchenObject : KitchenObject {
	
	public event EventHandler<OnIngredientAddedEventArgs> OnIngradientAdded;
	public class OnIngredientAddedEventArgs : EventArgs {
		public KitchenObjectSo kitchenObjectSo;
	}

	[SerializeField] private List<KitchenObjectSo> validKitchenObjectSoList;

    private List<KitchenObjectSo> kitchenObjectSoList;
	private void Awake() {
		kitchenObjectSoList = new List<KitchenObjectSo>();
	}
	public bool TryAddIngredient(KitchenObjectSo kitchenObjectSo) {
		if (!validKitchenObjectSoList.Contains(kitchenObjectSo)) {
			// Not a valid ingredient
			return false;
		}
		if (kitchenObjectSoList.Contains(kitchenObjectSo)) {
			// Already has this type
			return false;
		}
		else {
			kitchenObjectSoList.Add(kitchenObjectSo);
			OnIngradientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
				kitchenObjectSo = kitchenObjectSo
			});
			return true;
		}
	}
	public List<KitchenObjectSo> GetKitchenObjectSoList() {
		return kitchenObjectSoList;
	}
}
