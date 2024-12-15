using LoganovLab1Artem.ContactSpace;
using LoganovLab2Artem.FirmSpace;
using LoganovLab2Artem.MyForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Controller
{
    public class MainContr
    {
        private FirmMngr _firmMngr;
        private ContTypeCol _contTypeCol;

        public MainContr(FirmMngr firmMngr, ContTypeCol contTypeCol)
        {
            _firmMngr = firmMngr;
            _contTypeCol = contTypeCol;
        }

        public FirmMngr CurrentFirmMngr => _firmMngr;

        public void RefreshFirmsView(frmMain mainForm)
        {
            mainForm.DisplayFirms(_firmMngr, _firmMngr.CurrentView);
        }

        public void StartFilter()
        {
            FilterContr filterContr = new FilterContr(this, _firmMngr);
            using (var filterForm = new frmFilter(filterContr, _firmMngr.CurrentView))
            {
                if (filterForm.ShowDialog() == DialogResult.OK)
                {
                    _firmMngr = filterContr.GetFilteredFirmMngr();
                }
            }
        }

        public void SetFirmMngr(FirmMngr newMngr)
        {
            _firmMngr = newMngr;
        }
    }
}
