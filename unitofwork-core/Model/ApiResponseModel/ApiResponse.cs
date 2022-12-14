namespace unitofwork_core.Model.ApiResponseModel
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public void ToFailedResponse(string message) {
            Success = false;
            Message = message;
        }

        public void ToSuccessResponse(string message)
        {
            Success = true;
            Message = message;
        }

        public void ToSuccessResponse(T data,string message)
        {
            Success = true;
            Message = message;
            Data = data;
        }
    }

    public class ApiResponse {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        public void ToFailedResponse(string message) {
            Success = false;
            Message = message;
        }

        public void ToSuccessResponse(string message) {
            Success = true;
            Message = message;
        }
    }

    public class ApiResponseListError
    {
        public bool Success { get; set; } = true;
        public string Note { get; set; } = string.Empty;
        public List<string> Messages { get; set; }

        public ApiResponseListError()
        {
            Messages = new List<string>();
        }
        public void ToFailedResponse(List<string> errors)
        {
            Success = false;
            Messages = errors;
        }

        public void ToSuccessResponse(string message)
        {
            Success = true;
            Messages = new List<string>();
        }
    }
}
