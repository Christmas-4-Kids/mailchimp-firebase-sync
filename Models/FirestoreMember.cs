using Google.Cloud.Firestore;
using System;

namespace mailchimp_firebase_sync.Models
{
    [FirestoreData]
    public class FirestoreMember
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string MailchimpMemberId { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public string LastUpdated { get; set; }
        [FirestoreProperty]
        public string FirstName { get; set; }
        [FirestoreProperty]
        public string LastName { get; set; }
        [FirestoreProperty]
        public string Phone { get; set; }
        [FirestoreProperty]
        public DriversLicense DriversLicense { get; set; }
        [FirestoreProperty]
        public bool CheckedIn { get; set; }
        [FirestoreProperty]
        public bool Verified { get; set; }
        [FirestoreProperty]
        public MailchimpMember MailchimpMemberInfo { get; set; }
    }
}
