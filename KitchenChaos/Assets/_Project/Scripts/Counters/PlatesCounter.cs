using KitchenChaos.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

	[SerializeField] private KitchenObjectSo plateKitchenObjectSo;

	private float spawnPlateTimer;
	private float spawnPlateTimerMax = 4f;
	private int platesSpawnedAmount;
	private int platesSpawnedAmountMax = 4;

	public override void Interact(Player player) {

	}
	private void Update() {
		spawnPlateTimer += Time.deltaTime;
		if (spawnPlateTimer > spawnPlateTimerMax) {
			spawnPlateTimer = 0f;

			if (spawnPlateTimer < platesSpawnedAmountMax) {
				platesSpawnedAmount++;
			}
		}
	}
}
