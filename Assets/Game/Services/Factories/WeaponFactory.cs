using Game.Configs;
using Game.Model;

namespace Game.Services.Factories
{
	public class WeaponFactory : ItemFactoryBase<WeaponItem, WeaponConfig>
	{
		protected override WeaponItem Create(WeaponConfig config)
		{
			return new WeaponItem(config.Cost, config.Name, config.Icon, config.Damage);
		}
	}
}