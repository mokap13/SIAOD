using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.HIV
{
    public class HIV
    {
        public int Id { get; set; }
        public int Duration { get; set; }

        public HIVPhase HIVPhase { get; set; }
        public int HIVPhase_id { get; set; }
        public HIVStage HIVStage { get; set; }
        public int HIVStage_id { get; set; }
        public Patient Patient { get; set; }
        public int Patient_id { get; set; }
    }
}
