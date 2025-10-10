namespace FootNotes.Core.Application
{
    public class ApiResponse<T>(bool success, T? data, string? message, int statusCode)
    {
        public bool Success { get; set; } = success;
        public string? Message { get; set; } = message;
        public T? Data { get; set; } = data;
        public int StatusCode { get; set; } = statusCode;
    }
}
