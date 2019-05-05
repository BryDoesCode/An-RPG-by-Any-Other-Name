using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Models;

namespace ProjectMidTerm.Models.Items
{
    class Sword : Weapon
    {
        public Sword(int itemID, string name, int minDamage, int maxDamage, string damageType, string description, int price, int usesLeft, float useChance) : base()
        {
            this.ItemID = itemID;
            this.Name = name;
            this.MinDamage = minDamage;
            this.MaxDamage = maxDamage;
            this.DamageType = damageType;
            this.Description = description;
            this.Price = price;
            this.UsesLeft = usesLeft;
            this.UseChance = useChance;
            this.Type = "Sword";
        }
        
        public new Sword Clone()
        {
            return new Sword(ItemID, Name, MinDamage, MaxDamage, DamageType, Description, Price, UsesLeft, UseChance);
        }

    }
}
