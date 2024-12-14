using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.SubFirmSpace;
using System.Collections.Generic;

namespace LoganovLab1Artem.FirmSpace
{
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

        private Dictionary<string, string> _userFields;

        private List<SubFirm> _subFirms;

        public Firm(string[] userFieldNames)
        {
            _userFields = new Dictionary<string, string>();
            foreach (string name in userFieldNames)
            {
                _userFields[name] = string.Empty;
            }
            _subFirms = new List<SubFirm>();
        }

        public void AddSubFirm(SubFirm subFirm)
        {
            if (subFirm != null)
            {
                _subFirms.Add(subFirm);
            }
        }

        public bool RemoveSubFirm(SubFirm subFirm)
        {
            if (subFirm == null)
            {
                return false;
            }
            return _subFirms.Remove(subFirm);
        }

        public SubFirm[] GetAllSubFirms()
        {
            return _subFirms.ToArray();
        }

        public SubFirm GetMainOffice()
        {
            foreach (SubFirm sf in _subFirms)
            {
                if (sf.SubFirmType != null && sf.SubFirmType.IsMainOffice)
                {
                    return sf;
                }
            }
            return null;
        }

        public void SetUserFieldValue(string fieldName, string value)
        {
            if (_userFields.ContainsKey(fieldName))
            {
                _userFields[fieldName] = value;
            }
        }

        public string GetUserFieldValue(string fieldName)
        {
            if (_userFields.ContainsKey(fieldName))
            {
                return _userFields[fieldName];
            }
            return null;
        }

        public void AddContactToMainOffice(Contact contact)
        {
            SubFirm mainOffice = GetMainOffice();
            if (mainOffice != null)
            {
                mainOffice.AddContact(contact);
            }
        }

        public Contact[] GetAllContacts()
        {
            List<Contact> allContacts = new List<Contact>();
            foreach (SubFirm subFirm in _subFirms)
            {
                allContacts.AddRange(subFirm.GetContacts());
            }
            return allContacts.ToArray();
        }

        public override string ToString()
        {
            return FullName + " (" + ShortName + "), Region: " + Region + ", City: " + City;
        }
    }
}