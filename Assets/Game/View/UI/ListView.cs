using System;
using System.Collections.Generic;
using Core.ReactiveField;
using Game.Services;
using Game.Services.Prefabs;
using Game.Utils.Extensions;
using UnityEngine;

namespace Game.View.UI
{
	public class ListView<TModel, TView> : MonoBehaviour where TView : Component
	{
		[SerializeField]
		private Transform _root;

		private Dictionary<TModel, TView> _viewByModel = new Dictionary<TModel, TView>();

		private ReactiveList<TModel> _reactiveList;
		private Action<TModel, TView> _onInit;
		private IPrefabProvider _prefabProvider;

		public void Init(ReactiveList<TModel> list,
				Action<TModel, TView> onInit,
				IPrefabProvider prefabProvider)
		{
			_reactiveList = list;
			_prefabProvider = prefabProvider;
			_onInit = onInit;

			UpdateList(list.GetElements());

			list.OnAdd += OnAdd;
			list.OnRemove += OnRemove;
		}

		private void UpdateList(IReadOnlyCollection<TModel> elements)
		{
			_root.DestroyChildren();

			foreach (TModel model in elements)
			{
				OnAdd(model);
			}
		}

		private void OnAdd(TModel model)
		{
			TView view = _prefabProvider.InstantiateAt<TView>(_root);
			_onInit?.Invoke(model, view);
			_viewByModel.Add(model, view);
		}

		private void OnRemove(TModel model)
		{
			TView view = _viewByModel[model];
			Destroy(view.gameObject);
			_viewByModel.Remove(model);
		}

		private void OnDestroy()
		{
			_reactiveList.OnAdd -= OnAdd;
			_reactiveList.OnRemove -= OnRemove;
		}
	}
}