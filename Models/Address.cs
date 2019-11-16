using Google.Cloud.Firestore;

namespace mailchimp_firebase_sync.Models
{
    [FirestoreData]
    public class Address
    {
        [FirestoreProperty]
        public string Addr1 { get; set; }
        [FirestoreProperty]
        public string Addr2 { get; set; }
        [FirestoreProperty]
        public string City { get; set; }
        [FirestoreProperty]
        public string State { get; set; }
        [FirestoreProperty]
        public string Zip { get; set; }
        [FirestoreProperty]
        public string Country { get; set; }
    }
}