namespace Game.Services
{
	public interface IModelProvider
	{
		public T Get<T>();

		public void BindModel<T>(T model);
	}
}