using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Expressions
{
    public interface ILogExp
    {
        bool Compare(object fieldValue);
    }
}
