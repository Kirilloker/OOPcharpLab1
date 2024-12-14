using System.Collections.Generic;
using LoganovLab1Artem.ContactSpace;

namespace LoganovLab1Artem.SubFirmSpace
{
    public class SubFirm
    {
        private string _bossName;
        private List<Contact> _conts;
        private string _email;
        private string _name;
        private string _ofcBossName;
        private string _tel;
        private SbFirmType _tpy;

        public string BossName
        {
            get => _bossName;
            set => _bossName = value;
        }

        public int CountCont => _conts.Count;

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public bool IsMain => _tpy?.IsMain ?? false;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string OfcBossName
        {
            get => _ofcBossName;
            set => _ofcBossName = value;
        }

        public SbFirmType SbFirmTpy
        {
            get => _tpy;
            set => _tpy = value;
        }

        public string Tel
        {
            get => _tel;
            set => _tel = value;
        }

        public SubFirm(SbFirmType type, string name = "")
        {
            _tpy = type;
            _name = string.IsNullOrEmpty(name) ? type?.Name : name;
            _conts = new List<Contact>();
        }

        public void AddCont(Contact contact)
        {
            if (contact != null)
            {
                _conts.Add(contact);
            }
        }

        public bool ExistContact(Contact contact)
        {
            return _conts.Contains(contact);
        }

        public bool IsYourType(SbFirmType type)
        {
            return _tpy == type;
        }

        public Contact[] GetContacts()
        {
            return _conts.ToArray();
        }

        public override string ToString()
        {
            return $"{Name}, Type: {(_tpy != null ? _tpy.Name : "Undefined")}";
        }
    }
}
