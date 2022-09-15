using UnityEngine;

namespace Game.Configs
{
	[CreateAssetMenu(fileName = "WeaponItemConfig", menuName = "Data/Items/Weapon")]
	public class WeaponConfig : ItemConfig
	{
		public int Damage;
	}
}