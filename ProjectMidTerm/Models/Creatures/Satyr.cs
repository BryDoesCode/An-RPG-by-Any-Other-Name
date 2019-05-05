using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class Satyr : Fey
    {
        public Satyr(int x, int y, Direction facing) : base()
        {
            this.Strength = 12;
            this.Dexterity = 16;
            this.Constitution = 11;
            this.Intelligence = 12;
            this.Wisdom = 10;
            this.Charisma = 14;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            this.ResistanceToCold = 50;
            this.ResistanceToFire = 50;
            this.ResistanceToLightning = 50;
            this.ResistanceToPsychic = 50;
            this.ResistanceToRadiant = 50;
            this.ResistanceToThunder = 50;

            //HP 7d8
            this.CurrentHP = Dice.Roll(7, 8);
            this.MaxHP = CurrentHP;

            this.ExperiencePoints = 10;
            this.Gold = 15;

            this.ArmorClass = 14;

            this.ImageName = "Satyr.PNG";
            this.Name = "Satyr";

            DropableItems = new Container<ItemQuantity>(3);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(3) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3013, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1002, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3014, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +5 to hit, reach 5 ft., one target. 
        //   Hit: 1d6+3 piercing damage.
        public string Shortsword(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 5);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(1, 6, 3);
                def.CurrentHP -= damage;
                return "Satyr uses Shortsword against " + def.Name +
                        " for " + damage + " piercing damage!";
            }
            else
            {
                return "Satyr fails to hit " + def.Name + "!";
            }
        }

        //   Ranged Weapon Attack: +5 to hit, reach 80/320 ft., one target. 
        //   Hit: 1d6+3 piercing damage.
        public string Shortbow(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 5);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(1, 6, 3);
                def.CurrentHP -= damage;
                return "Satyr uses Shortbow against " + def.Name +
                        " for " + damage + " piercing damage!";
            }
            else
            {
                return "Satyr fails to hit " + def.Name + "!";
            }
        }

        //   Melee Weapon Attack: +3 to hit, reach 5 ft., one target. 
        //   Hit: 2d4+1 bludgeoning damage.
        public string Ram(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 3);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(2, 4, 1);
                def.CurrentHP -= damage;
                return "Satyr uses Ram against " + def.Name +
                        " for " + damage + " bludgeoning damage!";
            }
            else
            {
                return "Satyr fails to hit " + def.Name + "!";
            }
        }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Ram\n  Shortsword\n  Shortbow";
        }

        public override string Attack(Creature c)
        {
            switch(Dice.Roll(1, 3))
            {
                case 1:
                    return Ram(c);
                case 2:
                    return Shortsword(c);
                case 3:
                    return Shortbow(c);
                default:
                    return "Attack failed.";
            }
        }
    }
}
