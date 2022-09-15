using UnityEngine;

namespace Game.Configs
{
	public abstract class ItemConfig : ScriptableObject
	{
		public string Name;
		public Sprite Icon;
		public int Cost;
	}
}