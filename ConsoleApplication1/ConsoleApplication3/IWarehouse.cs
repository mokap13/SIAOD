using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    public interface IWarehouse
    {
        void Add(Component component);
        void Add(Component component,int count);
        void Add(params Component[] components);
        void Remove(Component component);
        void Display();
    }
}
