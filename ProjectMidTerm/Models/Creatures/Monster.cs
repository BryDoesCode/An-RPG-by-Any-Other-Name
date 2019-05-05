using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    public class Monster : Creature
    {
        public Container<ItemQuantity> DropableItems { get; set; }
    }
}
