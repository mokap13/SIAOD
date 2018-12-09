using Crypto1.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto1.Models.Enigma
{
    public class MapEnumerator<T1, T2> : IEnumerator
    {
        public Rotor<T1, T2>[] charPairs;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public MapEnumerator(Rotor<T1, T2>[] list)
        {
            charPairs = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < charPairs.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current => Current;

        public Rotor<T1, T2> Current
        {
            get
            {
                try
                {
                    return charPairs[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    public class Rotor<T1, T2> : IEnumerable
    {
        public class Indexer<T3, T4>
        {
            private Dictionary<T3, T4> _dictionary;
            public Indexer(Dictionary<T3, T4> dictionary)
            {
                _dictionary = dictionary;
            }
            public T4 this[T3 index]
            {
                get { return _dictionary[index]; }
                set { _dictionary[index] = value; }
            }
        }
        public Indexer<T1, T2> Forward { get; private set; }
        public Indexer<T2, T1> Reverse { get; private set; }

        private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

        public Rotor()
        {
            this.Forward = new Indexer<T1, T2>(_forward);
            this.Reverse = new Indexer<T2, T1>(_reverse);
        }

        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

        public void SetPosition(T1 newFirstObj)
        {
            int indexForward = this._forward.Keys.ToList().IndexOf(newFirstObj);
            T2 value = this._forward[newFirstObj];
            int indexRevers = this._reverse.Keys.ToList().IndexOf(value);

            //List<T2> newSequenceValues = this._forward.Values
            //    .ToList()
            //    .MoveRange(this._forward[newFirstObj], 0);
            //foreach (KeyValuePair<T1,T2> pair in this._forward)
            //{
            //    this._forward[pair.Key] = newSequenceValues[this._forward.ToList().IndexOf(pair)];
            //}
        }
        //public void SetPosition(T1 newFirstObj)
        //{
        //    this._forward.ToList().
        //}

        private readonly Rotor<T1, T2>[] charPairs;
        public Rotor(Rotor<T1, T2>[] pArray)
        {
            charPairs = new Rotor<T1, T2>[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                charPairs[i] = pArray[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MapEnumerator<T1, T2>(charPairs);
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }
    }

}
