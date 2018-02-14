using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.ObjectiveStatus
{
    public class ObjectiveStatusDisease
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ObjectiveStatus> ObjectiveStatuses { get; set; }
        public ObjectiveStatusDisease()
        {
            ObjectiveStatuses = new List<ObjectiveStatus>();
        }
    }
}
