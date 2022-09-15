using Game.Configs;
using Game.Model;
using Game.Services.Factories;
using UnityEngine;

namespace Game.Services
{
	public class ItemConfigBinder : MonoBehaviour
	{
		[SerializeField]
		private WeaponConfig _swordConfig;
		[SerializeField]
		private WeaponConfig _axeConfig;
		[SerializeField]
		private ConsumableConfig _bottleConfig;

		public void BindConfigs(IMainItemFactory mainItemFactory)
		{
			mainItemFactory.BindConfig<WeaponItem, WeaponConfig>(_swordConfig, WeaponItem.WeaponType.Sword);
			mainItemFactory.BindConfig<WeaponItem, WeaponConfig>(_axeConfig, WeaponItem.WeaponType.Axe);

			mainItemFactory.BindConfig<ConsumableItem, ConsumableConfig>(_bottleConfig,
					ConsumableItem.ConsumableType.Bottle);
		}
	}
}