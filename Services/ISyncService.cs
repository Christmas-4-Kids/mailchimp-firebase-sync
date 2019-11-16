using System.Threading.Tasks;

namespace mailchimp_firebase_sync.Services
{
    public interface ISyncService
    {
        Task SyncMailchimpMembersWithFirestoreAsync();
    }
}