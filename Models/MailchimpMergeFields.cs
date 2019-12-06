using Google.Cloud.Firestore;
using mailchimp_firebase_sync.Converters;
using Newtonsoft.Json;

namespace mailchimp_firebase_sync.Models
{
    [FirestoreData]
    public class MailchimpMergeFields
    {
        [FirestoreProperty]
        public string fname { get; set; }
        [FirestoreProperty]
        public string lname { get; set; }
        [FirestoreProperty]
        public Address address { get; set; }
        [FirestoreProperty]
        public string phone { get; set; }
        [FirestoreProperty]
        public string driver { get; set; }
        [FirestoreProperty]
        public string medical { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool espanol { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool prevexp { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool bgcheck { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool idcheck { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool over18 { get; set; }
        [FirestoreProperty]
        public string comments { get; set; }
    }
}