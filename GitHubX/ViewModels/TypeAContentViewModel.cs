using System;
using ReactiveUI;
using Octokit;
using Splat;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin;
using GitHubX.Utils;

namespace GitHubX.ViewModels
{
	public class TypeAContentViewModel : ReactiveObject, IXViewModel
	{
		public string Title
		{
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

		bool _IsRefreshing;
		public bool IsRefresing
		{
			get { return _IsRefreshing; }
			set { this.RaiseAndSetIfChanged (ref _IsRefreshing, value); }
		}

		public TypeAContentViewModel(IScreen hostScreen = null, IGitHubClient githubClient = null)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
			GitHubClient = githubClient ?? Locator.Current.GetService<IGitHubClient> ();
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

