using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    public abstract class Component:IComponent
    {
        protected string name;
        protected int[][][] size;
        protected int weight;
        protected ComponentMaterial componentMaterial;
        protected ComponentColor componentColor;
        protected Component(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int[][][] Size { get; set; }
        public ComponentColor ComponentColor { get; set; }
        public ComponentMaterial ComponentMaterial { get; set; }
        
        public abstract void Add(Component component);
        public abstract void Add(Component component, int count);
        public abstract void Remove(Component component);

        public abstract void Display();
    }
    
    public enum ComponentColor
    {
        Red,Blue,Green
    }
    public enum ComponentMaterial
    {
        Steel,Carbon,Plastic
    }
}
