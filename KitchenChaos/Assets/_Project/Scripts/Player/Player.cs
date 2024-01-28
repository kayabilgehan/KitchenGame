using System;
using UnityEngine;

namespace KitchenChaos.Player {
	public class Player : MonoBehaviour, IKitchenObjectParent {
		public static Player Instance { get; private set; }

		public class OnSelectedCounterChangedEventArgs : EventArgs {
			public BaseCounter selectedCounter;
		}
		public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

		[SerializeField] private PlayerSettingsSo playerSettings;
		[SerializeField] private CounterSettingsSo counterSettings;
		[SerializeField] private GameInput gameInput;
		[SerializeField] private Transform kitchenObjectHoldPoint;

		private bool isWalking = false;
		private float moveDistance = 0;
		private bool canMove = false;
		private BaseCounter selectedCounter;
		private KitchenObject kitchenObject;
		Vector2 inputVector;
		Vector3 moveDir;
		Vector3 moveDirX;
		Vector3 moveDirZ;
		Vector3 lastInteractDir;
		private void Awake() {
			if (Instance != null) {
				Debug.LogError("More than one player.");
			}
			Instance = this;
		}
		private void Start() {
			gameInput.OnInteractAction += GameInput_OnInteractAction;
			gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
		}

		private void GameInput_OnInteractAlternateAction(object sender, EventArgs e) {
			if (selectedCounter != null) {
				selectedCounter.InteractAlternate(this);
			}
		}

		private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
			if (selectedCounter != null) {
				selectedCounter.Interact(this);
			}
		}

		private void Update() {
			UpdateMoveDir();
			HandleMovement();
			HandleInteractions();
		}
		public bool IsWalking() {
			return isWalking;
		}
		private void UpdateMoveDir() {
			inputVector = gameInput.GetMovementVectorNormalized();
			moveDir = new Vector3(inputVector.x, 0, inputVector.y);
			if (moveDir != Vector3.zero) {
				lastInteractDir = moveDir;
			}
		}
		private void HandleInteractions() {
			RaycastHit hit;
			if (Physics.Raycast(transform.position, lastInteractDir, out hit, playerSettings.InteractDistance, counterSettings.CountersLayermask)) {
				if(hit.transform.TryGetComponent(out BaseCounter baseCounter)) {
					if (baseCounter != selectedCounter) {
						SetSelectedCounter(baseCounter);
					}
				}
				else {
					SetSelectedCounter(null);
				}
			}
			else {
				SetSelectedCounter(null);
			}
		}
		private void SetSelectedCounter(BaseCounter selectedCounter) {
			this.selectedCounter = selectedCounter;
			OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
				selectedCounter = selectedCounter
			});
		}
		private void HandleMovement() {
			moveDistance = Time.deltaTime * playerSettings.MoveSpeed;

			canMove = !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * playerSettings.PlayerHeight), playerSettings.PlayerRadius, moveDir, moveDistance);

			if (!canMove) {
				// Attemt only x movement
				moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
				canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * playerSettings.PlayerHeight), playerSettings.PlayerRadius, moveDirX, moveDistance);
				if (canMove) {
					moveDir = moveDirX;
				}
				else {
					// Attemt only z movement
					moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
					canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + (Vector3.up * playerSettings.PlayerHeight), playerSettings.PlayerRadius, moveDirZ, moveDistance);
					if (canMove) {
						moveDir = moveDirZ;
					}
				}
			}

			if (canMove) {
				transform.position += (Vector3)moveDir * moveDistance;
			}

			isWalking = moveDir != Vector3.zero;
			if (moveDir != Vector3.zero) {
				transform.forward = Vector3.Slerp(transform.forward, moveDir, playerSettings.RotateSpeed * Time.deltaTime);
			}
		}

		public Transform GetKitchenObjectFollowTransform() {
			return kitchenObjectHoldPoint;
		}
		public void SetKitchenObject(KitchenObject kitchenObject) {
			this.kitchenObject = kitchenObject;
		}
		public KitchenObject GetKitchenObject() {
			return kitchenObject;
		}
		public void ClearKitchenObject() {
			kitchenObject = null;
		}
		public bool HasKitchenObject() {
			return kitchenObject != null;
		}
	}
}
