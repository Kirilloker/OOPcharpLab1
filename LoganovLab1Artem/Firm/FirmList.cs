using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.SubFirmSpace;

namespace LoganovLab1Artem.FirmSpace
{
    public class FirmList
    {
        private List<Firm> _firms;

        public FirmList()
        {
            _firms = new List<Firm>();
        }

        public FirmList(IEnumerable<Firm> firms)
        {
            _firms = new List<Firm>();
            if (firms != null)
            {
                foreach (Firm firm in firms)
                {
                    _firms.Add(firm);
                }
            }
        }

        public void AddFirm(Firm firm)
        {
            if (firm != null)
            {
                _firms.Add(firm);
            }
        }

        public FirmList FilterByRegion(string region)
        {
            List<Firm> filtered = new List<Firm>();
            foreach (Firm firm in _firms)
            {
                if (firm.Region == region)
                {
                    filtered.Add(firm);
                }
            }
            return new FirmList(filtered);
        }

        public FirmList FilterBySubFirmType(string subFirmTypeName)
        {
            List<Firm> filtered = new List<Firm>();
            foreach (Firm firm in _firms)
            {
                foreach (SubFirm subFirm in firm.GetAllSubFirms())
                {
                    if (subFirm.SubFirmType != null && subFirm.SubFirmType.Name == subFirmTypeName)
                    {
                        filtered.Add(firm);
                        break;
                    }
                }
            }
            return new FirmList(filtered);
        }

        public FirmList FilterByContactType(ContType contactType, DateTime? start = null, DateTime? end = null)
        {
            List<Firm> filtered = new List<Firm>();
            foreach (Firm firm in _firms)
            {
                foreach (Contact contact in firm.GetAllContacts())
                {
                    if (contact.ContactType == contactType &&
                        (start == null || contact.Date >= start) &&
                        (end == null || contact.Date <= end))
                    {
                        filtered.Add(firm);
                        break;
                    }
                }
            }
            return new FirmList(filtered);
        }

        public void AddContactToAllFirms(Contact prototype)
        {
            foreach (Firm firm in _firms)
            {
                Contact clone = prototype.Clone();
                firm.AddContactToMainOffice(clone);
            }
        }

        public void AddContactToAllFirmsWithSubFirmType(Contact prototype, string subFirmTypeName, bool addToFirmsWithoutSuchSubFirm = false)
        {
            foreach (Firm firm in _firms)
            {
                bool subFirmFound = false;
                foreach (SubFirm subFirm in firm.GetAllSubFirms())
                {
                    if (subFirm.SubFirmType != null && subFirm.SubFirmType.Name == subFirmTypeName)
                    {
                        subFirm.AddContact(prototype.Clone());
                        subFirmFound = true;
                    }
                }
                if (!subFirmFound && addToFirmsWithoutSuchSubFirm)
                {
                    firm.AddContactToMainOffice(prototype.Clone());
                }
            }
        }

        public Firm[] ToArray()
        {
            return _firms.ToArray();
        }
    }
}
