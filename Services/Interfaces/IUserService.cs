using System.Collections.Generic;
using System.Threading.Tasks;
using SportSync.Models;
using SportSync.Models.Common;
using SportSync.Models.ViewModels;

namespace SportSync.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string id);
        Task<UserProfile> GetUserProfileAsync(string userId);
        Task<OperationResult> UpdateUserProfileAsync(string userId, UserProfileViewModel model);
        Task<OperationResult> UpdateUserStatusAsync(string userId, bool isActive);
        Task<IEnumerable<Registration>> GetUserRegistrationsAsync(string userId);
        Task<IEnumerable<Payment>> GetUserPaymentsAsync(string userId);
        Task<IEnumerable<Document>> GetUserDocumentsAsync(string userId);
        Task<bool> HasActiveSubscriptionAsync(string userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetUsersByRoleAsync(string role);
        Task<OperationResult> AssignRoleToUserAsync(string userId, string role);
        Task<OperationResult> RemoveRoleFromUserAsync(string userId, string role);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<UserDashboardViewModel> GetUserDashboardDataAsync(string userId);
        Task<OperationResult> DeleteUserAsync(string userId);
    }
}
