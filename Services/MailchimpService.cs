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
            var type = "all day chaperones";
            try
            {
                _logger.LogInformation($"Getting {type}...");
                var results = await _mailchimpClient.GetAllDayChaperones();
                _logger.LogInformation($"Got {type} - count: {results.members.Count()}");
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong getting {type}.");
                _logger.LogError(JsonConvert.SerializeObject(ex));
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<MailchimpMembers> GetEveningChaperones()
        {
            var type = "evening chaperones";
            try
            {
                _logger.LogInformation($"Getting {type}...");
                var results = await _mailchimpClient.GetEveningChaperones();
                _logger.LogInformation($"Got {type} - count: {results.members.Count()}");
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong getting {type}.");
                _logger.LogError(JsonConvert.SerializeObject(ex));
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<MailchimpMembers> GetLebanonChaperones()
        {
            var type = "Lebanon chaperones";
            try
            {
                _logger.LogInformation($"Getting {type}...");
                var results = await _mailchimpClient.GetLebanonChaperones();
                _logger.LogInformation($"Got {type} - count: {results.members.Count()}");
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong getting {type}.");
                _logger.LogError(JsonConvert.SerializeObject(ex));
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<MailchimpMembers> GetDrivers() 
        {
            var type = "drivers";
            try
            {
                _logger.LogInformation($"Getting {type}...");
                var results = await _mailchimpClient.GetDrivers();
                _logger.LogInformation($"Got {type} - count: {results.members.Count()}");
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong getting {type}.");
                _logger.LogError(JsonConvert.SerializeObject(ex));
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
