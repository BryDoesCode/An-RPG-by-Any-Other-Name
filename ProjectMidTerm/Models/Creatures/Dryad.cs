using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class Dryad : Fey
    {
        public Dryad(int x, int y, Direction facing) : base()
        {
            this.Strength = 10;
            this.Dexterity = 12;
            this.Constitution = 11;
            this.Intelligence = 14;
            this.Wisdom = 15;
            this.Charisma = 18;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            this.ResistanceToCold = 50;
            this.ResistanceToFire = 25;
            this.ResistanceToLightning = 50;
            this.ResistanceToPsychic = 50;
            this.ResistanceToRadiant = 50;
            this.ResistanceToThunder = 50;

            //HP 5d8
            this.CurrentHP = Dice.Roll(5, 8);
            this.MaxHP = CurrentHP;

            this.ExperiencePoints = 5;
            this.Gold = 10;

            this.ArmorClass = 11;
            this.ImageName = "Dryad.PNG";
            this.Name = "Dryad";

            DropableItems = new Container<ItemQuantity>(2);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(4) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3007, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1003, 1));
                }
                else if (Dice.Roll(4) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3008, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +2 to hit, reach 5 ft., one target. 
        //   Hit: 1d4 bludgeoning damage.
        public string Club(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 2);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(1, 4);
                def.CurrentHP -= damage;
                return "Dryad uses Club against " + def.Name +
                        " for " + damage + " bludgeoning damage!";
            }
            else
            {
                return "Dryad fails to hit " + def.Name + "!";
            }


        }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Club";
        }

        public override string Attack(Creature c)
        {
            return Club(c);
        }
    }
}
