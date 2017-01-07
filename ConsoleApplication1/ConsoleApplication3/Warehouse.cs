using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    public class Warehouse : IWarehouse
    {
        List<Component> components = new List<Component>();
        private string name;
        public Warehouse(string name)
        {
            this.name = name;
        }
        public string Name { get; }

        public void Add(Component component)
        {
            components.Add(component);
        }

        public void Add(Component component, int count)
        {
            for (int i = 0; i < count; i++)
            {
                components.Add(component);
            }
        }

        public void Remove(Component component)
        {
            components.Remove(component);
        }

        public void Display()
        {
            foreach (var component in components)
            {
                component.Display();
            }
        }

        public void Add(params Component[] components)
        {
            for (int i = 0; i < components.Length; i++)
            {
                this.components.Add(components[i]);
            }
        }
    }
}
