namespace SportSync.Models.Common
{
    public class OperationResult
    {
        public bool Successful { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public static OperationResult Failed(string message) =>
            new OperationResult { Successful = false, Message = message };

        public static OperationResult Success(string? message = null, object? data = null) =>
            new OperationResult { Successful = true, Message = message, Data = data };
    }
}
