using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public interface IWarehouse
    {
        void AddComponent(IComponent component);
        void RemoveComponent(IComponent component);
    }
}
