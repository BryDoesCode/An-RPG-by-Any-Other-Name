using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectMidTerm.Models.Creatures
{
    class IceMephit : Elemental
    {
        public IceMephit(int x, int y, Direction facing) : base()
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
            this.ResistanceToBludgeoning = -50;
            this.ResistanceToCold = 100;

            //HP 6d6
            this.CurrentHP = Dice.Roll(6, 6);
            this.MaxHP = CurrentHP;

            this.ExperiencePoints = 5;
            this.Gold = 10;

            this.ArmorClass = 11;
            this.ImageName = "IceMephit.PNG";
            this.Name = "Ice Mephit";

            DropableItems = new Container<ItemQuantity>(2);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(3) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3010, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1001, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +3 to hit, reach 5 ft., one creature. 
        //   Hit: 1d4+1 slashing damage plus 1d4 cold damage.
        public string Claws(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 3);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int slashDamage = Dice.Roll(1, 4, 1), coldDamage = Dice.Roll(4);
                def.CurrentHP -= (slashDamage + coldDamage);
                return "IceMephit uses Claws against " + def.Name +
                        " for " + slashDamage + " slashing and " + coldDamage + " cold damage!";
            }
            else
            {
                return "IceMephit fails to hit " + def.Name + "!";
            }
        }

        // Recharge 6. Defendant must succeed on a DC 10 Dexterity saving throw, taking 2d4 cold damage on
        // a failed save, or half as much on a successful one. Saving throw ignores modifiers for simplicity.
        public string FrostBreath(Creature def)
        {
            int coldDamage = Dice.Roll(2, 4);
            int savingThrow = Dice.Roll(20);
            if (savingThrow >= 10)
            {
                def.CurrentHP -= (coldDamage / 2);
                return "IceMephit uses Frost Breath against " + def.Name + ".\n  " + def.Name +
                    " successfully dodges and only takes " + (coldDamage / 2) + " cold damage!";
            }
            else
            {
                def.CurrentHP -= coldDamage;
                return "IceMephit uses Frost Breath against " + def.Name + ".\n  " + def.Name +
                    " fails to dodge and takes " + (coldDamage) + " cold damage!";
            }
        }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Claws\n  Frost Breath";
        }

        public override string Attack(Creature c)
        {
            if (Dice.Roll(6) == 6)
            {
                return FrostBreath(c);
            }
            else
            {
                return Claws(c);
            }
        }
    }
}

