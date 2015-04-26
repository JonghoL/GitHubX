using System;

using Xamarin.Forms;
using ReactiveUI;
using GitHubX.ViewModels;

namespace GitHubX.Views
{
	public partial class TestView : ContentPage, IViewFor<TestViewModel>
	{
		public TestView ()
		{
			InitializeComponent();

			this.OneWayBind (ViewModel, vm => vm.Repositories, v => v.ListView.ItemsSource);

			//this.Bind (ViewModel, vm => vm.IsRefresing, v => v.ListView.IsRefreshing);
			this.WhenAnyValue (x => x.ListView.IsRefreshing)
				.Subscribe (async y => 
					{
						if(y)
						{
							await ViewModel.OnRefresh();
							this.ListView.IsRefreshing = !y;
						}
				});
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

