using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
	[Serializable]
	public struct KitchenObjectSo_GameObject {
		public KitchenObjectSo kitchenObjectSo;
		public GameObject kitchenObjectGameObject;
	}

    [SerializeField] private PlateKitchenObject plateKitchenObject;
	[SerializeField] private List<KitchenObjectSo_GameObject> kitchenObjectSoGameObjectList;

	private void Start() {
		plateKitchenObject.OnIngradientAdded += PlateKitchenObject_OnIngradientAdded;

		foreach (KitchenObjectSo_GameObject kitchenObjectSoGameObject in kitchenObjectSoGameObjectList) {
			kitchenObjectSoGameObject.kitchenObjectGameObject.SetActive(false);
		}
	}

	private void PlateKitchenObject_OnIngradientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
		KitchenObjectSo_GameObject kitchenObjectSoGameObject = kitchenObjectSoGameObjectList.Where(ko => ko.kitchenObjectSo.Equals(e.kitchenObjectSo)).FirstOrDefault();
		kitchenObjectSoGameObject.kitchenObjectGameObject.SetActive(true);
	}
}
