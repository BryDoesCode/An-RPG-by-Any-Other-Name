using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectMidTerm.Models.Creatures
{
    class DustMephit : Elemental
    {
        public DustMephit(int x, int y, Direction facing) : base()
        {
            this.Strength = 7;
            this.Dexterity = 13;
            this.Constitution = 10;
            this.Intelligence = 9;
            this.Wisdom = 11;
            this.Charisma = 12;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            this.ResistanceToFire = -50;

            //HP 5d6
            this.CurrentHP = Dice.Roll(5, 6);
            this.MaxHP = CurrentHP;

            this.ExperiencePoints = 5;
            this.Gold = 10;

            this.ArmorClass = 12;
            this.ImageName = "DustMephit.PNG";
            this.Name = "Dust Mephit";

            DropableItems = new Container<ItemQuantity>(2);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(4) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3009, 1));
                }
                else if (Dice.Roll(7) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1001, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +4 to hit, reach 5 ft., one creature. 
        //   Hit: 1d4+2 slashing damage.
        public string Claws(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 4);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int slashDamage = Dice.Roll(1, 4, 2);
                def.CurrentHP -= slashDamage;
                return "DustMephit uses Claws against " + def.Name +
                        " for " + slashDamage + " slashing damage.";
            }
            else
            {
                return "DustMephit fails to hit " + def.Name + "!";
            }
        }        

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Claws";
        }

        public override string Attack(Creature c)
        {
            return Claws(c);
        }
    }
}

