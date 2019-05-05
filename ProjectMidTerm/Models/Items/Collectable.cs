using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Items
{
    class Collectable : Item
    {
        public Collectable()
        {
            this.UseChance = 1;
            this.UsesLeft = 1;
        }

        public override string FailureMessage()
        {
            return $"{this.Name} does nothing.";
        }

        public override string SuccessMessage()
        {
            return $"{this.Name} sucessfully does nothing.";
        }
    }
}
