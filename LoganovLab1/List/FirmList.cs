using LoganovLab1.Domain;
using LoganovLab1.Type;

namespace LoganovLab1.List
{
    // Список фирм с методами фильтрации
    public class FirmList
    {
        private List<Firm> _firms = new List<Firm>();

        public FirmList() { }

        public FirmList(IEnumerable<Firm> firms)
        {
            if (firms != null) _firms.AddRange(firms);
        }

        public void AddFirm(Firm firm)
        {
            if (firm != null) _firms.Add(firm);
        }

        public FirmList FilterByRegion(string region)
        {
            var filtered = _firms.Where(f => f.Region == region).ToList();
            return new FirmList(filtered);
        }

        public FirmList FilterBySubFirmType(string subFirmTypeName)
        {
            var filtered = _firms.Where(f => f.GetAllSubFirms().Any(sf => sf.SubFirmType != null && sf.SubFirmType.Name == subFirmTypeName)).ToList();
            return new FirmList(filtered);
        }

        public FirmList FilterByContactType(ContType contactType, DateTime? start = null, DateTime? end = null)
        {
            var filtered = _firms.Where(f =>
            {
                var contacts = f.GetAllContacts();
                return contacts.Any(c =>
                    c.ContactType == contactType &&
                    (start == null || c.Date >= start) &&
                    (end == null || c.Date <= end));
            }).ToList();

            return new FirmList(filtered);
        }

        public void AddContactToAllFirms(Contact prototype)
        {
            foreach (var firm in _firms)
            {
                var c = prototype.Clone();
                firm.AddContactToMainOffice(c);
            }
        }

        public void AddContactToAllFirmsWithSubFirmType(Contact prototype, string subFirmTypeName, bool addToFirmsWithoutSuchSubFirm = false)
        {
            foreach (var firm in _firms)
            {
                var targetSubs = firm.GetAllSubFirms().Where(sf => sf.SubFirmType != null && sf.SubFirmType.Name == subFirmTypeName).ToArray();

                if (targetSubs.Length > 0)
                {
                    // Добавляем контакт в эти подразделения
                    foreach (var tsf in targetSubs)
                    {
                        tsf.AddContact(prototype.Clone());
                    }
                }
                else
                {
                    if (addToFirmsWithoutSuchSubFirm)
                    {
                        // Добавляем контакт в основной офис
                        firm.AddContactToMainOffice(prototype.Clone());
                    }
                }
            }
        }

        public Firm[] ToArray()
        {
            return _firms.ToArray();
        }

        public void DeepPrint()
        {
            Console.WriteLine("FirmList:");
            foreach (var firm in _firms)
            {
                firm.DeepPrint();
            }
            Console.WriteLine("\n\n\n\n");
        }

    }

}
