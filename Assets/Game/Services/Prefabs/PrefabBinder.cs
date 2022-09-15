using Game.View.UI;
using UnityEngine;

namespace Game.Services.Prefabs
{
	public class PrefabBinder : MonoBehaviour
	{
		[SerializeField]
		private ItemView _itemViewPrefab;

		public void BindPrefabs(IPrefabProvider prefabProvider)
		{
			prefabProvider.BindPrefab(_itemViewPrefab);
		}
	}
}