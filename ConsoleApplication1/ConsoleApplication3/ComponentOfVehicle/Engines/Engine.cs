using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    abstract class Engine : CompoziteComponent
    {
        public Engine(string name) : base(name)
        {}

    }
}
