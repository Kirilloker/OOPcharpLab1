namespace LoganovLab2.Filtering
{
    public class ExpEQ : ILogExp
    {
        public bool Compare(object value, object condition)
        {
            if (value == null && condition == null) return true;
            if (value == null || condition == null) return false;
            return value.Equals(condition);
        }
    }

    public class ExpNoEQ : ILogExp
    {
        public bool Compare(object value, object condition)
        {
            if (value == null && condition == null) return false;
            if (value == null || condition == null) return true;
            return !value.Equals(condition);
        }
    }

    public class ExpContains : ILogExp
    {
        public bool Compare(object value, object condition)
        {
            var v = value as string;
            var c = condition as string;
            if (v == null || c == null) return false;
            return v.Contains(c);
        }
    }

    public class ExpNoContains : ILogExp
    {
        public bool Compare(object value, object condition)
        {
            var v = value as string;
            var c = condition as string;
            if (v == null || c == null) return false;
            return !v.Contains(c);
        }
    }

    public class ExpGT : ILogExp
    {
        public bool Compare(object value, object condition)
        {
            if (value is IComparable val && condition is IComparable cond)
                return val.CompareTo(cond) > 0;
            
            return false;
        }
    }

    public class ExpLT : ILogExp
    {
        public bool Compare(object value, object condition)
        {
            if (value is IComparable val && condition is IComparable cond)
                return val.CompareTo(cond) < 0;
            
            return false;
        }
    }

    public class ExpGE : ILogExp
    {
        public bool Compare(object value, object condition)
        {
            if (value is IComparable val && condition is IComparable cond)
                return val.CompareTo(cond) >= 0;
            
            return false;
        }
    }

    public class ExpLE : ILogExp
    {
        public bool Compare(object value, object condition)
        {
            if (value is IComparable val && condition is IComparable cond)
                return val.CompareTo(cond) <= 0;
            
            return false;
        }
    }
}
