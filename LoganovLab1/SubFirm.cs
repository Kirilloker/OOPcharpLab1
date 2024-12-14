namespace LoganovLab1
{
    // Подразделение фирмы
    public class SubFirm
    {
        public string Name { get; set; }
        public SbFirmType SubFirmType { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        private List<Contact> _contacts = new List<Contact>();

        public SubFirm(SbFirmType type, string name = "")
        {
            SubFirmType = type;
            Name = string.IsNullOrEmpty(name) ? type?.Name : name;
        }

        public void AddContact(Contact contact)
        {
            if (contact != null) _contacts.Add(contact);
        }

        public bool RemoveContact(Contact contact)
        {
            return _contacts.Remove(contact);
        }

        public Contact[] GetContacts()
        {
            return _contacts.ToArray();
        }

        public override string ToString()
        {
            return $"{Name}, Type: {SubFirmType?.Name}";
        }
    }
}
