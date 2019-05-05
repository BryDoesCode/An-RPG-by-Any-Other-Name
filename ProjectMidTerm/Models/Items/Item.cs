using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Interfaces;

namespace ProjectMidTerm.Models.Items
{
    public class Item : IUsable, ILocatable
    {
        public int UsesLeft { get; set; }
        public float UseChance { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get; set; }
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsUnique { get; set; }
        
        public Item(bool isUnique = false)
        {
            this.X = -99;
            this.Y = -99;
            this.Facing = Direction.NORTH;
            IsUnique = IsUnique;
        }

        public virtual string FailureMessage()
        {
            return null;
        }

        public virtual string SuccessMessage()
        {
            return null;
        }
        
        public virtual Item Clone()
        {
            return new Item();
        }

        public void UpdateLocation(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
