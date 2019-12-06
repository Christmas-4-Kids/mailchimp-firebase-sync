using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using mailchimp_firebase_sync.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mailchimp_firebase_sync.Services
{
    public class FirestoreService : IFirestoreService
    {
        private readonly FirestoreDb _db;
        private readonly CollectionReference _collection;
        private ILogger<FirestoreService> _logger;

        public FirestoreService(ILogger<FirestoreService> logger)
        {
            _logger = logger;
            try
            {
                _logger.LogInformation("Setting firestore credentials and getting initial collection...");
                var firestoreClientBuilder = new FirestoreClientBuilder
                {
                    CredentialsPath = @$"{Directory.GetCurrentDirectory()}\c4k-events-credentials.json"
                };
                _db = FirestoreDb.Create("c4k-events", firestoreClientBuilder.Build());
                _collection = _db.Collection("members");
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong setting firestore credentials or getting firestore collection.");
                _logger.LogError(JsonConvert.SerializeObject(ex));
                Console.WriteLine(ex);
            }

        }
        public async Task CreateMemberAsync(MailchimpMember mailchimpMember)
        {
            var firestoreMember = new FirestoreMember()
            {
                id = Guid.NewGuid().ToString(),
                checkedIn = false,
                verified = false,
                email = mailchimpMember.emailAddress.Trim(),
                emailLower = mailchimpMember.emailAddress.ToLower().Trim(),
                firstName = mailchimpMember.mergeFields.fname.Trim(),
                lastName = mailchimpMember.mergeFields.lname.Trim(),
                lastNameLower = mailchimpMember.mergeFields.lname.ToLower().Trim(),
                mailchimpMemberId = mailchimpMember.id,
                mailchimpMemberInfo = mailchimpMember,
                phone = mailchimpMember.mergeFields.phone.Trim(),
                lastUpdated = DateTime.Now.ToString(),
                driversLicense = null
            };
            try
            {
                _ = await _collection.AddAsync(firestoreMember);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public async Task CreateMultipleMembersAsync(IEnumerable<MailchimpMember> mailchimpMemberList)
        {
            _logger.LogInformation($"Creating {mailchimpMemberList.Count()} members.");
            foreach (var mailchimpMember in mailchimpMemberList)
            {
                await CreateMemberAsync(mailchimpMember);
            }
        }

        public async Task<List<FirestoreMember>> GetAllFirestoreMembersAsync()
        {
            try
            {
                _logger.LogInformation($"Getting all firestore members from collection...");
                var firestoreMemberList = new List<FirestoreMember>();
                var docs = _collection.ListDocumentsAsync();
                var docRefList = await docs.ToList();
                _logger.LogInformation($"Got docRefList from collection.");
                foreach (var docRef in docRefList)
                {
                    try
                    {
                        var snapshot = await docRef.GetSnapshotAsync();
                        var firestoreMember = snapshot.ConvertTo<FirestoreMember>();
                        firestoreMemberList.Add(firestoreMember);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Failed to convert to firestore member.");
                        _logger.LogError(JsonConvert.SerializeObject(ex));
                        continue;
                    }
                }
                _logger.LogInformation("Finished getting all firestore members.");
                return firestoreMemberList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong getting all firestore members from collection: {JsonConvert.SerializeObject(ex)}");
                return null;
            }
            
        }

        public async Task<FirestoreMember> GetMemberByEmailAndPhoneAsync(string email, string phone)
        {
            var query = _collection.WhereEqualTo("Email", email);
            var querySnapshot = await query.GetSnapshotAsync();
            var foundFirestoreMembers = new List<FirestoreMember>();
            foreach (var queryResult in querySnapshot.Documents)
            {
                try
                {
                    var firestoreMember = queryResult.ConvertTo<FirestoreMember>();
                    foundFirestoreMembers.Add(firestoreMember);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            var member = foundFirestoreMembers.FirstOrDefault(m => m.phone == phone);
            return member;
        }

        public async Task<FirestoreMember> GetMemberByIdAsync(string id)
        {
            var doc = _collection.Document(id);
            var docSnapshot = await doc.GetSnapshotAsync();
            var firestoreMember = docSnapshot.ConvertTo<FirestoreMember>();

            return firestoreMember;
        }

        public async Task RemoveMailchimpInfoForMultipleMembersAsync(List<string> membersToRemoveMailchimpInfo)
        {
            _logger.LogInformation($"Removing {membersToRemoveMailchimpInfo.Count()} members.");
            foreach (var memberToRemove in membersToRemoveMailchimpInfo)
            {
                var query = _collection.WhereEqualTo("MailchimpMemberId", memberToRemove);
                var querySnapshot = await query.GetSnapshotAsync();
                var docId = querySnapshot.Documents.FirstOrDefault().Id;
                var firestoreMember = querySnapshot.Documents.FirstOrDefault().ConvertTo<FirestoreMember>();
                firestoreMember.mailchimpMemberInfo = null;
                var docRef = _collection.Document(docId);
                await docRef.SetAsync(firestoreMember, SetOptions.Overwrite);
            }
        }

        public async Task UpdateMultipleMembersAsync(List<MailchimpMember> membersToUpdate)
        {
            _logger.LogInformation($"Updating {membersToUpdate.Count()} members.");
            foreach (var memberToUpdate in membersToUpdate)
            {
                var query = _collection.WhereEqualTo("MailchimpMemberId", memberToUpdate.id);
                var querySnapshot = await query.GetSnapshotAsync();
                var docId = querySnapshot.Documents.FirstOrDefault().Id;
                var firestoreMember = querySnapshot.Documents.FirstOrDefault().ConvertTo<FirestoreMember>();
                firestoreMember.mailchimpMemberInfo = memberToUpdate;
                var docRef = _collection.Document(docId);
                await docRef.SetAsync(firestoreMember, SetOptions.Overwrite);
            }
        }
    }
}
