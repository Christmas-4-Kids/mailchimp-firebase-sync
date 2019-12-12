using mailchimp_firebase_sync.Config;
using mailchimp_firebase_sync.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace mailchimp_firebase_sync.ExternalClients
{
    public class MailchimpClient : IMailchimpClient
    {
        private readonly ILogger<MailchimpClient> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _allDayChaperonesEndpoint;
        private readonly string _eveningChaperonesEndpoint;
        private readonly JsonSerializerSettings _snakeCaseSerializer;
        private readonly string _lebanonChaperonesEndpoint;
        private readonly string _driversEndpoint;

        public MailchimpClient(HttpClient httpClient, IOptions<ExternalClientsConfig> externalClientsConfig, ILogger<MailchimpClient> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            var apiKey = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes($"conner:{externalClientsConfig.Value.MailchimpApi.ApiKey}"));
            _logger.LogInformation($"Setting apiKey: {apiKey}");
            var authHeader = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", apiKey);
            _httpClient.DefaultRequestHeaders.Authorization = authHeader;
            _httpClient.BaseAddress = new Uri(externalClientsConfig.Value.MailchimpApi.BaseUrl);
            _logger.LogInformation($"Setting baseAddress: {_httpClient.BaseAddress}");
            _allDayChaperonesEndpoint = externalClientsConfig.Value.MailchimpApi.AllDayChaperonesEndpoint;
            _eveningChaperonesEndpoint = externalClientsConfig.Value.MailchimpApi.EveningChaperonesEndpoint;
            _lebanonChaperonesEndpoint = externalClientsConfig.Value.MailchimpApi.LebanonChaperonesEndpoint;
            _driversEndpoint = externalClientsConfig.Value.MailchimpApi.DriversEndpoint;
            _logger.LogInformation($"Setting allDayChaperones Endpoint: {_allDayChaperonesEndpoint}");
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            _snakeCaseSerializer = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
        }

        public async Task<MailchimpMembers> GetAllDayChaperones()
        {
            return await GetEndpoint(_allDayChaperonesEndpoint, "All Day Chaperones");
        }

        public async Task<MailchimpMembers> GetEveningChaperones()
        {
            return await GetEndpoint(_eveningChaperonesEndpoint, "Evening Chaperones");
        }

        public async Task<MailchimpMembers> GetLebanonChaperones()
        {
            return await GetEndpoint(_lebanonChaperonesEndpoint, "Lebanon Chaperones");
        }

        public async Task<MailchimpMembers> GetDrivers()
        {
            return await GetEndpoint(_driversEndpoint, "Drivers");
        }

        private async Task<MailchimpMembers> GetEndpoint(string endpoint, string endpointName)
        {
            try
            {
                var url = new Uri(endpoint, UriKind.Relative);
                _logger.LogInformation($"Getting {endpointName} from uri: {url}");
                var result = await _httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();
                var strResult = await result.Content.ReadAsStringAsync();
                _logger.LogInformation($"Got {endpointName}.");
                _logger.LogInformation($"Converting to MailchimpMembers and returning...");
                return JsonConvert.DeserializeObject<MailchimpMembers>(strResult, _snakeCaseSerializer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong getting {endpointName}.");
                _logger.LogError(JsonConvert.SerializeObject(ex));
                return null;
            }
        }
    }
}
