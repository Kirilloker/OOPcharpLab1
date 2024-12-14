using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab1Artem.ContactSpace
{
    public class Contact
    {
        public ContType ContactType { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public Contact Clone()
        {
            return new Contact
            {
                ContactType = ContactType,
                Date = Date,
                Description = Description
            };
        }

        public override bool Equals(object obj)
        {
            if (obj is Contact other)
            {
                return ContactType == other.ContactType &&
                       Description == other.Description &&
                       Date == other.Date;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + (ContactType?.Name.GetHashCode() ?? 0);
            hash = hash * 23 + Date.GetHashCode();
            hash = hash * 23 + (Description?.GetHashCode() ?? 0);
            return hash;
        }
    }
}
