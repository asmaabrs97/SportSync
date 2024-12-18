using System.Threading.Tasks;
using SportSync.Models;
using SportSync.Models.ViewModels;
using SportSync.Models.Common;

namespace SportSync.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> ValidateUserAsync(string email, string password);
        Task<OperationResult> RegisterUserAsync(RegisterViewModel model);
        Task<OperationResult> LoginAsync(LoginViewModel model);
        Task<OperationResult> LogoutAsync();
        Task<OperationResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<OperationResult> ResetPasswordAsync(string email);
        Task<OperationResult> ConfirmEmailAsync(string userId, string token);
        Task<bool> IsEmailConfirmedAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
    }
}
