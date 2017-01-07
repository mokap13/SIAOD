using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class LeafComponent : Component
    {
        public override void Add(IComponent component)
        {
            throw new NotImplementedException();
        }

        public override void Remove(IComponent component)
        {
            throw new NotImplementedException();
        }
    }
}
