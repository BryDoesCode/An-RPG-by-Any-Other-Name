using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Models.Creatures;
using ProjectMidTerm.ViewModels;

namespace ProjectMidTerm.Models.Items
{
    public class Weapon : Item
    {
        public int MinDamage { get; protected set; }
        public int MaxDamage { get; protected set; }
        public string DamageType { get; protected set; }
        public PCharacter CurrentPlayer { get; set; }

        public Weapon() : base (true)
        {
            CurrentPlayer = GameSession.CurrentPlayer; 
        }

        public override string FailureMessage()
        {
            return $"{this.Name} failed to equip.";
        }

        public override string SuccessMessage()
        {
            CurrentPlayer.ChooseWeapon(this);
            return $"{CurrentPlayer.Name} eqiupped {this.Name}.";
        }

    }
}

        
    

