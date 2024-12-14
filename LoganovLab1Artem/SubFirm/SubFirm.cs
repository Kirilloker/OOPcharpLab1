using LoganovLab1Artem.ContactSpace;

namespace LoganovLab1Artem.SubFirmSpace
{
    public class SubFirm
    {
        public string Name { get; set; }
        public SubFirmType SubFirmType { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        private List<Contact> _contacts;

        public SubFirm(SubFirmType type, string name = "")
        {
            SubFirmType = type;
            Name = string.IsNullOrEmpty(name) ? type?.Name : name;
            _contacts = new List<Contact>();
        }

        public void AddContact(Contact contact)
        {
            if (contact != null)
            {
                _contacts.Add(contact);
            }
        }

        public bool RemoveContact(Contact contact)
        {
            if (contact == null)
            {
                return false;
            }
            return _contacts.Remove(contact);
        }

        public Contact[] GetContacts()
        {
            return _contacts.ToArray();
        }

        public override string ToString()
        {
            return Name + ", Type: " + (SubFirmType != null ? SubFirmType.Name : "");
        }
    }
}
