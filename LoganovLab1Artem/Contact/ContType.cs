using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab1Artem.ContactSpace
{
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
            return Name + (string.IsNullOrEmpty(Description) ? "" : " (" + Description + ")");
        }
    }
}
