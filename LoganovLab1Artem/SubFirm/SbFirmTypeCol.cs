using System;
using System.Collections;
using System.Collections.Generic;

namespace LoganovLab1Artem.SubFirmSpace
{
    public class SbFirmTypeCol : IEnumerator<SbFirmType>, IEnumerable<SbFirmType>
    {
        private List<SbFirmType> _lst;
        private int _position = 0;

        public int Count => _lst.Count;

        public SbFirmType Current => _lst[_position];

        object IEnumerator.Current => Current;

        public SbFirmType this[int index]
        {
            get => _lst[index];
            set => _lst[index] = value;
        }

        public SbFirmTypeCol()
        {
            _lst = new List<SbFirmType>();
        }

        public void Add(SbFirmType type)
        {
            if (type == null)
                return;

            bool exists = false;
            foreach (SbFirmType t in _lst)
            {
                if (t.Name == type.Name)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                _lst.Add(type);
            }
        }


        public SbFirmType GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            foreach (SbFirmType type in _lst)
            {
                if (string.Equals(type.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    return type;
                }
            }

            return null;
        }

        public void Dispose()
        {
            Reset();
        }

        public IEnumerator<SbFirmType> GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (_position < _lst.Count - 1)
            {
                _position++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _position = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
