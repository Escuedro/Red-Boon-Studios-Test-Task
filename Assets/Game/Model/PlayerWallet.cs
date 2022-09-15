using Core.ReactiveField;

namespace Game.Model
{
	public class PlayerWallet
	{
		public ReactiveField<int> Coins = new ReactiveField<int>(50);
	}
}