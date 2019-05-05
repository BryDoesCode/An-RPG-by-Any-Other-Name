using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class AwakenedTree : Plant
    {
        public AwakenedTree(int x, int y, Direction facing) : base()
        {
            this.Strength = 19;
            this.Dexterity = 6;
            this.Constitution = 15;
            this.Intelligence = 10;
            this.Wisdom = 10;
            this.Charisma = 7;

            this.ResistanceToBludgeoning = 50;
            this.ResistanceToPiercing = 50;
            this.ResistanceToFire = -50;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            //HP 7d12+14
            this.CurrentHP = Dice.Roll(7, 12, 14);
            this.MaxHP = CurrentHP;

            this.ArmorClass = 13;

            this.ExperiencePoints = 10;
            this.Gold = 20;

            this.ImageName = "AwakenedTree.PNG";
            this.Name = "Awakened Tree";

            DropableItems = new Container<ItemQuantity>(3);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(3) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3003, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1002, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3004, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +6 to hit, reach 10 ft., one target. 
        //   Hit: 3d6 + 4 bludgeoning damage.
        public string Slam(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 6);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(3, 6, 4);
                def.CurrentHP -= damage;
                return "AwakenedTree uses Slam against " + def.Name +
                        " for " + damage + " bludgeoning damage!";
            }
            else
            {
                return "AwakenedTree fails to hit " + def.Name + "!";
            }
        }
        
        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Slam";
        }

        public override string Attack(Creature c)
        {
            return Slam(c);
        }
    }
}
