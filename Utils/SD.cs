namespace Utils
{
    public static class SD
    {
        public const string Role_Admin = "Admin";
        public const string Vacation = "Vacation";
        public const string Remote = "Remote";
        public const string SickLeave = "Sick Leave";
        public static IEnumerable<string> LeaveTypes = [Vacation, Remote, SickLeave];

    }
}
