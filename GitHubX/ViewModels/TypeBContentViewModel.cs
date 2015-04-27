using System;
using ReactiveUI;
using GitHubX.Utils;

namespace GitHubX
{
	public class TypeBContentViewModel : ReactiveObject, IXViewModel
	{
		public string Title
		{
			get { return "Type B"; }
		}
	}
}

