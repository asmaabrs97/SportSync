using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SportSync.Models;
using SportSync.Models.Common;
using SportSync.Models.DTOs;
using SportSync.Models.ViewModels;

namespace SportSync.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request);
        Task<PaymentResult> CreateSubscriptionAsync(string userId, int sportId);
        Task<RefundResult> ProcessRefundAsync(RefundRequest request);
        Task<IEnumerable<Payment>> GetUserPaymentsAsync(string userId);
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Payment>> GetPendingPaymentsAsync();
        Task<OperationResult> UpdatePaymentStatusAsync(int paymentId, PaymentStatus status);
        Task<bool> HasOutstandingPaymentsAsync(string userId);
        Task<IEnumerable<PaymentMethod>> GetUserPaymentMethodsAsync(string userId);
        Task<OperationResult> AddPaymentMethodAsync(string userId, PaymentMethodViewModel model);
        Task<OperationResult> RemovePaymentMethodAsync(string userId, string paymentMethodId);
    }
}
