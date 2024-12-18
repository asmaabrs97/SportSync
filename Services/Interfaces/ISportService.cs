using System.Collections.Generic;
using System.Threading.Tasks;
using SportSync.Models;
using SportSync.Models.Common;
using SportSync.Models.ViewModels;

namespace SportSync.Services.Interfaces
{
    public interface ISportService
    {
        Task<IEnumerable<Sport>> GetAllSportsAsync();
        Task<Sport> GetSportByIdAsync(int id);
        Task<OperationResult> CreateSportAsync(SportViewModel model);
        Task<OperationResult> UpdateSportAsync(int id, SportViewModel model);
        Task<OperationResult> DeleteSportAsync(int id);
        Task<IEnumerable<Sport>> GetActiveSportsAsync();
        Task<IEnumerable<Sport>> GetSportsByCoachAsync(int coachId);
        Task<OperationResult> AssignCoachToSportAsync(int sportId, int coachId);
        Task<OperationResult> RemoveCoachFromSportAsync(int sportId, int coachId);
        Task<decimal> GetSportPriceAsync(int sportId);
        Task<IEnumerable<Session>> GetSportScheduleAsync(int sportId);
        Task<int> GetActiveParticipantsCountAsync(int sportId);
        Task<bool> IsSportFullAsync(int sportId);
        Task<IEnumerable<Sport>> SearchSportsAsync(string searchTerm);
    }
}
