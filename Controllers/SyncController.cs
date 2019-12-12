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
        private readonly ICollection _collection;

        public SyncController(ISyncService syncService, ICollection collection)
        {
            _syncService = syncService;
            _collection = collection;
        }

        [HttpGet("{name}")]
        public async Task SyncMailchimpMembersWithFirestoreAsync(string name)
        {
            if (name.ToUpper() == Constants.AllDayChaperones.ToUpper())
            {
                _collection.SetName(Constants.AllDayChaperones);
            }
            else if (name.ToUpper() == Constants.EveningChaperones.ToUpper())
            {
                _collection.SetName(Constants.EveningChaperones);
            }
            else if (name.ToUpper() == Constants.LebanonChaperones.ToUpper())
            {
                _collection.SetName(Constants.LebanonChaperones);
            }
            else if (name.ToUpper() == Constants.Drivers.ToUpper())
            {
                _collection.SetName(Constants.Drivers);
            }
            await _syncService.SyncMailchimpMembersWithFirestoreAsync();
        }
    }
}
