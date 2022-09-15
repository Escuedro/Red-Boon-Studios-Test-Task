using UnityEngine;

namespace Game.Model
{
	public class ConsumableItem : Item
	{
		public enum ConsumableType
		{
			None = 0,
			Bottle = 1
		}

		public readonly int Heal;

		public ConsumableItem(int cost, string name, Sprite icon, int heal) : base(cost, name, icon)
		{
			Heal = heal;
		}
	}
}