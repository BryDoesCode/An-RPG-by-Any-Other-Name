using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class Sprite : Fey
    {
        public Sprite(int x, int y, Direction facing) : base()
        {
            this.Strength = 3;
            this.Dexterity = 18;
            this.Constitution = 10;
            this.Intelligence = 14;
            this.Wisdom = 13;
            this.Charisma = 11;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            //HP 2d4
            this.CurrentHP = Dice.Roll(2, 4);
            this.MaxHP = CurrentHP;

            this.ArmorClass = 15;

            this.ExperiencePoints = 2;
            this.Gold = 5;

            this.ImageName = "Sprite.PNG";
            this.Name = "Sprite";

            DropableItems = new Container<ItemQuantity>(2);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(7) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3016, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1001, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +2 to hit, reach 5 ft., one target. 
        //   Hit: 1d2 slashing damage. Edited so he has a chance. 
        public string Longsword(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 2);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(1, 2);
                def.CurrentHP -= damage;
                return "Sprite uses Rake against " + def.Name +
                        " for " + damage + " slashing damage!";
            }
            else
            {
                return "Sprite fails to hit " + def.Name + "!";
            }
        }

        //   Ranged Weapon Attack: +6 to hit, reach 40/160 ft., one target. 
        //   Hit: 1d2 piercing damage. Edited so he has a chance. 
        public string Shortbow(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 6);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(1, 2);
                def.CurrentHP -= damage;
                return "Sprite uses Shortbow against " + def.Name +
                        " for " + damage + " piercing damage!";
            }
            else
            {
                return "Sprite fails to hit " + def.Name + "!";
            }
        }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Longsword\n  Shortbow";
        }

        public override string Attack(Creature c)
        {
            if (Dice.Roll(1, 2) == 1)
            {
                return Longsword(c);
            }
            else
            {
                return Shortbow(c);
            }
        }
    }
}
