﻿using System;
using ReactiveUI;
using Splat;
using System.Diagnostics;
using Octokit;

namespace GitHubX.ViewModels
{
	public class LoginViewModel : ReactiveObject, IRoutableViewModel
	{
		public string UrlPathSegment {
			get { return "Login"; }
		}

		string _User;
		public string User
		{
			get { return _User; }
			set { this.RaiseAndSetIfChanged (ref _User, value); }
		}

		string _Password;
		public string Password
		{
			get { return _Password; }
			set { this.RaiseAndSetIfChanged (ref _Password, value); }
		}

		public IScreen HostScreen { get; protected set; }
		public IGitHubClient GitHubClient { get; protected set; }

		public ReactiveCommand<Object> SignIn { get; protected set; }

		public LoginViewModel(IScreen hostScreen = null, IGitHubClient gitHubClient = null)
		{
			HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
		    GitHubClient = gitHubClient ?? Locator.Current.GetService<IGitHubClient> ();

			SignIn = ReactiveCommand.Create();

			this.WhenAnyObservable(x => x.SignIn)
				.Subscribe(_ => { 

					GitHubClient.Connection.Credentials = new Credentials("");

					HostScreen.Router.Navigate.Execute(ViewModelConstructor.Current.Construct<TabbedViewModel>());
				});
		}
	}
}

