using System.Net.Http;

namespace SimpleGithubUserDataFetch.Service
{
    public interface IResponseCallback
    {
        void OnFailure(HttpResponseMessage responseMessage);
     
        void OnSuccess(HttpContent content);
    }
}
