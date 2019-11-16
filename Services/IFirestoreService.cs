using System.Collections.Generic;
using System.Threading.Tasks;
using mailchimp_firebase_sync.Models;

namespace mailchimp_firebase_sync.Services
{
    public interface IFirestoreService
    {
        Task CreateMemberAsync(MailchimpMember mailchimpMember);
        Task CreateMultipleMembersAsync(IEnumerable<MailchimpMember> mailchimpMemberList);
        Task<List<FirestoreMember>> GetAllFirestoreMembersAsync();
        Task<FirestoreMember> GetMemberByEmailAndPhoneAsync(string email, string phone);
        Task UpdateMultipleMembersAsync(List<MailchimpMember> membersToUpdate);
        Task RemoveMailchimpInfoForMultipleMembersAsync(List<string> membersToRemoveMailchimpInfo);
    }
}