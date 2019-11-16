using System.Threading.Tasks;
using mailchimp_firebase_sync.Models;

namespace mailchimp_firebase_sync.ExternalClients
{
    public interface IMailchimpClient
    {
        Task<MailchimpMembers> GetAllDayChaperones();
    }
}