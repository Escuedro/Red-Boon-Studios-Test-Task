using UnityEngine;

namespace Game.Configs
{
	[CreateAssetMenu(fileName = "ConsumableItemConfig", menuName = "Data/Items/Consumable")]
	public class ConsumableConfig : ItemConfig
	{
		public int Heal;
	}
}