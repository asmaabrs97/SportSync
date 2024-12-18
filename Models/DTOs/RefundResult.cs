namespace SportSync.Models.DTOs
{
    public class RefundResult
    {
        public bool Success { get; set; }
        public string RefundId { get; set; }
        public string Message { get; set; }
        public decimal Amount { get; set; }
        public DateTime ProcessedAt { get; set; }

        public static RefundResult Successful(string refundId, decimal amount)
        {
            return new RefundResult
            {
                Success = true,
                RefundId = refundId,
                Amount = amount,
                ProcessedAt = DateTime.UtcNow,
                Message = "Refund processed successfully"
            };
        }

        public static RefundResult Failed(string message)
        {
            return new RefundResult
            {
                Success = false,
                Message = message,
                ProcessedAt = DateTime.UtcNow
            };
        }
    }
}
