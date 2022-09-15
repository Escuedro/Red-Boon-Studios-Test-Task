using System;
using Game.Configs;
using Game.Model;
using UnityEngine;

namespace Game.Commands
{
	public class SellItemCommand : Command
	{
		private Item _item;

		public SellItemCommand(Item item)
		{
			_item = item;
		}

		public override void Execute()
		{
			PlayerWallet playerWallet = Get<PlayerWallet>();
			int itemCost = Mathf.RoundToInt(_item.Cost * Get<MerchantConfig>().SellMultiplier);
			playerWallet.Coins.Value += itemCost;
			Get<PlayerItems>().Items.Remove(_item);
			Get<MerchantItems>().Items.Add(_item);
		}
	}
}