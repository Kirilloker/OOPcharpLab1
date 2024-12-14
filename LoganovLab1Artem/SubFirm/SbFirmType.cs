namespace LoganovLab1Artem.SubFirmSpace
{
    public class SbFirmType
    {
        private bool _isMain;
        private string _name;

        public bool IsMain
        {
            get => _isMain;
            set => _isMain = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public SbFirmType(string name, bool isMain = false)
        {
            _name = name;
            _isMain = isMain;
        }

        public override string ToString()
        {
            return _name + (_isMain ? " (Основной офис)" : "");
        }
    }
}
