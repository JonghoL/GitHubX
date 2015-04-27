using System;
using System.Collections.Generic;

using Xamarin.Forms;
using GitHubX.ViewModels;
using ReactiveUI;

namespace GitHubX.Views
{
	public partial class TabbedView : TabbedPage, IViewFor<TabbedViewModel>
	{
		public TabbedView ()
		{
			InitializeComponent ();

			this.OneWayBind (ViewModel, vm => vm.TypeA_VM, v => v.firstTab.ViewModel);
			this.OneWayBind (ViewModel, vm => vm.TypeB_VM, v => v.secondTab.ViewModel);
		}

		/// <summary>
		/// The ViewModel to display
		/// </summary>
		public TabbedViewModel ViewModel {
			get { return (TabbedViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create<TabbedView, TabbedViewModel>(x => x.ViewModel, default(TabbedViewModel), BindingMode.OneWay);

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (TabbedViewModel)value; }
		}
	}
}

