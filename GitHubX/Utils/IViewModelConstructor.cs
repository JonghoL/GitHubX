using System;

namespace GitHubX
{
	public interface IViewModelConstructor
	{
		object Construct(Type type);
	}
}

