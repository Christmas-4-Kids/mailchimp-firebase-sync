using Google.Cloud.Firestore;
using mailchimp_firebase_sync.Converters;
using Newtonsoft.Json;

namespace mailchimp_firebase_sync.Models
{
    [FirestoreData]
    public class MailchimpMergeFields
    {
        [FirestoreProperty]
        public string Fname { get; set; }
        [FirestoreProperty]
        public string Lname { get; set; }
        [FirestoreProperty]
        public Address Address { get; set; }
        [FirestoreProperty]
        public string Phone { get; set; }
        [FirestoreProperty]
        public string Driver { get; set; }
        [FirestoreProperty]
        public string Medical { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool Espanol { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool Prevexp { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool Bgcheck { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool Idcheck { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool Over18 { get; set; }
        [FirestoreProperty]
        public string Comments { get; set; }
    }
}