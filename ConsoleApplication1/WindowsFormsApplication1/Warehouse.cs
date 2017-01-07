using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Warehouse : IWarehouse
    {
        public void AddComponent(IComponent component)
        {
            throw new NotImplementedException();
        }

        public void RemoveComponent(IComponent component)
        {
            throw new NotImplementedException();
        }
    }
}
