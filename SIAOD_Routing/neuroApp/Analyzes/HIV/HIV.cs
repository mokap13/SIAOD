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
        public double Duration { get; set; }
        public double DurationTreshold
        { get
            {
                return 12.5;
            }
        }

        public HIVPhase Phase { get; set; }
        public int PhaseId { get; set; }
        public HIVStage Stage { get; set; }
        public int StageId { get; set; }
        public Patient Patient { get; set; }
        public int PatientId { get; set; }
    }
}
