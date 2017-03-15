using System;
using UIWidgets;
using URECA;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace URECA
{
	
	public class InteractionComboBox : ComboboxCustom<InteractionListView,InteractionListViewItem,KeyValuePair<string,string>>
	{
		public string currentValue ="";
		protected override void SetData(InteractionListViewItem component, KeyValuePair<string,string> item)
		{
			component.SetData(item);
			currentValue = item.Key;
		}
	}

}
