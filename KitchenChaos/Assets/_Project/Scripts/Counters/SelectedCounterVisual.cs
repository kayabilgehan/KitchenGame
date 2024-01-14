using KitchenChaos.Player;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour {
	[SerializeField] private BaseCounter counter;
	[SerializeField] private List<GameObject> selectedVisuals;
	private void Start() {
		Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
	}

	private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
		if (e.selectedCounter == counter) {
			Show();
		}
		else {
			Hide();
		}
	}
	private void Show() {
		ChangeSelected(true);
	}
	private void Hide() {
		ChangeSelected(false);
	}
	private void ChangeSelected(bool state) {
		foreach (GameObject selectedVisual in selectedVisuals) {
			selectedVisual.SetActive(state);
		}
	}
}
