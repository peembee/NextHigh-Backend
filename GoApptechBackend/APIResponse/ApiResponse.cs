using System.Net;

namespace GoApptechBackend.APIResponse
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public object Result { get; set; } = new object();
    }
}
