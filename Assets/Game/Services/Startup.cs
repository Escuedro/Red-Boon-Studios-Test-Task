using Game.Commands;
using Game.Configs;
using Game.Model;
using Game.Services.Commands;
using Game.Services.Factories;
using Game.Services.Prefabs;
using Game.View.UI;
using UnityEngine;

namespace Game.Services
{
	public class Startup : MonoBehaviour
	{
		[Header("Views")]
		[SerializeField]
		private PlayerItemsView _playerItemsView;
		[SerializeField]
		private MerchantItemsView _merchantItemsView;
		[SerializeField]
		private PlayerWalletView _playerWalletView;
		[SerializeField]
		private DragAndDropHandler _dragAndDropHandler;
		[Header("SubServices")]
		[SerializeField]
		private PrefabBinder _prefabBinder;
		[SerializeField]
		private ItemConfigBinder _itemConfigBinder;
		[SerializeField]
		private GeneralConfigsBinder _generalConfigsBinder;

		private IModelProvider _modelProvider;
		private ICommandExecutor _commandExecutor;
		private IPrefabProvider _prefabProvider;
		private IMainItemFactory _mainItemFactory;

		private void Awake()
		{
			ConfigureServices();
			BindModels();
			InitializeViews();

			AddItems();
		}

		private void ConfigureServices()
		{
			_modelProvider = new ModelProvider();
			_commandExecutor = new Commands.CommandExecutor(_modelProvider);
			_prefabProvider = new PrefabProvider();
			_mainItemFactory = new ItemFactory();

			_prefabBinder.BindPrefabs(_prefabProvider);
			_itemConfigBinder.BindConfigs(_mainItemFactory);
			_generalConfigsBinder.BindGeneralConfigs(_modelProvider);
		}

		private void BindModels()
		{
			_modelProvider.BindModel(new PlayerItems());
			_modelProvider.BindModel(new PlayerWallet());
			_modelProvider.BindModel(new MerchantItems());
		}

		private void InitializeViews()
		{
			_dragAndDropHandler.Init(_prefabProvider, _commandExecutor, _modelProvider.Get<PlayerWallet>(), _modelProvider.Get<MerchantConfig>());
			_playerItemsView.Init(_modelProvider.Get<PlayerItems>(), _prefabProvider, _dragAndDropHandler, _modelProvider.Get<MerchantConfig>());
			_merchantItemsView.Init(_modelProvider.Get<MerchantItems>(), _prefabProvider, _dragAndDropHandler);
			_playerWalletView.Init(_modelProvider.Get<PlayerWallet>());
		}

		private void AddItems()
		{
			_commandExecutor.Execute(
					new AddPlayerItemCommand(_mainItemFactory.Create<WeaponItem>(WeaponItem.WeaponType.Sword)));
			_commandExecutor.Execute(
					new AddPlayerItemCommand(
							_mainItemFactory.Create<ConsumableItem>(ConsumableItem.ConsumableType.Bottle)));
			_commandExecutor.Execute(
					new AddMerchantItemCommand(_mainItemFactory.Create<WeaponItem>(WeaponItem.WeaponType.Axe)));
		}
	}
}