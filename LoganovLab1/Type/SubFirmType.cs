﻿namespace LoganovLab1.Type
{
    // Тип подразделения фирмы
    public class SubFirmType
    {
        public string Name { get; set; }
        public bool IsMainOffice { get; set; }

        public SubFirmType(string name, bool isMainOffice = false)
        {
            Name = name;
            IsMainOffice = isMainOffice;
        }

        public override string ToString()
        {
            return Name + (IsMainOffice ? " (IsMainOffice)" : "");
        }
    }
}
