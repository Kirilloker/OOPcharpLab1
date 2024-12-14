namespace LoganovLab1
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
    }
}
