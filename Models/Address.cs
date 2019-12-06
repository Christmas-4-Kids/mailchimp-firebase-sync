using Google.Cloud.Firestore;

namespace mailchimp_firebase_sync.Models
{
    [FirestoreData]
    public class Address
    {
        [FirestoreProperty]
        public string addr1 { get; set; }
        [FirestoreProperty]
        public string addr2 { get; set; }
        [FirestoreProperty]
        public string city { get; set; }
        [FirestoreProperty]
        public string state { get; set; }
        [FirestoreProperty]
        public string zip { get; set; }
        [FirestoreProperty]
        public string country { get; set; }
    }
}