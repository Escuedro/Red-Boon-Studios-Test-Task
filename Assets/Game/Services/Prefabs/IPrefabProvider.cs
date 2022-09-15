using UnityEngine;

namespace Game.Services.Prefabs
{
	public interface IPrefabProvider
	{
		public void BindPrefab<T>(T prefab) where T : Component;

		public T Instantiate<T>() where T : Component;

		public T InstantiateAt<T>(Transform root) where T : Component;
	}
}