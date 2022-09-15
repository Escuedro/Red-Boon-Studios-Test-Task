using Game.Commands;

namespace Game.Services.Commands
{
	public interface ICommandExecutor
	{
		public void Execute<TCommand>(TCommand command) where TCommand : Command;
	}
}