using System;
using System.Collections.Generic;
using Xamarin.Forms;
using GitHubX.ViewModels;
using ReactiveUI;

namespace GitHubX.Views
{
	public partial class TypeAContentView : ContentView, IViewFor<TypeAContentViewModel>
	{
		public TypeAContentView ()
		{
			InitializeComponent ();

			this.OneWayBind (ViewModel, vm => vm.Repositories, v => v.ListView.ItemsSource);

			this.WhenAnyValue (x => x.ListView.IsRefreshing)
				.Subscribe (async y => 
					{
						if(y){
							await ViewModel.OnRefresh();
							this.ListView.IsRefreshing = false;
						}
					});
		}

		/// <summary>
		/// The ViewModel to display
		/// </summary>
		public TypeAContentViewModel ViewModel {
			get { return (TypeAContentViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create<TypeAContentView, TypeAContentViewModel>(x => x.ViewModel, default(TypeAContentViewModel), BindingMode.OneWay);

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (TypeAContentViewModel)value; }
		}
	}
}

