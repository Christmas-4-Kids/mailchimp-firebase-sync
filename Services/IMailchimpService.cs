﻿using System.Threading.Tasks;
using mailchimp_firebase_sync.Models;

namespace mailchimp_firebase_sync.Services
{
    public interface IMailchimpService
    {
        Task<MailchimpMembers> GetAllDayChaperones();
    }
}