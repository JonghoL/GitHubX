using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.Runtime.Serialization;
using Splat;
using Octokit;
using System.Collections.ObjectModel;
using Xamarin;

namespace GitHubX.ViewModels
{
	[DataContract]
	public class TestViewModel : ReactiveObject, IRoutableViewModel
	{
		public string UrlPathSegment {
			get { return "Repos"; }
		}

		public IScreen HostScreen { get; protected set; }
		public IGitHubClient GitHubClient { get; protected set; }

		string _TheGuid;
		[DataMember] public string TheGuid {
			get { return _TheGuid; }
			set { this.RaiseAndSetIfChanged(ref _TheGuid, value); }
		}

		ReactiveList<Repository> _Repositories;
		public ReactiveList<Repository> Repositories
		{
			get { return _Repositories; }
			set { this.RaiseAndSetIfChanged (ref _Repositories, value); }
		}

		public TestViewModel(IScreen hostScreen = null)
		{
			TheGuid = Guid.NewGuid().ToString();

			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
			GitHubClient = Locator.Current.GetService<IGitHubClient> ();

			var handle = Insights.TrackTime("TimeToGetRepos");
			handle.Start();

			var repos = GitHubClient.Repository.GetAllForUser ("jonghoL").Result;
			handle.Stop ();

			this.Repositories = new ReactiveList<Repository> (repos);
		}
	}
}
