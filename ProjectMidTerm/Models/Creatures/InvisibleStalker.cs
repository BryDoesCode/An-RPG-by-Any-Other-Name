using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class InvisibleStalker : Elemental
    {
        public InvisibleStalker(int x, int y, Direction facing) : base()
        {
            this.Strength = 16;
            this.Dexterity = 19;
            this.Constitution = 14;
            this.Intelligence = 10;
            this.Wisdom = 15;
            this.Charisma = 11;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            this.ResistanceToBludgeoning = 50;
            this.ResistanceToPiercing = 50;
            this.ResistanceToSlashing = 50;

            //HP 16d8+32
            this.CurrentHP = Dice.Roll(16, 8, 32);
            this.MaxHP = CurrentHP;

            this.ExperiencePoints = 25;
            this.Gold = 30;

            this.ArmorClass = 14;
            this.ImageName = "InvisibleStalker.PNG";
            this.Name = "Invisible Stalker";

            DropableItems = new Container<ItemQuantity>(3);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(3) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3011, 1));
                }
                else if (Dice.Roll(4) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1003, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +6 to hit, reach 5 ft., one target. 
        //   Hit: 2d6 + 3 bludgeoning damage.
        public string Slam(Creature def)
        {
            int toHit = Dice.Roll(1,20,6);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(2, 6, 3);
                def.CurrentHP -= damage;
                return "InvisibleStalker uses Slam against " + def.Name +
                        " for " + damage + " bludgeoning damage!";
            }
            else
            {
                return "InvisibleStalker fails to hit " + def.Name + "!";
            }
        }

        public string Multiattack(Creature def)
        {
            return "InvisisbleStalker uses Multiattack.\n" + Slam(def) + "\n" + Slam(def);
        }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Slam\n  Multiattack";
        }

        public override string Attack(Creature c)
        {
            if (Dice.Roll(3) == 1)
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
