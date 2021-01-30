using SimpleGithubUserDataFetch.Service.dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleGithubUserDataFetch.Service
{
    public class UserService : GithubService
    {
        public async Task<UserDTO> FetchUser(string username)
        {
            var response = await GetHttpClient().GetAsync($"users/{username}");
            return await response.Content.ReadAsAsync<UserDTO>();
        }

        public async Task<List<FollowerDTO>> FetchFollowers(string username)
        {
            var response = await GetHttpClient().GetAsync($"users/{username}/followers");
            return await response.Content.ReadAsAsync<List<FollowerDTO>>();
        }

        public async Task<List<RepositoryDTO>> FetchRepositories(string username)
        {
            var response = await GetHttpClient().GetAsync($"users/{username}/repos");
            return await response.Content.ReadAsAsync<List<RepositoryDTO>>();
        }

        public async Task<SimpleGitUserData> FetchSimpleUserData(string username)
        {
            var user = new SimpleGitUserData();
            await FetchUser(username).ContinueWith(async u =>
            {
                user.Name = string.IsNullOrEmpty(u.Result.name) ? username : u.Result.name;
            });

            var followers = await FetchFollowers(username);

            foreach (FollowerDTO f in followers)
            {
                var repos = await FetchRepositories(f.login);
                var u = await FetchUser(f.login);

                user.AddFollower(new SimpleGitUserData.Follower
                {
                    FullName = string.IsNullOrEmpty(u.name) ? u.login : u.name,
                    repositories = repos
                });
            }

            return user;
        }
    }
}
