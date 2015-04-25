using System.Collections.Generic;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using Octokit;
using Xamarin;

namespace GitHubX.ViewModels
{
	public class TestViewModel : ReactiveObject, IRoutableViewModel
	{
		public string UrlPathSegment {
			get { return "Repos"; }
		}

		public IScreen HostScreen { get; protected set; }
		public IGitHubClient GitHubClient { get; protected set; }

		ReactiveList<Repository> _Repositories;
		public ReactiveList<Repository> Repositories
		{
			get { return _Repositories; }
			set { this.RaiseAndSetIfChanged (ref _Repositories, value); }
		}

		public TestViewModel(IScreen hostScreen, IGitHubClient githubClient)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
			GitHubClient = githubClient ?? Locator.Current.GetService<IGitHubClient> ();

			//var repos = GetRepositories ().Result;
			//this.Repositories = new ReactiveList<Repository> (repos.Where(r => r.FullName.Contains("Git")));
		}

		public async Task<IReadOnlyList<Repository>> GetRepositories()
		{
//			var handle = Insights.TrackTime("TimeToGetRepos");
//			handle.Start();
//			var repos = await GitHubClient.Repository.GetAllForCurrent ();
//			handle.Stop ();

//			return repos;

			using (var handle = Insights.TrackTime("TimeToGetRepos")) 
			{
				return await GitHubClient.Repository.GetAllForCurrent ();
			}
		}

		public async Task OnRefresh()
		{
			var repos = await GetRepositories ();
			this.Repositories = new ReactiveList<Repository> (repos);
		}
	}
}
