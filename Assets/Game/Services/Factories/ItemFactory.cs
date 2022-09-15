using System;
using Game.Configs;
using Game.Model;

namespace Game.Services.Factories
{
	public class ItemFactory : IMainItemFactory
	{
		private WeaponFactory _weaponFactory = new WeaponFactory();
		private ConsumableFactory _consumableFactory = new ConsumableFactory();

		public T Create<T>(object id = null)
				where T : Item
		{
			Type itemType = typeof(T);
			if (itemType == typeof(WeaponItem))
			{
				return _weaponFactory.Create(id) as T;
			}
			if (itemType == typeof(ConsumableItem))
			{
				return _consumableFactory.Create(id) as T;
			}
			throw new Exception($"Not found factory for type {itemType}");
		}

		public void BindConfig<T, TConfig>(ItemConfig itemConfig, object id = null) where T : Item where TConfig : ItemConfig
		{
			Type itemType = typeof(T);
			if (itemType == typeof(WeaponItem))
			{
				_weaponFactory.BindConfig(itemConfig as WeaponConfig, id);
			}
			else if (itemType == typeof(ConsumableItem))
			{
				_consumableFactory.BindConfig(itemConfig as ConsumableConfig, id);
			}
		}
	}
}