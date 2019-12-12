namespace mailchimp_firebase_sync.Services
{
    public class Collection : ICollection
    {
        private string _name;

        public void SetName(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }
}
