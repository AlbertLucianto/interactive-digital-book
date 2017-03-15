using UnityEngine;
using System.Collections.Generic;
using URECA;
using UIWidgets;

namespace URECA {
	[RequireComponent(typeof(InteractionListView))]
	public class InteractionTypeForCombo : MonoBehaviour {
		void Start()
		{
			var items = GetComponent<InteractionListView>().DataSource;

			items.BeginUpdate();

			items.Add(new KeyValuePair<string, string>("Interaction_ObjectRotatorBySound", "BLOW"));
			items.Add(new KeyValuePair<string, string>("Interation_ZoomByFigure", "ZOOM"));

			items.EndUpdate();
		}
	}
}