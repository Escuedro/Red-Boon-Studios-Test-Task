using Game.Model;
using UnityEngine;

namespace Game.View.UI
{
	public class PlayerWalletView : MonoBehaviour
	{
		[SerializeField]
		private IntFieldView _coinsField;

		public void Init(PlayerWallet playerWallet)
		{
			_coinsField.Init(playerWallet.Coins);
		}
	}
}