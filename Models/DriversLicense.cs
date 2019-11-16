using Google.Cloud.Firestore;

namespace mailchimp_firebase_sync.Models
{
    [FirestoreData]
    public class DriversLicense
    {
        [FirestoreProperty]
        public string Number { get; set; }
        [FirestoreProperty]
        public string FirstName { get; set; }
        [FirestoreProperty]
        public string LastName { get; set; }
        [FirestoreProperty]
        public Address Address { get; set; }
        [FirestoreProperty]
        public string DateOfBirth { get; set; }
        [FirestoreProperty]
        public string ExpirationDate { get; set; }
        [FirestoreProperty]
        public bool IsValid { get; set; }
        [FirestoreProperty]
        public string Gender { get; set; }
        [FirestoreProperty]
        public string Height { get; set; }
        [FirestoreProperty]
        public string EyeColor { get; set; }
        [FirestoreProperty]
        public string IssueDate { get; set; }
    }
}
