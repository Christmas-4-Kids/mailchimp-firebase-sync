using mailchimp_firebase_sync.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mailchimp_firebase_sync.Services
{
    public class SyncService : ISyncService
    {
        private readonly IMailchimpService _mailchimpService;
        private readonly IFirestoreService _firestoreService;
        private readonly ILogger<SyncService> _logger;
        private readonly ICollection _collection;

        public SyncService(IMailchimpService mailchimpService, IFirestoreService firestoreService, ILogger<SyncService> logger, ICollection collection)
        {
            _mailchimpService = mailchimpService;
            _firestoreService = firestoreService;
            _logger = logger;
            _collection = collection;
        }
        public async Task SyncMailchimpMembersWithFirestoreAsync()
        {
            try
            {
                var mailchimpMembers = _collection.GetName() switch
                {
                    Constants.AllDayChaperones => await _mailchimpService.GetAllDayChaperones(),
                    Constants.EveningChaperones => await _mailchimpService.GetEveningChaperones(),
                    Constants.LebanonChaperones => await _mailchimpService.GetLebanonChaperones(),
                    Constants.Drivers => await _mailchimpService.GetDrivers(),
                    _ => null
                };
                _logger.LogInformation("Starting Sync...");
                var firestoreMembers = await _firestoreService.GetAllFirestoreMembersAsync();

                var membersToUpdate = new List<MailchimpMember>();
                var membersToAdd = new List<MailchimpMember>();
                var membersToRemoveMailchimpInfo = new List<string>();
                var firestoreMemberMailchimpMemberIdsFound = new List<string>();

                foreach (var member in mailchimpMembers.members)
                {
                    var lastUpdatedInMailchimp = DateTime.Parse(member.lastChanged);

                    var matchingFirestoreMember = firestoreMembers.FirstOrDefault(m => m.mailchimpMemberId == member.id);

                    if (matchingFirestoreMember != null)
                    {
                        firestoreMemberMailchimpMemberIdsFound.Add(matchingFirestoreMember.mailchimpMemberId);
                        var lastUpdatedInFirestore = DateTime.Parse(matchingFirestoreMember.lastUpdated);
                        if (lastUpdatedInMailchimp > lastUpdatedInFirestore)
                        {
                            _logger.LogInformation($"Found mailchimp member to update: {member.id}");
                            membersToUpdate.Add(member);
                        }
                    }
                    else
                    {
                        _logger.LogInformation($"Found mailchimp member to add: {member.id}");
                        membersToAdd.Add(member);
                    }
                }
                var existingFirestoreMailchimpMemberIds = firestoreMembers.Select(m => m.mailchimpMemberId).ToList();
                var mailchimpMembersExistingInFirestoreNotInMailchimp = firestoreMemberMailchimpMemberIdsFound.Except(existingFirestoreMailchimpMemberIds).ToList();
                membersToRemoveMailchimpInfo.AddRange(mailchimpMembersExistingInFirestoreNotInMailchimp);

                await _firestoreService.UpdateMultipleMembersAsync(membersToUpdate);
                await _firestoreService.CreateMultipleMembersAsync(membersToAdd);
                await _firestoreService.RemoveMailchimpInfoForMultipleMembersAsync(membersToRemoveMailchimpInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the sync process: {ex.Message}");
                _logger.LogError(JsonConvert.SerializeObject(ex));
            }
        }
    }
}
