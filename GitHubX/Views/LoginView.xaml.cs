using Xamarin.Forms;
using ReactiveUI;
using GitHubX.ViewModels;

namespace GitHubX.Views
{
	public partial class LoginView : ContentPage, IViewFor<LoginViewModel>
	{
		public LoginView ()
		{
			InitializeComponent ();

			this.Bind (ViewModel, vm => vm.User, v => v.user.Text);
			this.Bind (ViewModel, vm => vm.Password, v => v.password.Text);

            this.BindCommand(ViewModel, vm => vm.SignIn, v => v.signIn);
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

