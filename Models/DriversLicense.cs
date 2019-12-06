using Google.Cloud.Firestore;

namespace mailchimp_firebase_sync.Models
{
    [FirestoreData]
    public class DriversLicense
    {
        [FirestoreProperty]
        public string number { get; set; }
        [FirestoreProperty]
        public string firstName { get; set; }
        [FirestoreProperty]
        public string lastName { get; set; }
        [FirestoreProperty]
        public string address { get; set; }
        [FirestoreProperty]
        public string dateOfBirth { get; set; }
        [FirestoreProperty]
        public string expirationDate { get; set; }
        [FirestoreProperty]
        public bool isValid { get; set; }
        [FirestoreProperty]
        public string gender { get; set; }
        [FirestoreProperty]
        public string height { get; set; }
        [FirestoreProperty]
        public string eyeColor { get; set; }
        [FirestoreProperty]
        public string issueDate { get; set; }
    }
}
