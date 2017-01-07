using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    abstract class Component:IComponent
    {
        protected string name;
        protected int[][][] size;
        protected int weight;
        protected ComponentMaterial componentMaterial;
        protected ComponentColor componentColor;

        public string Name { get; set; }
        public int Weight { get; set; }
        public int[][][] Size { get; set; }
        public ComponentColor ComponentColor { get; set; }
        public ComponentMaterial ComponentMaterial { get; set; }
        
        public abstract void Add(IComponent component);
        public abstract void Remove(IComponent component);
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
