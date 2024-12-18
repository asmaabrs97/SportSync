namespace SportSync.Models.ViewModels
{
    public class UserDashboardViewModel
    {
        public UserProfileViewModel Profile { get; set; }
        public List<Registration> ActiveRegistrations { get; set; }
        public List<Payment> RecentPayments { get; set; }
        public List<Session> UpcomingSessions { get; set; }
        public List<Document> PendingDocuments { get; set; }

        public UserDashboardViewModel()
        {
            ActiveRegistrations = new List<Registration>();
            RecentPayments = new List<Payment>();
            UpcomingSessions = new List<Session>();
            PendingDocuments = new List<Document>();
        }
    }
}
