using System.Net;

namespace ApplicationService.Exceptions
{
    public class ServiceException : Exception
    {
        public int Code { get; set; }
        public object Json { get; set; }

        public ServiceException(string message, HttpStatusCode code) : base(message)
        {
            Code = (int)code;
            Json = new { code, message };
        }
    }
}
