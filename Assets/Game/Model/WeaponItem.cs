using Game.Configs;
using UnityEngine;

namespace Game.Model
{
	public class WeaponItem : Item
	{
		public enum WeaponType
		{
			None = 0,
			Sword = 1,
			Axe = 2
		}

		public readonly int Damage;


		public WeaponItem(int cost, string name, Sprite icon, int damage) : base(cost, name, icon)
		{
			Damage = damage;
		}
	}
}