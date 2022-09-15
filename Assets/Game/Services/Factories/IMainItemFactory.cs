using Game.Configs;
using Game.Model;

namespace Game.Services.Factories
{
	public interface IMainItemFactory
	{
		public T Create<T>(object id = null)
				where T : Item;

		public void BindConfig<T, TConfig>(ItemConfig itemConfig, object id = null)
				where T : Item
				where TConfig : ItemConfig;
	}
}