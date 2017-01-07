using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    public class LeafComponent : Component
    {
        public LeafComponent(string name) : base(name)
        {
        }

        public override void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public override void Add(Component component, int count)
        {
            throw new NotImplementedException();
        }

        public override void Display()
        {
            Console.WriteLine(Name);
        }

        public override void Remove(Component component)
        {
            throw new NotImplementedException();
        }
    }
}
