using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SimpleGithubUserDataFetch.Service
{
    public abstract class GithubService
    {
        public const string BASE_URL = "https://api.github.com";

        protected HttpClient GetHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BASE_URL),
            };

            client.DefaultRequestHeaders.Accept
               .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("SimpleGithubUserDataFetch", "1.0"));

            return client;
        }
    }
}
