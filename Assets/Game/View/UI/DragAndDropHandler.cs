using Game.Commands;
using Game.Configs;
using Game.Model;
using Game.Services.Commands;
using Game.Services.Prefabs;
using UnityEngine;

namespace Game.View.UI
{
	public class DragAndDropHandler : MonoBehaviour
	{
		[SerializeField]
		private OnDropEventListener _buyArea;
		[SerializeField]
		private OnDropEventListener _sellArea;
		[SerializeField]
		private Canvas _canvas;

		private ItemView _draggingItemView;
		private ItemView _currentChosenItemView;

		private IPrefabProvider _prefabProvider;
		private ICommandExecutor _commandExecutor;

		private PlayerWallet _playerWallet;
		private MerchantConfig _merchantConfig;

		public void Init(IPrefabProvider prefabProvider,
				ICommandExecutor commandExecutor,
				PlayerWallet playerWallet,
				MerchantConfig merchantConfig)
		{
			_prefabProvider = prefabProvider;
			_commandExecutor = commandExecutor;
			_playerWallet = playerWallet;
			_merchantConfig = merchantConfig;
		}

		private void Start()
		{
			_draggingItemView = _prefabProvider.InstantiateAt<ItemView>(_canvas.transform);
			_draggingItemView.gameObject.SetActive(false);
			_draggingItemView.CanvasGroup.blocksRaycasts = false;

			_buyArea.SetOnDrop(OnItemBuy);
			_sellArea.SetOnDrop(OnItemSell);
		}

		private void OnItemBuy()
		{
			Item itemToBuy = _currentChosenItemView.Item;
			ResetChosenItemView(_currentChosenItemView);
			_commandExecutor.Execute(new BuyItemCommand(itemToBuy));
		}

		private void OnItemSell()
		{
			Item itemToSell = _currentChosenItemView.Item;
			ResetChosenItemView(_currentChosenItemView);
			_commandExecutor.Execute(new SellItemCommand(itemToSell));
		}

		public void SetChosenItemView(bool playerItem, ItemView itemView, Vector2 position)
		{
			_buyArea.gameObject.SetActive(!playerItem);
			_sellArea.gameObject.SetActive(playerItem);
			_currentChosenItemView = itemView;
			if (!playerItem)
			{
				ValidateItemCost();
			}
			itemView.CanvasGroup.alpha = 0.5f;
			OnDrag(position);
			Item item = itemView.Item;
			_draggingItemView.Init(item,
					playerItem ? Mathf.RoundToInt(item.Cost * _merchantConfig.SellMultiplier) :
							item.Cost);
			_draggingItemView.gameObject.SetActive(true);
		}

		private void ValidateItemCost()
		{
			int itemCost = Mathf.RoundToInt(_currentChosenItemView.Item.Cost);
			_buyArea.SetState(itemCost <= _playerWallet.Coins.Value);
		}

		public void ResetChosenItemView(ItemView itemView)
		{
			_buyArea.gameObject.SetActive(false);
			_sellArea.gameObject.SetActive(false);
			_currentChosenItemView = null;
			itemView.CanvasGroup.alpha = 1.0f;
			_draggingItemView.gameObject.SetActive(false);
		}

		public void OnDrag(Vector2 cursorPosition)
		{
			_draggingItemView.transform.position = cursorPosition;
		}
	}
}