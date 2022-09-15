using System;
using System.Collections.Generic;

namespace Core.ReactiveField
{
	public class ReactiveList<T>
	{
		private readonly List<T> _list = new List<T>();

		public Action<T> OnAdd;
		public Action<T> OnRemove;
		public Action<List<T>> OnChange;

		public void Add(T element)
		{
			_list.Add(element);
			OnAdd?.Invoke(element);
			OnChange?.Invoke(_list);
		}

		public void Remove(T element)
		{
			_list.Remove(element);
			OnRemove?.Invoke(element);
			OnChange?.Invoke(_list);
		}

		public IReadOnlyCollection<T> GetElements()
		{
			return _list;
		}
	}
}