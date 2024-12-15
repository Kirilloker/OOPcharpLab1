namespace LoganovLab2.Filtering
{
    public static class LogExpFactory
    {
        public static ILogExp Create(LogExpEnum expType)
        {
            switch (expType)
            {
                case LogExpEnum.EQ: return new ExpEQ();
                case LogExpEnum.NoEQ: return new ExpNoEQ();
                case LogExpEnum.Contains: return new ExpContains();
                case LogExpEnum.NoContains: return new ExpNoContains();
                case LogExpEnum.GT: return new ExpGT();
                case LogExpEnum.LT: return new ExpLT();
                case LogExpEnum.GE: return new ExpGE();
                case LogExpEnum.LE: return new ExpLE();
                default:
                    return new ExpEQ();
            }
        }
    }
}
