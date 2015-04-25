using System;

namespace GitHubX
{
	public interface IServiceConstructor
	{
		object Construct(Type type);
	}
}

