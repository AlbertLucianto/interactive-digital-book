using UIWidgets;
using System.Collections.Generic;

namespace URECA {

	public class InteractionListView : ListViewCustom<InteractionListViewItem,KeyValuePair<string,string>> {
		protected override void SetData(InteractionListViewItem component, KeyValuePair<string,string> item)
		{
			component.SetData(item);
		}
	}
}
