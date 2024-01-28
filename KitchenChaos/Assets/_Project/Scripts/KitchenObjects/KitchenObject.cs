using KitchenChaos.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSo kitchenObjectSo;

    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSo GetKitchenObjectSo() { 
        return kitchenObjectSo;
    }
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) { 
        if (this.kitchenObjectParent != null) { this.kitchenObjectParent.ClearKitchenObject(); }
        this.kitchenObjectParent = kitchenObjectParent;
        if (this.kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("KitchenObjectParent already has a KitchenObject!");
        }
		this.kitchenObjectParent.SetKitchenObject(this);
        transform.parent = this.kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
	public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
	}
	public void DestroySelf() {
		kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
	}

    public static KitchenObject SpawnKitchenObject(KitchenObjectSo kitchenObjectSo, IKitchenObjectParent kitchenObjectParent) {
		Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.Prefab);
		KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
	}
}
