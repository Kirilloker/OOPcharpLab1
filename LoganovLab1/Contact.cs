namespace LoganovLab1
{

    // Контакт
    public class Contact
    {
        public ContType ContactType { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Contact Clone()
        {
            return new Contact
            {
                ContactType = this.ContactType,
                Date = this.Date,
                Description = this.Description
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
            // Для простоты: хеш по сочетанию имени типа и даты
            int hash = 17;
            hash = hash * 23 + (ContactType?.Name.GetHashCode() ?? 0);
            hash = hash * 23 + Date.GetHashCode();
            hash = hash * 23 + (Description?.GetHashCode() ?? 0);
            return hash;
        }
    }
}
