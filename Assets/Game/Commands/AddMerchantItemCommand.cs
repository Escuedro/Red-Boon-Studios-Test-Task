using Game.Model;

namespace Game.Commands
{
	public class AddMerchantItemCommand : Command
	{
		private Item _item;

		public AddMerchantItemCommand(Item item)
		{
			_item = item;
		}

		public override void Execute()
		{
			Get<MerchantItems>().Items.Add(_item);
		}
	}
}