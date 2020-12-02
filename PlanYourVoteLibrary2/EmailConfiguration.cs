namespace PlanYourVoteLibrary2
{
    public class EmailConfiguration
    {
        public string NetworkCredName { get; set; }
        public string NetworkCredUserName { get; set; }
        public string NetworkCredPassword { get; set; }
        public string SmtpHost { get; set; }
        public bool SmtpEnableSSL { get; set; }
        public bool SmptUseDefaultCredentials { get; set; }
        public int SmtpPort { get; set; }
    }
}
