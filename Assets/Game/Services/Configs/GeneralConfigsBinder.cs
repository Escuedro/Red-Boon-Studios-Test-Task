using Game.Configs;
using UnityEngine;

namespace Game.Services
{
	public class GeneralConfigsBinder : MonoBehaviour
	{
		[SerializeField]
		private MerchantConfig _merchantConfig;

		public void BindGeneralConfigs(IModelProvider modelProvider)
		{
			modelProvider.BindModel(_merchantConfig);
		}
	}
}