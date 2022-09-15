using System.Collections.Generic;
using Game.Configs;
using Game.Model;

namespace Game.Services.Factories
{
	public abstract class ItemFactoryBase<TItem, TConfig> where TItem : Item where TConfig : ItemConfig
	{
		private TConfig _defaultConfig;
		private Dictionary<object, TConfig> _configById = new Dictionary<object, TConfig>();
		public TItem Create(object id = null)
		{
			TConfig config = id == null ? _defaultConfig : _configById[id];
			return Create(config);
		}

		protected abstract TItem Create(TConfig config);

		public void BindConfig(TConfig config, object id = null)
		{
			if (id == null)
			{
				_defaultConfig = config;
			}
			else
			{
				_configById.Add(id, config);
			}
		}
	}
}