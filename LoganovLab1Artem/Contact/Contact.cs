using System;

namespace LoganovLab1Artem.ContactSpace
{
    public class Contact
    {
        private DateTime _beginDt;
        private DateTime _endDt;
        private string _descr;
        private string _dataInfo;
        private ContType _cntType;

        public DateTime BeginDt
        {
            get => _beginDt;
            set => _beginDt = value;
        }

        public DateTime EndDt
        {
            get => _endDt;
            set => _endDt = value;
        }

        public string Descr
        {
            get => _descr;
            set => _descr = value;
        }

        public string DataInfo
        {
            get => _dataInfo;
            set => _dataInfo = value;
        }

        public ContType CntType
        {
            get => _cntType;
            set => _cntType = value;
        }

        public Contact Clone()
        {
            return new Contact
            {
                _beginDt = this._beginDt,
                _endDt = this._endDt,
                _descr = this._descr,
                _dataInfo = this._dataInfo,
                _cntType = new ContType(this._cntType.Name, this._cntType.Note)
            };
        }

        public override bool Equals(object obj)
        {
            if (obj is Contact other)
            {
                return _beginDt == other._beginDt &&
                       _endDt == other._endDt &&
                       _descr == other._descr &&
                       _dataInfo == other._dataInfo &&
                       Equals(_cntType, other._cntType);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + _beginDt.GetHashCode();
            hash = hash * 23 + _endDt.GetHashCode();
            hash = hash * 23 + (_descr?.GetHashCode() ?? 0);
            hash = hash * 23 + (_dataInfo?.GetHashCode() ?? 0);
            hash = hash * 23 + (_cntType?.GetHashCode() ?? 0);
            return hash;
        }
    }
}
