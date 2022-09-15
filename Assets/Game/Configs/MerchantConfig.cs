using UnityEngine;

namespace Game.Configs
{
	[CreateAssetMenu(fileName = "MerchantConfig", menuName = "Data/Merchant/MerchantConfig")]
	public class MerchantConfig : ScriptableObject
	{
		public float SellMultiplier = 0.8f;
	}
}