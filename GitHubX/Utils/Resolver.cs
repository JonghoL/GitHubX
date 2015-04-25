using Splat;

namespace GitHubX
{
	public static class Resolver
	{
		public static T GetService<T>()
		{
			return Locator.Current.GetService<IServiceConstructor>().Construct<T>();
		}
	}
}

