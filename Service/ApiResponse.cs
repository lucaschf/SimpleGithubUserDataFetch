namespace SimpleGithubUserDataFetch.Service
{
    public class ApiResponse<T>
    {
        public T data { get; set; }

        public string Error { get; set; }

        public bool IsSuccessFull()
        {
            return string.IsNullOrEmpty(Error) && data != null;
        }
    }
}
