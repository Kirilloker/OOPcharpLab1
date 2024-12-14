namespace LoganovLab1.Type
{
    // Тип контакта
    public class ContType
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ContType(string name, string description = "")
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
            Console.WriteLine("    ContType:");
            Console.WriteLine($"      Name: {Name}");
            Console.WriteLine($"      Description: {Description}");
        }

    }
}
