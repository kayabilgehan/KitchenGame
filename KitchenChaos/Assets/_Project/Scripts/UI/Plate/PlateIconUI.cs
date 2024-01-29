using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour {
    [SerializeField] private Image icon;
    public void SetKitchenObjectSo(KitchenObjectSo kitchenObjectSo) {
        icon.sprite = kitchenObjectSo.Sprite;
    }
}
