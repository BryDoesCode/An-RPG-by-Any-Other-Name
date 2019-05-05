using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class Elemental : Monster
    {
        /* Fields */
        /* Properties */
        /* Constructors */

        public Elemental() : base()
        {
            this.ResistanceToPoison = 100;
            this.Darkvision = true;
        }
        /* Methods */
    }
}
