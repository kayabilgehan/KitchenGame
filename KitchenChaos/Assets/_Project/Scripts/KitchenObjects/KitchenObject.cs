using KitchenChaos.Player;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if (this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else { 
            plateKitchenObject = null;
            return false; 
        }
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSo kitchenObjectSo, IKitchenObjectParent kitchenObjectParent) {
		Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.Prefab);
		KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
	}
}
