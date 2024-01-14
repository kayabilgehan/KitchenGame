using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace KitchenChaos.Player {

	[CreateAssetMenu(fileName = "CounterSettingsSo", menuName = "Settings/CounterSettings", order = 1)]
	public class CounterSettingsSo : ScriptableObject {
		[SerializeField] private LayerMask countersLayermask;

		public LayerMask CountersLayermask => countersLayermask;
	}
}

