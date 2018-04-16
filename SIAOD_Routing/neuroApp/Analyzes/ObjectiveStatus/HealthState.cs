using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.ObjectiveStatus
{
    public class HealthState
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ObjectiveStatus> ObjectiveStatuses { get; set; }
    }
}
