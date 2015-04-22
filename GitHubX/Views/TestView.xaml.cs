using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using System.Reactive.Linq;
using GitHubX.ViewModels;

namespace GitHubX.Views
{
	public partial class TestView : ContentPage, IViewFor<TestViewModel>
	{
		public TestView ()
		{
			InitializeComponent();

//			this.OneWayBind(ViewModel, x => x.TheGuid, x => x.TheGuid.Text);
//
//			this.WhenAnyValue(x => x.ViewModel.HostScreen.Router)
//				.Select(x => x.NavigateCommandFor<DifferentViewModel>())
//				.BindTo(this, x => x.NavigateToDifferentView.Command);

			this.OneWayBind (ViewModel, x => x.Repositories, x => x.ListView.ItemsSource);
		}

		/// <summary>
		/// The ViewModel to display
		/// </summary>
		public TestViewModel ViewModel {
			get { return (TestViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create<TestView, TestViewModel>(x => x.ViewModel, default(TestViewModel), BindingMode.OneWay);

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (TestViewModel)value; }
		}
	}
}

