using Game.Configs;
using Game.Model;
using Game.Services.Prefabs;
using UnityEngine;

namespace Game.View.UI
{
	public class PlayerItemsView : MonoBehaviour
	{
		[SerializeField]
		private ItemsListView _itemsListView;

		public void Init(PlayerItems playerItems, IPrefabProvider prefabProvider, DragAndDropHandler dragAndDropHandler, MerchantConfig merchantConfig)
		{
			_itemsListView.Init(playerItems.Items,
					(item, view) =>
					{
						view.Init(item, Mathf.RoundToInt(item.Cost * merchantConfig.SellMultiplier));
						view.SetOnClick((itemView, position) => dragAndDropHandler.SetChosenItemView(true, itemView, position));
						view.SetOnRelease(dragAndDropHandler.ResetChosenItemView);
						view.SetOnDrag(dragAndDropHandler.OnDrag);
					}, prefabProvider);
		}
	}
}