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
			Locator.CurrentMutable.RegisterConstant(new DefaultViewModelConstructor(), typeof(IViewModelConstructor));

			Locator.CurrentMutable.RegisterLazySingleton(() => new LoginView (), typeof(IViewFor<LoginViewModel>));
			Locator.CurrentMutable.RegisterLazySingleton(() => new TabbedView (), typeof(IViewFor<TabbedViewModel>));
			Locator.CurrentMutable.RegisterLazySingleton(() => new TypeAContentView (), typeof(IViewFor<TypeAContentViewModel>));
			Locator.CurrentMutable.RegisterLazySingleton(() => new TypeBContentView (), typeof(IViewFor<TypeBContentViewModel>));
			Locator.CurrentMutable.RegisterLazySingleton(() => new TestView(), typeof(IViewFor<TestViewModel>));
			Locator.CurrentMutable.RegisterLazySingleton(() => new DifferentView(), typeof(IViewFor<DifferentViewModel>));

			Locator.CurrentMutable.RegisterLazySingleton(() => new Octokit.GitHubClient(new Octokit.ProductHeaderValue("GitHubX")),
													 typeof(Octokit.IGitHubClient));

			//new LoginViewModel();
			//new LoginViewModel(Locator.Current.GetService<IScreen>(), Locator.Current.GetService<Octokit.IGitHubClient>());
			var firstViewModel = ViewModelConstructor.Current.Construct<LoginViewModel>();
			Router.Navigate.Execute(firstViewModel);
		}

		public Page CreateMainView()
		{
			return new RoutedViewHost();
		}
	}
}

