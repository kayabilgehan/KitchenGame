using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {
	[SerializeField] private PlatesCounter platesCounter;
	[SerializeField] private Transform CounterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
	[SerializeField] private float plateOffsetY = 0.1f;

	private List<GameObject> plateVisualGameObjectList;
	

	private void Awake() {
		plateVisualGameObjectList = new List<GameObject>();
	}

	private void Start() {
		platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
		platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved; ;
	}

	private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e) {
		if (plateVisualGameObjectList.Count > 0) {
			GameObject lastSpawnedPlate = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
			plateVisualGameObjectList.Remove(lastSpawnedPlate);
			Destroy(lastSpawnedPlate);
		}
	}

	private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e) {
		Transform plateVisualTransform = Instantiate(plateVisualPrefab, CounterTopPoint);
		plateVisualTransform.localPosition = new Vector3 (0, plateOffsetY * plateVisualGameObjectList.Count, 0);
		plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
	}
}
