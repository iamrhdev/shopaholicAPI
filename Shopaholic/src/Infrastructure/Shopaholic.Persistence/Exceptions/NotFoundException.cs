using System.Net;

namespace Shopaholic.Persistence.Exceptions
{
    public class NotFoundException : Exception
    {
        public string? CustomMessage { get; set; }
        public int StatusCode { get; set; }
        public NotFoundException(string? customMessage)
        {
            CustomMessage = customMessage;
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
