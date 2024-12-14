using LoganovLab1.Type;

namespace LoganovLab1.Domain
{

    // Контакт
    public class Contact
    {
        public ContactType ContactType { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

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
                // Считаем контакты равными, если совпадает тип, дата и описание.
                return ContactType == other.ContactType &&
                       Date == other.Date &&
                       Description == other.Description;
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

        public void DeepPrint()
        {
            Console.WriteLine("    Contact:");
            Console.WriteLine($"      ContactType: {ContactType}");
            Console.WriteLine($"      Date: {Date}");
            Console.WriteLine($"      Description: {Description}");
        }

    }
}
