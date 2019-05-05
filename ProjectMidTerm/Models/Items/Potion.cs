using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.ViewModels;
using ProjectMidTerm.Models.Creatures;

namespace ProjectMidTerm.Models.Items
{
    public class Potion : Item
    {
        public PCharacter CurrentPlayer { get; set; } 
        public int EffectAmount { get; set; }
        
        public Potion()
        {
            CurrentPlayer = GameSession.CurrentPlayer; 
        }


        public override string FailureMessage()
        {
            this.UsesLeft -= 1;
            if (this.UsesLeft <= 0)
            {
                CurrentPlayer.RemoveItemFromInventory(this);
            }
            return $"The {this.Name} didn't work.";
        }

    }
}
