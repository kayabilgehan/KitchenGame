using KitchenChaos.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress {

	public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
	public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

	public class OnStateChangedEventArgs : EventArgs {
		public State state;
	}

	public enum State {
		Idle,
		Frying,
		Fried,
		Burned
	}

	[SerializeField] private FryingRecipeSo[] fryingRecipeSoArray;
	[SerializeField] private BurningRecipeSo[] burningRecipeSoArray;

	private State state;
	private float fryingTimer;
	private float burningTimer;
	private FryingRecipeSo fryingRecipeSo;
	private BurningRecipeSo burningRecipeSo;

	private void Start() {
		state = State.Idle;
		
	}
	private void Update() {
		if (HasKitchenObject()) {
			switch (state) {
			case State.Idle:
				break;
			case State.Frying:
					fryingTimer += Time.deltaTime;

					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
						progressNormalized = fryingTimer / fryingRecipeSo.fryingTimerMax
					});

					if (fryingTimer >= fryingRecipeSo.fryingTimerMax) {
						// Fried
						GetKitchenObject().DestroySelf();
						KitchenObject.SpawnKitchenObject(fryingRecipeSo.output, this);
						state = State.Fried;
						burningTimer = 0;
						burningRecipeSo = GetBurningRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
					}
				break;
			case State.Fried:
					burningTimer += Time.deltaTime;

					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
						progressNormalized = burningTimer / burningRecipeSo.burningTimerMax
					});

					if (burningTimer >= burningRecipeSo.burningTimerMax) {
						// Fried
						GetKitchenObject().DestroySelf();
						KitchenObject.SpawnKitchenObject(burningRecipeSo.output, this);
						state = State.Burned;

						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
							progressNormalized = 0f
						});
					}
					break;
			case State.Burned:
				break;
			default:
				break;
			}
		}
	}

	public override void Interact(Player player) {
		
		if (!HasKitchenObject()) {
			// There is no kitchen object here
			if (player.HasKitchenObject()) {
				// Player carries something
				if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo())) {
					player.GetKitchenObject().SetKitchenObjectParent(this);
					fryingRecipeSo = GetFryingRecipeSoWithInput(GetKitchenObject().GetKitchenObjectSo());
					state = State.Frying;
					fryingTimer = 0f;

					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });

					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
						progressNormalized = fryingTimer / fryingRecipeSo.fryingTimerMax
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
				state = State.Idle;
				OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
					progressNormalized = 0f
				});
			}
		}
	}



	private FryingRecipeSo GetFryingRecipeSoWithInput(KitchenObjectSo inputKitchenObjectSo) {
		FryingRecipeSo fryingRecipeSo = fryingRecipeSoArray.ToList().Where(e => e.input.Equals(inputKitchenObjectSo)).FirstOrDefault();
		if (fryingRecipeSo != null) {
			return fryingRecipeSo;
		}
		else {
			return null;
		}
	}
	private bool HasRecipeWithInput(KitchenObjectSo inputKitchenObjectSo) {
		FryingRecipeSo fryingRecipeSo = GetFryingRecipeSoWithInput(inputKitchenObjectSo);
		return fryingRecipeSo != null;
	}
	private KitchenObjectSo GetOutputForInput(KitchenObjectSo inputKitchenObjectSo) {
		FryingRecipeSo fryingRecipeSo = GetFryingRecipeSoWithInput(inputKitchenObjectSo);
		if (fryingRecipeSo != null) {
			return fryingRecipeSo.output;
		}
		else {
			return null;
		}
	}

	private BurningRecipeSo GetBurningRecipeSoWithInput(KitchenObjectSo inputKitchenObjectSo) {
		BurningRecipeSo burningRecipeSo = burningRecipeSoArray.ToList().Where(e => e.input.Equals(inputKitchenObjectSo)).FirstOrDefault();
		if (burningRecipeSo != null) {
			return burningRecipeSo;
		}
		else {
			return null;
		}
	}
}
