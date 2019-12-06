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
        public string id { get; set; }
        [FirestoreProperty]
        public string emailAddress { get; set; }
        [FirestoreProperty]
        public string uniqueEmailId { get; set; }
        [FirestoreProperty]
        public int webId { get; set; }
        [FirestoreProperty]
        public string emailType { get; set; }
        [FirestoreProperty]
        public string status { get; set; }
        [FirestoreProperty]
        public MailchimpMergeFields mergeFields { get; set; }
        [FirestoreProperty]
        public string timestampSignup { get; set; }
        [FirestoreProperty]
        public string ipOpt { get; set; }
        [FirestoreProperty]
        public string timestampOpt { get; set; }
        [FirestoreProperty]
        public string memberRating { get; set; }
        [FirestoreProperty]
        public string lastChanged { get; set; }
        [FirestoreProperty]
        public string language { get; set; }
        [FirestoreProperty]
        [JsonConverter(typeof(BooleanJsonConverter))]
        public bool vip { get; set; }
        [FirestoreProperty]
        public string emailClient { get; set; }
        [FirestoreProperty]
        public string listId { get; set; }
    }

    public class MailchimpMembers
    {
        public IEnumerable<MailchimpMember> members { get; set; }
        public string listId { get; set; }
        public int totalItems { get; set; }
    }
}
