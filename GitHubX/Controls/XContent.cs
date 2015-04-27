using System;
using Xamarin.Forms;
using ReactiveUI;
using System.Diagnostics;
using GitHubX.Utils;

namespace GitHubX.Controls
{
	public class XContent : ContentPage
	{
		public static readonly BindableProperty ViewModelProperty =
			BindableProperty.Create<XContent, IXViewModel>(x => x.ViewModel, default(IXViewModel), BindingMode.OneWay, null, ViewModelChanged);

		public XContent () { }

		public IXViewModel ViewModel
		{
			get { return (IXViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		private static void ViewModelChanged(BindableObject bindable, IXViewModel oldValue, IXViewModel newValue)
		{
			if (newValue != null)
			{
				var vm = newValue as ReactiveObject;
				if (vm != null)
				{
					var xc = bindable as XContent;
					if (xc != null)
					{
						var ret = ViewLocator.Current.ResolveView(vm);
						if (ret == null) {
							var msg = String.Format(
								"Couldn't find a View for ViewModel. You probably need to register an IViewFor<{0}>",
								vm.GetType().Name);
							Debugger.Break ();
						}

						ret.ViewModel = vm;

						var view = (View)ret;
						xc.Content = view;
						xc.Title = newValue.Title;;
					}
				}
			}
		}
	}
}

