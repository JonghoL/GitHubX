using System;
using ReactiveUI;
using Splat;
using System.Diagnostics;

namespace GitHubX.ViewModels
{
	public class LoginViewModel : ReactiveObject, IRoutableViewModel
	{
		public string UrlPathSegment {
			get { return "Login"; }
		}

		string _User;
		public string User
		{
			get { return _User; }
			set {
				this.RaiseAndSetIfChanged (ref _User, value);
			}
		}

		string _Password;
		public string Password
		{
			get { return _Password; }
			set {
				this.RaiseAndSetIfChanged (ref _Password, value);
			}
		}

		public IScreen HostScreen { get; protected set; }

		public ReactiveCommand<Object> SignIn { get; protected set; }

		public LoginViewModel() : this(null) {}

		public LoginViewModel(IScreen hostScreen = null)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

			SignIn = ReactiveCommand.Create();

			this.WhenAnyObservable(x => x.SignIn)
				.Subscribe(_ => Debug.WriteLine(this.User+":"+this.Password));
		}
	}
}

