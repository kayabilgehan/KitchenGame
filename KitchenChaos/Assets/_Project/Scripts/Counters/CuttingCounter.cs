using KitchenChaos.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress {
	public event EventHandler OnCut;
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

	[SerializeField] private CuttingRecipeSo[] cuttingKitchenObjectSoArray;

	private int cuttingProgress = 0;

	public override void Interact(Player player) {
		if (!HasKitchenObject()) {
			// There is no kitchen object here
			if (player.HasKitchenObject()) {
				// Player carries something
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo())) {
					player.GetKitchenObject().SetKitchenObjectParent(this);
					cuttingProgress = 0;
					CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() { 
						progressNormalized = (float)cuttingProgress / (float)cuttingRecipeSo.cuttingProgressMax
					});
				}
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
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
					progressNormalized = 0f
				});
			}
		}
	}

	public override void InteractAlternate(Player player) {
		if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSo())) {
			cuttingProgress++;
			OnCut?.Invoke(this, EventArgs.Empty);
			
			CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());

			OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
				progressNormalized = (float)cuttingProgress / (float)cuttingRecipeSo.cuttingProgressMax
			});

			if (cuttingProgress >= cuttingRecipeSo.cuttingProgressMax) {
				KitchenObjectSo cutKitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSo());
				GetKitchenObject().DestroySelf();
				KitchenObject.SpawnKitchenObject(cutKitchenObjectSo, this);
			}
		}
	}
	private CuttingRecipeSo GetCuttingRecipeSoWithInput(KitchenObjectSo inputKitchenObjectSo) {
		CuttingRecipeSo cuttingRecipeSo = cuttingKitchenObjectSoArray.ToList().Where(e => e.input.Equals(inputKitchenObjectSo)).FirstOrDefault();
		if (cuttingRecipeSo != null) {
			return cuttingRecipeSo;
		}
		else {
			return null;
		}
	}
	private bool HasRecipeWithInput(KitchenObjectSo inputKitchenObjectSo) {
		CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeSoWithInput(inputKitchenObjectSo);
		return cuttingRecipeSo != null;
	}
	private KitchenObjectSo GetOutputForInput(KitchenObjectSo inputKitchenObjectSo) {
		CuttingRecipeSo cuttingRecipeSo = GetCuttingRecipeSoWithInput(inputKitchenObjectSo);
		if (cuttingRecipeSo != null) {
			return cuttingRecipeSo.output;
		}
		else {
			return null;
		}
	}
}
