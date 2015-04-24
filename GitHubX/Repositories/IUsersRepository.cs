using System;
using Octokit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GitHubX.Repositories
{
	public interface IUsersRepository
	{
		Task<IEnumerable<User>> GetAll ();
	}
}

