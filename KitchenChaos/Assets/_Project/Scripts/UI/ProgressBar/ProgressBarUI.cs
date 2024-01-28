using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
	[SerializeField] private GameObject hasProgressGameObject;
	[SerializeField] private Image barImage;

	private IHasProgress hasProgress;

	private void Start() {
		hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
		if (hasProgress == null ) {
			Debug.Log("GameObject " + hasProgressGameObject + " does not have IHasProgress!");
		}
		hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
		barImage.fillAmount = 0;
		Hide();
	}

	private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
		if (e.progressNormalized == 0f || e.progressNormalized == 1f) {
			Hide();
		}
		else {
			Show();
			barImage.fillAmount = e.progressNormalized;
		}
	}

	private void Show() { 
		gameObject.SetActive(true);
	}
	private void Hide() {
		gameObject.SetActive(false);
	}
}