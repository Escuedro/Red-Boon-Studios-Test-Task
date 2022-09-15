using System;
using Game.Configs;
using Game.Model;
using UnityEngine;

namespace Game.Commands
{
	public class BuyItemCommand : Command
	{
		private Item _item;

		public BuyItemCommand(Item item)
		{
			_item = item;
		}

		public override void Execute()
		{
			PlayerWallet playerWallet = Get<PlayerWallet>();
			int itemCost = _item.Cost;
			if (itemCost > playerWallet.Coins.Value)
			{
				throw new Exception($"Cannot buy item [{_item.Name}]! Not enough gold");
			}
			playerWallet.Coins.Value -= itemCost;
			Get<PlayerItems>().Items.Add(_item);
			Get<MerchantItems>().Items.Remove(_item);
		}
	}
}