using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.SubFirmSpace;

namespace LoganovLab1Artem.FirmSpace
{
    public class List
    {
        private List<Firm> _firms;

        public List()
        {
            _firms = new List<Firm>();
        }

        public List(IEnumerable<Firm> firms)
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

        public List FilterByRegion(string region)
        {
            List<Firm> filtered = new List<Firm>();
            foreach (Firm firm in _firms)
            {
                if (firm.Region == region)
                {
                    filtered.Add(firm);
                }
            }
            return new List(filtered);
        }

        public List FilterBySubFirmType(string subFirmTypeName)
        {
            List<Firm> filtered = new List<Firm>();
            foreach (Firm firm in _firms)
            {
                foreach (SubFirm subFirm in firm.GetAllSubFirms())
                {
                    if (subFirm.SbFirmTpy != null && subFirm.SbFirmTpy.Name == subFirmTypeName)
                    {
                        filtered.Add(firm);
                        break;
                    }
                }
            }
            return new List(filtered);
        }

        public List FilterByContactType(ContType contactType, DateTime? start = null, DateTime? end = null)
        {
            List<Firm> filtered = new List<Firm>();
            foreach (Firm firm in _firms)
            {
                foreach (Contact contact in firm.GetAllContacts())
                {
                    if (contact.CntType == contactType &&
                        (start == null || contact.BeginDt >= start) &&
                        (end == null || contact.EndDt <= end))
                    {
                        filtered.Add(firm);
                        break;
                    }
                }
            }
            return new List(filtered);
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
                    if (subFirm.SbFirmTpy != null && subFirm.SbFirmTpy.Name == subFirmTypeName)
                    {
                        subFirm.AddCont(prototype.Clone());
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
