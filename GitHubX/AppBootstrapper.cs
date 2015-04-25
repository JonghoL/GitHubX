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
			Locator.CurrentMutable.RegisterConstant(new ServiceConstructor(), typeof(IServiceConstructor));

			Locator.CurrentMutable.RegisterLazySingleton(() => new LoginView (), typeof(IViewFor<LoginViewModel>));
			Locator.CurrentMutable.RegisterLazySingleton(() => new TestView(), typeof(IViewFor<TestViewModel>));
			Locator.CurrentMutable.RegisterLazySingleton(() => new DifferentView(), typeof(IViewFor<DifferentViewModel>));

			Locator.CurrentMutable.RegisterLazySingleton (() => new Octokit.GitHubClient(new Octokit.ProductHeaderValue("GitHubX")),
													 typeof(Octokit.IGitHubClient));

			//Router.Navigate.Execute(new LoginViewModel(Locator.Current.GetService<IScreen>(), Locator.Current.GetService<Octokit.IGitHubClient>()));
			Router.Navigate.Execute(Resolver.GetService<LoginViewModel>());
		}

		public Page CreateMainView()
		{
			return new RoutedViewHost();
		}
	}
}

