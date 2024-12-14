namespace LoganovLab1.Type
{
    // Тип подразделения фирмы
    public class SbFirmType
    {
        public string Name { get; set; }
        public bool IsMainOffice { get; set; }

        public SbFirmType(string name, bool isMainOffice = false)
        {
            Name = name;
            IsMainOffice = isMainOffice;
        }

        public override string ToString()
        {
            return $"{Name}" + (IsMainOffice ? " (Основной офис)" : "");
        }

        public void DeepPrint()
        {
            Console.WriteLine("    SbFirmType:");
            Console.WriteLine($"      Name: {Name}");
            Console.WriteLine($"      IsMainOffice: {IsMainOffice}");
        }

    }
}
