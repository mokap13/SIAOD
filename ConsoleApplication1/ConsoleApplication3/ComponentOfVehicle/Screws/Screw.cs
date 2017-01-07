using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3.ComponentOfVehicle
{
    abstract class Screw : CompoziteComponent
    {
        public Screw(string name) : base(name)
        {
        }
    }
}
