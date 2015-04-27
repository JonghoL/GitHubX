using System;
using ReactiveUI;
using Splat;

namespace GitHubX.ViewModels
{
	public class TabbedViewModel : ReactiveObject, IRoutableViewModel
	{
		public string UrlPathSegment {
			get { return "Tab"; }
		}

		public IScreen HostScreen { get; protected set; }

		TypeAContentViewModel _TypeA_VM;
		public TypeAContentViewModel TypeA_VM
		{
			get { return _TypeA_VM; }
			set { this.RaiseAndSetIfChanged (ref _TypeA_VM, value); }
		}

		TypeBContentViewModel _TypeB_VM;
		public TypeBContentViewModel TypeB_VM
		{
			get { return _TypeB_VM; }
			set { this.RaiseAndSetIfChanged (ref _TypeB_VM, value); }
		}

		public TabbedViewModel(IScreen hostScreen = null)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

			TypeA_VM = ViewModelConstructor.Current.Construct<TypeAContentViewModel> ();
			TypeB_VM = ViewModelConstructor.Current.Construct<TypeBContentViewModel> ();
		}
	}
}

