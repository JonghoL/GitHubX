using System;
using ReactiveUI;
using Splat;
using Xamarin.Forms;
using ReactiveUI.XamForms;

using GitHubX.Views;
using GitHubX.ViewModels;

namespace GitHubX
{
	public class AppBootstrapper : ReactiveObject, IScreen
	{
		public RoutingState Router { get; protected set; }

		public AppBootstrapper()
		{
			Router = new RoutingState();

			Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
			Locator.CurrentMutable.Register(() => new TestView(), typeof(IViewFor<TestViewModel>));
			Locator.CurrentMutable.Register(() => new DifferentView(), typeof(IViewFor<DifferentViewModel>));

			Locator.CurrentMutable.RegisterConstant (new Octokit.GitHubClient(new Octokit.ProductHeaderValue("GitHubX")),
													 typeof(Octokit.IGitHubClient));

			Router.Navigate.Execute(new TestViewModel(this));
			// Router.NavigationStack.Add(new TestViewModel(this));
		}

		public Page CreateMainView()
		{
			return new RoutedViewHost();
		}
	}
}

