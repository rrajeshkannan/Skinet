namespace API.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, String details = null) : base(statusCode, message)
        {
            Details = details;
        }
        public String Details { get; set; }
    }
}