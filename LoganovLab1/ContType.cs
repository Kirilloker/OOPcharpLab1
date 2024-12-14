namespace LoganovLab1
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
    }
}
