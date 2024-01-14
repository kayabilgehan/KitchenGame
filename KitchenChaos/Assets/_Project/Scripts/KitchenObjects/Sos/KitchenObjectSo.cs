using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class KitchenObjectSo : ScriptableObject {
    [SerializeField] private Transform prefab;
	[SerializeField] private Sprite sprite;
	[SerializeField] private string objectName;

	public Transform Prefab => prefab;
	public Sprite Sprite => sprite;
	public string ObjectName => objectName;
}
