using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBKursovaia.Helpers
{
    public class Formatter
    {
        public Func<double, string> ASD => new Func<double, string>(value => "ASD");
    }
}
