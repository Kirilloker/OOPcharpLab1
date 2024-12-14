using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.SubFirmSpace;

namespace LoganovLab1Artem.FirmSpace
{
    public class Firm
    {
        private string _country;
        private DateTime? _dateIn;
        private string _email;
        private string _name;
        private string _postInx;
        private string _region;
        private List<SubFirm> _sbFirms;
        private string _street;
        private string _town;
        private Dictionary<string, string> _usrFields;
        private string _web;

        public string Country
        {
            get => _country;
            set => _country = value;
        }

        public DateTime? DateIn
        {
            get => _dateIn;
            set => _dateIn = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string PostInx
        {
            get => _postInx;
            set => _postInx = value;
        }

        public string Region
        {
            get => _region;
            set => _region = value;
        }

        public int SbFirmsCount => _sbFirms.Count;

        public string Street
        {
            get => _street;
            set => _street = value;
        }

        public string Town
        {
            get => _town;
            set => _town = value;
        }

        public string Web
        {
            get => _web;
            set => _web = value;
        }

        public string this[string fieldName]
        {
            get => _usrFields.ContainsKey(fieldName) ? _usrFields[fieldName] : null;
            set
            {
                if (_usrFields.ContainsKey(fieldName))
                {
                    _usrFields[fieldName] = value;
                }
            }
        }

        public Firm(string[] userFieldNames)
        {
            _usrFields = new Dictionary<string, string>();
            foreach (string field in userFieldNames)
            {
                _usrFields[field] = string.Empty;
            }
            _sbFirms = new List<SubFirm>();
        }

        public void AddCont(Contact contact)
        {
            if (_sbFirms.Count > 0)
            {
                _sbFirms[0].AddCont(contact);
            }
        }

        public void AddContToSbFirm(SubFirm subFirm, Contact contact)
        {
            if (subFirm != null && contact != null && _sbFirms.Contains(subFirm))
            {
                subFirm.AddCont(contact);
            }
        }

        public void AddSbFirm(SubFirm subFirm)
        {
            if (subFirm != null)
            {
                _sbFirms.Add(subFirm);
            }
        }

        public SubFirm[] GetAllSubFirms()
        {
            return _sbFirms.ToArray();
        }

        public Contact[] GetAllContacts()
        {
            var allContacts = new List<Contact>();
            foreach (var subFirm in _sbFirms)
            {
                allContacts.AddRange(subFirm.GetContacts());
            }
            return allContacts.ToArray();
        }

        public void AddContactToMainOffice(Contact contact)
        {
            var mainOffice = _sbFirms.FirstOrDefault(sf => sf.IsMain);
            if (mainOffice == null && _sbFirms.Count > 0)
            {
                mainOffice = _sbFirms[0]; 
            }
            if (mainOffice != null)
            {
                mainOffice.AddCont(contact);
            }
        }


        public void AddField(string fieldName)
        {
            if (!_usrFields.ContainsKey(fieldName))
            {
                _usrFields[fieldName] = string.Empty;
            }
        }

        public void SetField(string fieldName, string value)
        {
            if (_usrFields.ContainsKey(fieldName))
            {
                _usrFields[fieldName] = value;
            }
        }

        public void RenameField(string oldName, string newName)
        {
            if (_usrFields.ContainsKey(oldName) && !_usrFields.ContainsKey(newName))
            {
                string value = _usrFields[oldName];
                _usrFields.Remove(oldName);
                _usrFields[newName] = value;
            }
        }

        public string GetField(string fieldName)
        {
            return _usrFields.ContainsKey(fieldName) ? _usrFields[fieldName] : null;
        }

        public override string ToString()
        {
            return $"{Name}, {Country}, {Region}, {Town}, {Street}";
        }
    }
}
