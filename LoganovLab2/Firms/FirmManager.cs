using LoganovLab1.Domain;
using LoganovLab1.List;

namespace LoganovLab2.Firms
{
    public class FirmManager
    {
        private FirmView _firmVw;
        private FirmList _firmList;

        public FirmManager(FirmView firmVw, params Firm[] firms)
        {
            _firmVw = firmVw;
            _firmList = new FirmList(firms);
        }

        public FirmView FirmView
        {
            get { return _firmVw; }
            set { _firmVw = value; }
        }

        public Firm[] GetAllFirms()
        {
            return _firmList.ToArray();
        }

        public void AddFirm(Firm firm)
        {
            _firmList.AddFirm(firm);
        }

        public void ReplaceFirms(Firm[] firms)
        {
            _firmList = new FirmList(firms);
        }
    }
}
