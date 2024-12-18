using System;
using System.Collections.Generic;

namespace SportSync.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<UserViewModel> Users { get; set; }
        public RegistrationStatistics Statistics { get; set; }
        public List<DisciplineViewModel> Disciplines { get; set; }
        public List<SessionViewModel> Sessions { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class RegistrationStatistics
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public Dictionary<string, int> RegistrationsPerDiscipline { get; set; }
        public Dictionary<string, int> RegistrationsPerMonth { get; set; }
    }

    public class DisciplineViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ActiveSessions { get; set; }
        public int RegisteredUsers { get; set; }
        public bool IsActive { get; set; }
    }
}
