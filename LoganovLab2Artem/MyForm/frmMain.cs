using LoganovLab2Artem.Controller;
using LoganovLab2Artem.FirmSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.MyForm
{
    public class frmMain : Form
    {
        private ListView listViewFirms;
        private MainContr _mainContr;

        public frmMain(MainContr mainContr)
        {
            _mainContr = mainContr;
            listViewFirms = new ListView { Dock = DockStyle.Fill, View = View.Details };
            this.Controls.Add(listViewFirms);

            var menu = new MenuStrip();
            var filterItem = new ToolStripMenuItem("Фильтр");
            filterItem.Click += (s, e) => { _mainContr.StartFilter(); RefreshList(); };
            menu.Items.Add(filterItem);
            this.Controls.Add(menu);
            this.MainMenuStrip = menu;

            RefreshList();
        }

        public void DisplayFirms(FirmMngr mngr, FirmVw vw)
        {
            listViewFirms.Clear();
            var fields = vw.GetFields();
            foreach (var f in fields)
            {
                listViewFirms.Columns.Add(f.GetType().Name.Replace("Field", ""));
            }

            foreach (var firm in mngr.GetAllFirms())
            {
                var vals = vw.GetFirmValues(firm);
                var lvi = new ListViewItem(vals[0]);
                for (int i = 1; i < vals.Length; i++)
                    lvi.SubItems.Add(vals[i]);
                listViewFirms.Items.Add(lvi);
            }
        }

        private void RefreshList()
        {
            DisplayFirms(_mainContr.CurrentFirmMngr, _mainContr.CurrentFirmMngr.CurrentView);
        }
    }
}
