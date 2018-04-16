using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.ObjectiveStatus
{
    public class ObjectiveStatusDiseaseObjectiveStatus
    {
        public int ObjectiveStatusId { get; set; }
        public int ObjectiveStatusDiseaseId { get; set; }
        public virtual ObjectiveStatus ObjectiveStatus { get; set; }
        public virtual ObjectiveStatusDisease ObjectiveStatusDisease { get; set; }
    }
}
