namespace unitofwork_core.Constant.User
{
    public static class UserStatus
    {
        public const string ACTIVE = "ACTIVE";
        public const string INACTIVE = "INACTIVE";
        public const string WAITING = "WAITING";

        public static List<string> GetAllStatus()
        {
            List<string> allStatus = new List<string>();
            allStatus.Add(ACTIVE);
            allStatus.Add(INACTIVE);
            allStatus.Add(WAITING);
            return allStatus;
        }
    }
}
