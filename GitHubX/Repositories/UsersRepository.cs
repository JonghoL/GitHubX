using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

namespace GitHubX.Repositories
{
	public class UsersRepository : IUsersRepository
	{
		public UsersRepository ()
		{
		}

		#region IUserRepository implementation

		public Task<IEnumerable<User>> GetAll ()
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

