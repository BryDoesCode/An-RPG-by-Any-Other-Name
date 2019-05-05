using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.ViewModels;

namespace ProjectMidTerm.Models.Items
{
    class ExperiencePotion : Potion
    {
        public ExperiencePotion(int itemID, string name, int effectAmount, string description, int price, int usesLeft, float useChance) : base()
        {
            this.ItemID = itemID;
            this.Name = name;
            this.EffectAmount = effectAmount;
            this.Description = description;
            this.Price = price;
            this.UsesLeft = usesLeft;
            this.UseChance = useChance;

            this.Type = "Experience Potion";
        }
        
        public override string SuccessMessage()
        {
            this.UsesLeft -= 1;
            if (this.UsesLeft <= 0)
            {
                CurrentPlayer.RemoveItemFromInventory(this);
            }
            return $"The {this.Name} worked! " + CurrentPlayer.GainXP(EffectAmount);
        }
        public new ExperiencePotion Clone()
        {
            return new ExperiencePotion(ItemID, Name, EffectAmount, Description, Price, UsesLeft, UseChance);
        }
    }
}
