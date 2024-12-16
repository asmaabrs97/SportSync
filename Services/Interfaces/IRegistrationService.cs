using System.Collections.Generic;
using System.Threading.Tasks;
using SportSync.Models;
using SportSync.Models.Common;
using SportSync.Models.DTOs;

namespace SportSync.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<OperationResult> CreateRegistrationAsync(RegistrationRequest request);
        Task<OperationResult> CancelRegistrationAsync(int registrationId);
        Task<Registration> GetRegistrationByIdAsync(int registrationId);
        Task<IEnumerable<Registration>> GetUserRegistrationsAsync(string userId);
        Task<IEnumerable<Registration>> GetSportRegistrationsAsync(int sportId);
        Task<bool> IsUserRegisteredAsync(string userId, int sportId);
        Task<OperationResult> UpdateRegistrationStatusAsync(int registrationId, RegistrationStatus status);
        Task<IEnumerable<Registration>> GetPendingRegistrationsAsync();
        Task<int> GetActiveRegistrationsCountAsync(int sportId);
        Task<bool> CanUserRegisterAsync(string userId, int sportId);
        Task<OperationResult> RenewRegistrationAsync(int registrationId);
    }
}
