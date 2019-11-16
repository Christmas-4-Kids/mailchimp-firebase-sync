using Google.Cloud.Firestore;
using mailchimp_firebase_sync.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace mailchimp_firebase_sync.Models
{
    [FirestoreData]
    public class MailchimpMember 
    {
        [FirestoreProperty]
        public string Id { get; set; }
        [FirestoreProperty]
        public string EmailAddress { get; set; }
        [FirestoreProperty]
        public string UniqueEmailId { get; set; }
        [FirestoreProperty]
        public int WebId { get; set; }
        [FirestoreProperty]
        public string EmailType { get; set; }
        [FirestoreProperty]
        public string Status { get; set; }
        [FirestoreProperty]
        public MailchimpMergeFields MergeFields { get; set; }
        [FirestoreProperty]
        public string TimestampSignup { get; set; }
        [FirestoreProperty]
        public string IpOpt { get; set; }
        [FirestoreProperty]
        public string TimestampOpt { get; set; }
        [FirestoreProperty]
        public string MemberRating { get; set; }
        [FirestoreProperty]
        public string LastChanged { get; set; }
        [FirestoreProperty]
        public string Language { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool Vip { get; set; }
        [FirestoreProperty]
        public string EmailClient { get; set; }
        [FirestoreProperty]
        public string ListId { get; set; }
    }

    public class MailchimpMembers
    {
        public IEnumerable<MailchimpMember> Members { get; set; }
        public string ListId { get; set; }
        public int TotalItems { get; set; }
    }
}
