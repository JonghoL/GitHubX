using ReactiveUI;
using ReactiveUI.XamForms;
using GitHubX.ViewModels;

namespace GitHubX.Views
{
	public class DifferentViewBase : ReactiveContentPage<DifferentViewModel> { }

	public partial class DifferentView : DifferentViewBase
	{
		public DifferentView ()
		{
			InitializeComponent ();

			this.BindCommand(ViewModel, x => x.HostScreen.Router.NavigateBack, x => x.NavigateBack);
		}
	}
}


