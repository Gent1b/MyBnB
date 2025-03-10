
    namespace User.Management.API.Models
    {
        public class ApiResponse<T> where T : class
        {
            public bool Success { get; set; }
            public int StatusCode { get; set; } // This is the HTTP status code.
            public string? Message { get; set; }
            public T? Data { get; set; }
            public string? Error { get; set; }
        }
    }

