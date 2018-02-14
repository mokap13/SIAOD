using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.HIV
{
    public class HIVPhase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<HIV> HIVPhases { get; set; }

        public HIVPhase()
        {
            HIVPhases = new List<HIV>();
        }
    }
}
