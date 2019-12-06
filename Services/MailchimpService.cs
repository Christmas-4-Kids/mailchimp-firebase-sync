using mailchimp_firebase_sync.ExternalClients;
using mailchimp_firebase_sync.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace mailchimp_firebase_sync.Services
{
    public class MailchimpService : IMailchimpService
    {
        private readonly IMailchimpClient _mailchimpClient;
        private readonly ILogger<MailchimpService> _logger;

        public MailchimpService(IMailchimpClient mailchimpClient, ILogger<MailchimpService> logger)
        {
            _mailchimpClient = mailchimpClient;
            _logger = logger;
        }

        public async Task<MailchimpMembers> GetAllDayChaperones()
        {
            try
            {
                _logger.LogInformation("Getting all day chaperones...");
                var results = await _mailchimpClient.GetAllDayChaperones();
                _logger.LogInformation($"Got all day chaperones - count: {results.members.Count()}");
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong getting all day chaperones.");
                _logger.LogError(JsonConvert.SerializeObject(ex));
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
