using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Services.Prefabs
{
	public class PrefabProvider : IPrefabProvider
	{
		private Dictionary<Type, Component> _prefabsByType = new Dictionary<Type, Component>();

		public void BindPrefab<T>(T prefab) where T : Component
		{
			_prefabsByType.Add(typeof(T), prefab);
		}

		public T Instantiate<T>() where T : Component
		{
			return (T)Object.Instantiate(_prefabsByType[typeof(T)]);
		}

		public T InstantiateAt<T>(Transform root) where T : Component
		{
			return (T)Object.Instantiate(_prefabsByType[typeof(T)], root);
		}
	}
}