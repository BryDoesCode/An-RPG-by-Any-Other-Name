using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class Plant : Monster
    {
        /* Fields */
        /* Properties */
        /* Constructors */

        public Plant() : base()
        {
            this.ResistanceToPoison = 100;
            this.ResistanceToPsychic = 100; // Immune to all mind-affecting effects.
            this.Darkvision = true;
        }
        /* Methods */

    }
}
