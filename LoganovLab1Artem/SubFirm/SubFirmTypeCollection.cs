using System;
using System.Collections;
using System.Collections.Generic;

namespace LoganovLab1Artem.SubFirmSpace
{
    public class SbFirmTypeCol : IEnumerator<SubFirmType>, IEnumerable<SubFirmType>
    {
        private List<SubFirmType> _lst;
        private int _position = 0;

        public int Count => _lst.Count;

        public SubFirmType Current => _lst[_position];

        object IEnumerator.Current => Current;

        public SubFirmType this[int index]
        {
            get => _lst[index];
            set => _lst[index] = value;
        }

        public SbFirmTypeCol()
        {
            _lst = new List<SubFirmType>();
        }

        public void Add(SubFirmType type)
        {
            if (type == null)
                return;

            bool exists = false;
            foreach (SubFirmType t in _lst)
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


        public SubFirmType GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            foreach (SubFirmType type in _lst)
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

        public IEnumerator<SubFirmType> GetEnumerator()
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
