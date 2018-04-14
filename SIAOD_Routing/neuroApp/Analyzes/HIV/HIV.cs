using neuroApp.Model;
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

        public HIVPhase Phase { get; set; }
        public int Phase_id { get; set; }
        public HIVStage Stage { get; set; }
        public int Stage_id { get; set; }
        public Patient Patient { get; set; }
        public int Patient_id { get; set; }
    }
}
