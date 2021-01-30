using System.Collections.Generic;

namespace SimpleGithubUserDataFetch.Service.dto
{
    public class SimpleGitUserData
    {
        public string Name { get; set; }

        private readonly List<Follower> Followers = new List<Follower>();

        public List<Follower> GetFollowers()
        {
            return Followers;
        }

        public void AddFollower(Follower follower)
        {
            Followers.Add(follower);
        }

        public class Follower
        {
            public string FullName { get; set; }

            public List<RepositoryDTO> repositories { get; set; }
        }
    }
}
