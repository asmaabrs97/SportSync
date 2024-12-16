namespace SportSync.Models.DTOs
{
    public class PaymentResult
    {
        public bool Success { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }
        public decimal Amount { get; set; }
        public DateTime ProcessedAt { get; set; }

        public static PaymentResult Successful(string transactionId, decimal amount)
        {
            return new PaymentResult
            {
                Success = true,
                TransactionId = transactionId,
                Amount = amount,
                ProcessedAt = DateTime.UtcNow,
                Message = "Payment processed successfully"
            };
        }

        public static PaymentResult Failed(string message)
        {
            return new PaymentResult
            {
                Success = false,
                Message = message,
                ProcessedAt = DateTime.UtcNow
            };
        }
    }
}
