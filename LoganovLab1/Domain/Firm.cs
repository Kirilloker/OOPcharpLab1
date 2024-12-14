namespace LoganovLab1.Domain
{
    // Фирма
    public class Firm
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string CEO { get; set; }
        public DateTime? InsertDate { get; set; }
        public string PhoneCEO { get; set; }
        public string PhoneContact { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        // Словарь пользовательских полей. Ключ - имя поля, значение - данные
        private Dictionary<string, string> _userFields;

        private List<SubFirm> _subFirms = new List<SubFirm>();

        public Firm(string[] userFieldNames)
        {
            _userFields = new Dictionary<string, string>();
            foreach (var name in userFieldNames)
            {
                _userFields[name] = string.Empty;
            }
        }

        public void AddSubFirm(SubFirm subFirm)
        {
            if (subFirm != null) _subFirms.Add(subFirm);
        }

        public bool RemoveSubFirm(SubFirm subFirm)
        {
            return _subFirms.Remove(subFirm);
        }

        public SubFirm[] GetAllSubFirms()
        {
            return _subFirms.ToArray();
        }

        public SubFirm GetMainOffice()
        {
            return _subFirms.FirstOrDefault(sf => sf.SubFirmType != null && sf.SubFirmType.IsMainOffice);
        }

        public void SetUserFieldValue(string fieldName, string value)
        {
            if (_userFields.ContainsKey(fieldName))
                _userFields[fieldName] = value;
        }

        public string GetUserFieldValue(string fieldName)
        {
            if (_userFields.ContainsKey(fieldName))
                return _userFields[fieldName];
            return null;
        }

        public void AddContactToMainOffice(Contact contact)
        {
            var main = GetMainOffice();
            if (main != null) main.AddContact(contact);
        }

        public Contact[] GetAllContacts()
        {
            return _subFirms.SelectMany(sf => sf.GetContacts()).ToArray();
        }

        public override string ToString()
        {
            return $"{FullName} ({ShortName}), Region: {Region}, City: {City}";
        }

        public void DeepPrint()
        {
            Console.WriteLine("Firm:");
            Console.WriteLine($"  FullName: {FullName}");
            Console.WriteLine($"  ShortName: {ShortName}");
            Console.WriteLine($"  Region: {Region}");
            Console.WriteLine($"  City: {City}");
            Console.WriteLine($"  Address: {Address}");
            Console.WriteLine($"  CEO: {CEO}");
            Console.WriteLine($"  InsertDate: {InsertDate}");
            Console.WriteLine($"  PhoneCEO: {PhoneCEO}");
            Console.WriteLine($"  PhoneContact: {PhoneContact}");
            Console.WriteLine($"  Fax: {Fax}");
            Console.WriteLine($"  Email: {Email}");
            Console.WriteLine($"  Website: {Website}");
            Console.WriteLine("  User Fields:");
            foreach (var field in _userFields)
            {
                Console.WriteLine($"    {field.Key}: {field.Value}");
            }

            Console.WriteLine("  SubFirms:");
            foreach (var subFirm in _subFirms)
            {
                subFirm.DeepPrint();
            }
        }
    }
}
