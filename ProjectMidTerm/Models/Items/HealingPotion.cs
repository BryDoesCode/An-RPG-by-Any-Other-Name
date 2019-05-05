using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Items
{
    class HealingPotion : Potion
    {
        public HealingPotion (int itemID, string name, int effectAmount, string description, int price, int usesLeft, float useChance) : base()
        {
            this.ItemID = itemID;
            this.Name = name;
            this.EffectAmount = effectAmount;
            this.Description = description;
            this.Price = price;
            this.UsesLeft = usesLeft;
            this.UseChance = useChance;
            
            this.Type = "Health Potion";
        }
        
        public override string SuccessMessage()
        {
            this.UsesLeft -= 1;
            CurrentPlayer.Heal(EffectAmount);
            if (this.UsesLeft <= 0)
            {
                CurrentPlayer.RemoveItemFromInventory(this);
            }
            return $"The {this.Name} worked! {CurrentPlayer.Name} gained {this.EffectAmount} HP!";
        }
        
        public new HealingPotion Clone()
        {
            return new HealingPotion(ItemID, Name, EffectAmount, Description, Price, UsesLeft, UseChance);
        }

    }
}
