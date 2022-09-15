using Game.Configs;
using Game.Model;

namespace Game.Services.Factories
{
	public class ConsumableFactory : ItemFactoryBase<ConsumableItem, ConsumableConfig>
	{
		protected override ConsumableItem Create(ConsumableConfig config)
		{
			return new ConsumableItem(config.Cost, config.Name, config.Icon, config.Heal);
		}
	}
}