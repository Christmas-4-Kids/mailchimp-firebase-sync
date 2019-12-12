namespace mailchimp_firebase_sync.Config
{
    public class ExternalClientsConfig
    {
        public MailchimpApi MailchimpApi { get; set; }
    }
    public class MailchimpApi
    {
        public string BaseUrl { get; set; }
        public string AllDayChaperonesEndpoint { get; set; }
        public string EveningChaperonesEndpoint { get; set; }
        public string LebanonChaperonesEndpoint { get; set; }
        public string DriversEndpoint { get; set; }
        public string ApiKey { get; set; }
    }
}
