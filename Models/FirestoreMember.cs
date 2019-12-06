using Google.Cloud.Firestore;

namespace mailchimp_firebase_sync.Models
{
    [FirestoreData]
    public class FirestoreMember
    {
        [FirestoreProperty]
        public string id { get; set; }
        [FirestoreProperty]
        public string mailchimpMemberId { get; set; }
        [FirestoreProperty]
        public string email { get; set; }
        [FirestoreProperty]
        public string emailLower { get; set; }
        [FirestoreProperty]
        public string lastUpdated { get; set; }
        [FirestoreProperty]
        public string firstName { get; set; }
        [FirestoreProperty]
        public string lastName { get; set; }
        [FirestoreProperty]
        public string lastNameLower { get; set; }
        [FirestoreProperty]
        public string phone { get; set; }
        [FirestoreProperty]
        public DriversLicense driversLicense { get; set; }
        [FirestoreProperty]
        public bool checkedIn { get; set; }
        [FirestoreProperty]
        public bool verified { get; set; }
        [FirestoreProperty]
        public MailchimpMember mailchimpMemberInfo { get; set; }
    }
}
