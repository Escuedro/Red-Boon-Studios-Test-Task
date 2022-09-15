using System;
using System.Collections.Generic;

namespace Game.Services
{
	public class ModelProvider : IModelProvider
	{
		private readonly Dictionary<Type, object> _modelsByType = new Dictionary<Type, object>();

		public T Get<T>()
		{
			return (T)_modelsByType[typeof(T)];
		}

		public void BindModel<T>(T model)
		{
			_modelsByType.Add(typeof(T), model);
		}
	}
}