namespace Sophia.Core
{
	/// <summary>
	/// Spawner interface
	/// </summary>
	/// <typeparam name="T">Type of the object to spawn</typeparam>
	public interface ISpawner<T>
	{
		T spawn();
		T[] spawn(int amount);
	}
}