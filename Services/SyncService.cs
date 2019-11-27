﻿using mailchimp_firebase_sync.Models;
using Microsoft.Extensions.Logging;
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

        public SyncService(IMailchimpService mailchimpService, IFirestoreService firestoreService, ILogger<SyncService> logger)
        {
            _mailchimpService = mailchimpService;
            _firestoreService = firestoreService;
            _logger = logger;
        }
        public async Task SyncMailchimpMembersWithFirestoreAsync()
        {
            try
            {
                _logger.LogInformation("Starting Sync...");
                var allDayChaperones = await _mailchimpService.GetAllDayChaperones();
                var firestoreMembers = await _firestoreService.GetAllFirestoreMembersAsync();

                var membersToUpdate = new List<MailchimpMember>();
                var membersToAdd = new List<MailchimpMember>();
                var membersToRemoveMailchimpInfo = new List<string>();
                var firestoreMemberMailchimpMemberIdsFound = new List<string>();

                foreach (var allDayChaperone in allDayChaperones.Members)
                {
                    var lastUpdatedInMailchimp = DateTime.Parse(allDayChaperone.LastChanged);

                    var matchingFirestoreMember = firestoreMembers.FirstOrDefault(m => m.MailchimpMemberId == allDayChaperone.Id);

                    if (matchingFirestoreMember != null)
                    {
                        firestoreMemberMailchimpMemberIdsFound.Add(matchingFirestoreMember.MailchimpMemberId);
                        var lastUpdatedInFirestore = DateTime.Parse(matchingFirestoreMember.LastUpdated);
                        if (lastUpdatedInMailchimp > lastUpdatedInFirestore)
                        {
                            membersToUpdate.Add(allDayChaperone);
                        }
                    }
                    else
                    {
                        membersToAdd.Add(allDayChaperone);
                    }
                }
                var existingFirestoreMailchimpMemberIds = firestoreMembers.Select(m => m.MailchimpMemberId).ToList();
                var mailchimpMembersExistingInFirestoreNotInMailchimp = firestoreMemberMailchimpMemberIdsFound.Except(existingFirestoreMailchimpMemberIds).ToList();
                membersToRemoveMailchimpInfo.AddRange(mailchimpMembersExistingInFirestoreNotInMailchimp);

                await _firestoreService.UpdateMultipleMembersAsync(membersToUpdate);
                await _firestoreService.CreateMultipleMembersAsync(membersToAdd);
                await _firestoreService.RemoveMailchimpInfoForMultipleMembersAsync(membersToRemoveMailchimpInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
