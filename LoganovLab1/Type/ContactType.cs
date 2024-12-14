namespace LoganovLab1.Type
{
    // Тип контакта
    public class ContactType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ContactType(string name, string description = "")
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Name} ({Description})";
        }

        public void DeepPrint()
        {
            Console.WriteLine("    ContactType:");
            Console.WriteLine($"      Name: {Name}");
            Console.WriteLine($"      Description: {Description}");
        }
    }
}
