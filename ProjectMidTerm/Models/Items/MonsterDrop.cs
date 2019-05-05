using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Items
{
    class MonsterDrop : Collectable
    {
        public MonsterDrop(int itemID, string name, string description, int price) : base()
        {
            this.ItemID = itemID;
            this.Name = name;
            this.Description = description;
            this.Price = price;

            this.Type = "Monster Drop";
        }

        public new MonsterDrop Clone()
        {
            return new MonsterDrop(ItemID, Name, Description, Price);
        }
    }
}
