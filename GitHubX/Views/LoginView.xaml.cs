using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using GitHubX.ViewModels;
using System.Text;
using System.Diagnostics;

namespace GitHubX.Views
{
	public partial class LoginView : ContentPage, IViewFor<LoginViewModel>
	{
		public LoginView ()
		{
			InitializeComponent ();

			this.BindCommand(ViewModel, x => x.SignIn, x => x.signIn);

			this.Bind (ViewModel, x => x.User, x => x.user.Text);
			this.Bind (ViewModel, x => x.Password, x => x.password.Text);

//			this.WhenAnyObservable(x => x.ViewModel.SignIn)
//				.Subscribe(_ => {
//					Debug.WriteLine("Sign in");
//				});
		}

		/// <summary>
		/// The ViewModel to display
		/// </summary>
		public LoginViewModel ViewModel {
			get { return (LoginViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create<LoginView, LoginViewModel>(x => x.ViewModel, default(LoginViewModel), BindingMode.OneWay);

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (LoginViewModel)value; }
		}
	}
}

