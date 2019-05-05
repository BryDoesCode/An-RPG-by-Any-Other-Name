using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class AwakenedShrub : Plant
    {
        public AwakenedShrub(int x, int y, Direction facing) : base()
        {
            this.Strength = 3;
            this.Dexterity = 8;
            this.Constitution = 11;
            this.Intelligence = 10;
            this.Wisdom = 10;
            this.Charisma = 6;
            
            this.ResistanceToPiercing = 50;
            this.ResistanceToFire = -50;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            //HP 3d6
            this.CurrentHP = Dice.Roll(3, 6);
            this.MaxHP = CurrentHP;

            this.ArmorClass = 9;

            this.ExperiencePoints = 1;
            this.Gold = 1;

            this.ImageName = "AwakenedShrub.PNG";
            this.Name = "Awakened Shrub";

            DropableItems = new Container<ItemQuantity>(3);

            for(int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(3) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3001, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1001, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3002, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +1 to hit, reach 5 ft., one target. 
        //   Hit: 1d4-1 slashing damage.
        public string Rake(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 1);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(1, 4, -1);
                def.CurrentHP -= damage;
                return "AwakenedShrub uses Rake against " + def.Name +
                        " for " + damage + " slashing damage!";
            }
            else
            {
                return "AwakenedShrub fails to hit " + def.Name + "!";
            }
        }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Rake";
        }

        public override string Attack(Creature c)
        {
            return Rake(c);
        }
    }
}
