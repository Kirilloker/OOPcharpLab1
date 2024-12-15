using LoganovLab1.Domain;
using LoganovLab1.Factory;
using LoganovLab1.Type;
using LoganovLab2.Controller;
using LoganovLab2.Firms;

namespace LoganovLab2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            InitializeData();

            var firmVw = new FirmView();
            // ��������� ��� �������� ����
            firmVw.AddField(new NameField());
            firmVw.AddField(new RegionField());
            firmVw.AddField(new TownField());
            // �������� ��������� ����


            var allFirms = CreateSampleFirms();
            var firmMngr = new FirmManager(firmVw, allFirms);
            var mainContr = new MainController(firmMngr);

            Application.Run(new FormMain(mainContr));
        }

        static void InitializeData()
        {
            var f = FirmFactory.Instance;
            f.SubFirmTypes.AddType(new SubFirmType("MainOffice", true));
            f.SubFirmTypes.AddType(new SubFirmType("Branch1"));
            f.ContactTypes.AddType(new ContactType("�������", "����������� � ��������"));
            f.ContactTypes.AddType(new ContactType("������", "��������� �������"));
        }

        static Firm[] CreateSampleFirms()
        {
            var f = FirmFactory.Instance;

            Firm MakeFirm(string full, string reg, string city)
            {
                var firm = f.CreateFirm();
                firm.FullName = full;
                firm.Region = reg;
                firm.City = city;

                return firm;
            }

            return new[]
            {
        MakeFirm("��� ����������", "������-��������", "�����������"),
        MakeFirm("��� ���������", "���������", "������������"),
        MakeFirm("��� ���������", "�����������", "������"),
        MakeFirm("��� ������������", "���������", "�������"),
        MakeFirm("��� ��������", "�����", "�����������")
    };
        }


    }
}
