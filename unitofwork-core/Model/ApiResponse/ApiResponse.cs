namespace unitofwork_core.Model.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public object? Error { get; set; }
        public T? Data { get; set; }
    }
}
