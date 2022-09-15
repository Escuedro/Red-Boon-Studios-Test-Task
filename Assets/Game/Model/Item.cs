using Game.Configs;
using UnityEngine;

namespace Game.Model
{
	public abstract class Item
	{
		public readonly int Cost;
		public readonly string Name;
		public readonly Sprite Icon;

		protected Item(int cost, string name, Sprite icon)
		{
			Cost = cost;
			Name = name;
			Icon = icon;
		}
	}
}