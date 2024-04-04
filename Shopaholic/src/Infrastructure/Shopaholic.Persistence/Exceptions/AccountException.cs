using System.Net;

namespace Shopaholic.Persistence.Exceptions
{
    public class AccountException : Exception
    {
        public string? CustomMessage { get; set; }
        public int StatusCode { get; set; }
        public AccountException(string? customMessage)
        {
            CustomMessage = customMessage;
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
