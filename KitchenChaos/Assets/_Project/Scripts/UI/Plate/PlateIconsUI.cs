using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour {
    [SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private Transform iconTemplate;

	private void Awake() {
		iconTemplate.gameObject.SetActive(false);
	}
	private void Start() {
		plateKitchenObject.OnIngradientAdded += PlateKitchenObject_OnIngradientAdded;
	}

	private void PlateKitchenObject_OnIngradientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
		AddIcon(e.kitchenObjectSo);
	}

	private void AddIcon(KitchenObjectSo kitchenObjectSo) {
		Transform iconTransform = Instantiate(iconTemplate, transform);
		iconTransform.gameObject.SetActive(true);
		iconTransform.GetComponent<PlateIconUI>().SetKitchenObjectSo(kitchenObjectSo);
	}
}
