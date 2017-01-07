using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class CompoziteComponent : Component
    {
        private List<IComponent> components = new List<IComponent>();

        public override void Add(IComponent component)
        {
            this.components.Add(component);
        }

        public override void Remove(IComponent component)
        {
            this.components.Remove(component);
        }
    }
}
