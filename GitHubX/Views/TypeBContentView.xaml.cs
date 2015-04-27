using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ReactiveUI;

namespace GitHubX.Views
{
	public partial class TypeBContentView : ContentView, IViewFor<TypeBContentViewModel>
	{
		public TypeBContentView ()
		{
			InitializeComponent ();
		}

		/// <summary>
		/// The ViewModel to display
		/// </summary>
		public TypeBContentViewModel ViewModel {
			get { return (TypeBContentViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create<TypeBContentView, TypeBContentViewModel>(x => x.ViewModel, default(TypeBContentViewModel), BindingMode.OneWay);

		object IViewFor.ViewModel {
			get { return ViewModel; }
			set { ViewModel = (TypeBContentViewModel)value; }
		}
	}
}
