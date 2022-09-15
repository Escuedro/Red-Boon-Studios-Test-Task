using Game.Model;
using Game.Services.Prefabs;
using UnityEngine;

namespace Game.View.UI
{
	public class MerchantItemsView : MonoBehaviour
	{
		[SerializeField]
		private ItemsListView _itemsListView;

		public void Init(MerchantItems merchantItems, IPrefabProvider prefabProvider, DragAndDropHandler dragAndDropHandler)
		{
			_itemsListView.Init(merchantItems.Items,
					(item, view) =>
					{
						view.Init(item, item.Cost);
						view.SetOnClick((itemView, position) => dragAndDropHandler.SetChosenItemView(false, itemView, position));
						view.SetOnRelease(dragAndDropHandler.ResetChosenItemView);
						view.SetOnDrag(dragAndDropHandler.OnDrag);
					}, prefabProvider);
		}
	}
}