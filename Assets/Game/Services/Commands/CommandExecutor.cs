using Game.Commands;

namespace Game.Services.Commands
{
	public class CommandExecutor : ICommandExecutor
	{
		private readonly IModelProvider _modelProvider;

		public CommandExecutor(IModelProvider modelProvider)
		{
			_modelProvider = modelProvider;
		}

		public void Execute<TCommand>(TCommand command) where TCommand : Command
		{
			command.Inject(_modelProvider);
			command.Execute();
		}
	}
}