using mailchimp_firebase_sync.ExternalClients;
using mailchimp_firebase_sync.Models;
using System;
using System.Threading.Tasks;

namespace mailchimp_firebase_sync.Services
{
    public class MailchimpService : IMailchimpService
    {
        private readonly IMailchimpClient _mailchimpClient;

        public MailchimpService(IMailchimpClient mailchimpClient)
        {
            _mailchimpClient = mailchimpClient;
        }

        public async Task<MailchimpMembers> GetAllDayChaperones()
        {
            try
            {
                var results = await _mailchimpClient.GetAllDayChaperones();
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
