using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.HIV
{
    public class HIVStage
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<HIV> HIVs { get; set; }

        public HIVStage()
        {
            HIVs = new List<HIV>();
        }
    }
}
