using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuroApp.Analyzes.HIVAssociateDisease
{
    public class HIVAssociateDiseaseGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<HIVAssociateDisease> HIVAssociateDiseases { get; set; }
        public HIVAssociateDiseaseGroup()
        {
            HIVAssociateDiseases = new List<HIVAssociateDisease>();
        }
    }
}
