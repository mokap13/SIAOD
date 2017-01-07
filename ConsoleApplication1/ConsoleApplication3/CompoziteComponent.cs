using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    public class CompoziteComponent : Component
    {
        private List<Component> components = new List<Component>();
        public CompoziteComponent(string name):base(name)
        {}
        public override void Add(Component component)
        {
            this.components.Add(component);
        }

        public override void Add(Component component, int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.Add(component);
            }
        }

        public override void Display()
        {
            Console.WriteLine();
            foreach (var component in components)
            {
                component.Display();
            }
            Console.Write(this.Name);
        }

        public override void Remove(Component component)
        {
            this.components.Remove(component);
        }
    }
}
