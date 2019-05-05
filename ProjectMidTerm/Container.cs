using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm
{
    public class Container<T>
    {
        public int FixedCapacity { get; }
        private T[] Elements { get; set; }
        private bool[] SlotsInUse { get; set; }

        public T this[int index]
        {
            get
            {
                if (SlotsInUse[index])
                {

                    return Elements[index];
                }
                else
                {
                    return default(T);
                }
            }
        }

        public Container()
        {
            FixedCapacity = 15;
            Elements = new T[FixedCapacity];
            SlotsInUse = new bool[FixedCapacity];
        }

        public Container(int size)
        {
            FixedCapacity = size;
            Elements = new T[FixedCapacity];
            SlotsInUse = new bool[FixedCapacity];
        }        

        public override string ToString()
        {
            string output = "Contents:\n";
            for (int idx = 0; idx < FixedCapacity; idx++)
            {
                if (SlotsInUse[idx])
                {
                    output += Elements[idx] + "\n";
                }
                else
                {
                    output += "...\n";
                }
            }
            return output;
        }

        public bool Add(T newElement)
        {
            for (int idx = 0; idx < FixedCapacity; idx++)
            {
                if (!SlotsInUse[idx])
                {
                    Elements[idx] = newElement;
                    SlotsInUse[idx] = true;
                    return true;
                }
            }
            return false;
        }

        public bool Remove(T newElement)
        {
            for (int idx = 0; idx < FixedCapacity; idx++)
            {
                if (Elements[idx].Equals(newElement))
                {
                    SlotsInUse[idx] = false;
                    return true;
                }
            }
            return false;
        }

        public int Length()
        {
            return FixedCapacity;
        }

    }
}
