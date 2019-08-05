namespace GRINTSYS.SAPMiddleware.Mail
{
    public class EmailArgs
    {
        public string ApiKey { get; set; }
        public string FromEmail { get; set; }
        public string FromEmailDisplayName { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
    }
}