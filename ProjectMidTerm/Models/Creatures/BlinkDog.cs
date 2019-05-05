using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class BlinkDog : Fey
    {
        public BlinkDog(int x, int y, Direction facing) : base()
        {
            this.Strength = 12;
            this.Dexterity = 17;
            this.Constitution = 12;
            this.Intelligence = 10;
            this.Wisdom = 13;
            this.Charisma = 11;
            this.X = x;
            this.Y = y;
            this.Facing = facing;

            //HP 4d8 + 4
            this.CurrentHP = Dice.Roll(4, 8, 4);
            this.MaxHP = CurrentHP;

            this.ArmorClass = 13;

            this.ExperiencePoints = 5;
            this.Gold = 10;

            this.ImageName = "BlinkDog.PNG";
            this.Name = "Blink Dog";

            DropableItems = new Container<ItemQuantity>(2);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(3) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3005, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1002, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3006, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +3 to hit, reach 5 ft., one target. 
        //   Hit: 1d6+1 piercing damage.
        public string Bite(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 3);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(1, 6, 1);
                def.CurrentHP -= damage;
                return "BlinkDog uses Rake against " + def.Name +
                        " for " + damage + " piercing damage!";
            }
            else
            {
                return "BlinkDog fails to hit " + def.Name + "!";
            }
        }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Bite";
        }

        public override string Attack(Creature c)
        {
            return Bite(c);
        }
    }
}
