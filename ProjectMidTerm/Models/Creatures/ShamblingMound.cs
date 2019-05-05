using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class ShamblingMound : Plant
    {
        public ShamblingMound(int x, int y, Direction facing) : base()
        {
            this.Strength = 18;
            this.Dexterity = 8;
            this.Constitution = 16;
            this.Intelligence = 5;
            this.Wisdom = 10;
            this.Charisma = 5;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            this.ResistanceToCold = 50;
            this.ResistanceToFire = 50;
            this.ResistanceToLightning = 100;

            //HP 16d10 + 48
            this.CurrentHP = Dice.Roll(16, 10, 48);
            this.MaxHP = CurrentHP;

            this.ArmorClass = 15;

            this.ExperiencePoints = 20;
            this.Gold = 40;

            this.ImageName = "ShamblingMound.PNG";
            this.Name = "Shambling Mound";

            DropableItems = new Container<ItemQuantity>(5);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(3) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3015, 1));
                }
                else if (Dice.Roll(8) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1003, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +7 to hit, reach 5 ft., one target. 
        //   Hit: 2d8 + 4 bludgeoning damage.
        public string Slam(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 7);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(2, 8, 4);
                def.CurrentHP -= damage;
                return "ShamblingMound uses Slam against " + def.Name +
                        " for " + damage + " bludgeoning damage!";
            }
            else
            {
                return "ShamblingMound fails to hit " + def.Name + "!";
            }
        }

        public string Multiattack(Creature def)
        {
            return "ShamblingMound uses Multiattack.\n" + Slam(def) + "\n" + Slam(def);
        }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Slam\n  Multiattack";
        }

        public override string Attack(Creature c)
        {
            if (Dice.Roll(4) == 1)
            {
                return Multiattack(c);
            }
            else
            {
                return Slam(c);
            }
        }
    }
}
