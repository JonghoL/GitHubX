using ReactiveUI;
using Splat;

namespace GitHubX.ViewModels
{
	public class DifferentViewModel : ReactiveObject, IRoutableViewModel
	{
		public string UrlPathSegment {
			get { return "Just a different screen"; }
		}

		public IScreen HostScreen { get; protected set; }

		public DifferentViewModel(IScreen hostScreen = null)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
		}
	}
}


