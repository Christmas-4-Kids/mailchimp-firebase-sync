using mailchimp_firebase_sync.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace mailchimp_firebase_sync.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SyncController
    {
        private readonly ISyncService _syncService;

        public SyncController(ISyncService syncService)
        {
            _syncService = syncService;
        }

        [HttpPost]
        public async Task SyncMailchimpMembersWithFirestoreAsync()
        {
            await _syncService.SyncMailchimpMembersWithFirestoreAsync();
        }
    }
}
