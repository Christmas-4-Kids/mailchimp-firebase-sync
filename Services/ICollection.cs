namespace mailchimp_firebase_sync.Services
{
    public interface ICollection
    {
        string GetName();
        void SetName(string name);
    }
}