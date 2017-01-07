using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Father
    {
        public State State { get; set; }
        public Father(State state)
        {
            this.State = state;
        }
    }
}
