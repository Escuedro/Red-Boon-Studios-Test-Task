using Game.Services;

namespace Game.Commands
{
	public abstract class Command
	{
		private IModelProvider _modelProvider;

		public void Inject(IModelProvider modelProvider)
		{
			_modelProvider = modelProvider;
		}

		public abstract void Execute();

		protected T Get<T>()
		{
			return _modelProvider.Get<T>();
		}
	}
}