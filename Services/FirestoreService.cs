using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using mailchimp_firebase_sync.Models;
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

        public FirestoreService()
        {
            try
            {
                var firestoreClientBuilder = new FirestoreClientBuilder
                {
                    CredentialsPath = @$"{Directory.GetCurrentDirectory()}\c4k-events-credentials.json"
                };
                _db = FirestoreDb.Create("c4k-events", firestoreClientBuilder.Build());
                _collection = _db.Collection("members");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public async Task CreateMemberAsync(MailchimpMember mailchimpMember)
        {
            var firestoreMember = new FirestoreMember()
            {
                Id = Guid.NewGuid().ToString(),
                CheckedIn = false,
                Verified = false,
                Email = mailchimpMember.EmailAddress,
                FirstName = mailchimpMember.MergeFields.Fname,
                LastName = mailchimpMember.MergeFields.Lname,
                MailchimpMemberId = mailchimpMember.Id,
                MailchimpMemberInfo = mailchimpMember,
                Phone = mailchimpMember.MergeFields.Phone,
                LastUpdated = DateTime.Now.ToString(),
                DriversLicense = null
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
            foreach (var mailchimpMember in mailchimpMemberList)
            {
                await CreateMemberAsync(mailchimpMember);
            }
        }

        public async Task<List<FirestoreMember>> GetAllFirestoreMembersAsync()
        {
            var firestoreMemberList = new List<FirestoreMember>();
            var docs = _collection.ListDocumentsAsync();
            var docRefList = await docs.ToList();
            foreach (var docRef in docRefList)
            {
                var snapshot = await docRef.GetSnapshotAsync();
                var firestoreMember = snapshot.ConvertTo<FirestoreMember>();
                firestoreMemberList.Add(firestoreMember);
            }
            return firestoreMemberList;
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
            var member = foundFirestoreMembers.FirstOrDefault(m => m.Phone == phone);
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
            foreach (var memberToRemove in membersToRemoveMailchimpInfo)
            {
                var query = _collection.WhereEqualTo("MailchimpMemberId", memberToRemove);
                var querySnapshot = await query.GetSnapshotAsync();
                var firestoreMember = querySnapshot.Documents.FirstOrDefault().ConvertTo<FirestoreMember>();
                firestoreMember.MailchimpMemberInfo = null;
                var docRef = _collection.Document(firestoreMember.Id);
                await docRef.SetAsync(firestoreMember, SetOptions.Overwrite);
            }
        }

        public async Task UpdateMultipleMembersAsync(List<MailchimpMember> membersToUpdate)
        {
            foreach (var memberToUpdate in membersToUpdate)
            {
                var query = _collection.WhereEqualTo("MailchimpMemberId", memberToUpdate.Id);
                var querySnapshot = await query.GetSnapshotAsync();
                var firestoreMember = querySnapshot.Documents.FirstOrDefault().ConvertTo<FirestoreMember>();
                firestoreMember.MailchimpMemberInfo = memberToUpdate;
                var docRef = _collection.Document(firestoreMember.Id);
                await docRef.SetAsync(firestoreMember, SetOptions.Overwrite);
            }
        }
    }
}
