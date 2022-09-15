using Game.Model;

namespace Game.Commands
{
	public class AddPlayerItemCommand : Command
	{
		private Item _item;

		public AddPlayerItemCommand(Item item)
		{
			_item = item;
		}

		public override void Execute()
		{
			Get<PlayerItems>().Items.Add(_item);
		}
	}
}