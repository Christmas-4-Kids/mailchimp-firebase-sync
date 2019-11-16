using mailchimp_firebase_sync.Models;
using mailchimp_firebase_sync.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace mailchimp_firebase_sync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MailchimpController
    {
        private readonly IMailchimpService _mailchimpService;

        public MailchimpController(IMailchimpService mailchimpService)
        {
            _mailchimpService = mailchimpService;
        }
        [HttpGet("all-day-chaperones")]
        public async Task<MailchimpMembers> GetAllDayChaperones()
        {
            var results = await _mailchimpService.GetAllDayChaperones();
            return results;
        }
    }
}
