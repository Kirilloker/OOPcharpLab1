using LoganovLab1.Type;

namespace LoganovLab1.Domain
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

        public void DeepPrint()
        {
            Console.WriteLine("  SubFirm:");
            Console.WriteLine($"    Name: {Name}");
            Console.WriteLine($"    SubFirmType: {SubFirmType}");
            Console.WriteLine($"    ContactPerson: {ContactPerson}");
            Console.WriteLine($"    ContactPhone: {ContactPhone}");
            Console.WriteLine($"    ContactEmail: {ContactEmail}");

            Console.WriteLine("    Contacts:");
            foreach (var contact in _contacts)
            {
                contact.DeepPrint();
            }
        }

    }
}
