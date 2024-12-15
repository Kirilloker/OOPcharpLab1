﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Expressions.ConcreteExpressions
{
    public class ExpLT : ILogExp
    {
        private IComparable _val;
        public ExpLT(object val) { _val = val as IComparable; }
        public bool Compare(object fieldValue)
        {
            if (fieldValue is IComparable cmp)
                return cmp.CompareTo(_val) < 0;
            return false;
        }
    }
}