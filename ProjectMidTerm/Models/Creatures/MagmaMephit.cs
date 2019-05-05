using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectMidTerm.Models.Creatures
{
    class MagmaMephit : Elemental
    {
        public MagmaMephit(int x, int y, Direction facing) : base()
        {
            this.Strength = 8;
            this.Dexterity = 12;
            this.Constitution = 12;
            this.Intelligence = 7;
            this.Wisdom = 10;
            this.Charisma = 10;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            this.ResistanceToFire = 100;
            this.ResistanceToCold = -50;

            //HP 5d6+5
            this.CurrentHP = Dice.Roll(5, 6, 5);
            this.MaxHP = CurrentHP;

            this.ArmorClass = 11;

            this.ExperiencePoints = 5;
            this.Gold = 10;

            this.ImageName = "MagmaMephit.PNG";
            this.Name = "Magma Mephit";

            DropableItems = new Container<ItemQuantity>(2);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(3) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3012, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1001, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +3 to hit, reach 5 ft., one creature. 
        //   Hit: 1d4+1 slashing damage plus 1d4 fire damage.
        public string Claws(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 3);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int slashDamage = Dice.Roll(1, 4, 1), fireDamage = Dice.Roll(4);
                def.CurrentHP -= (slashDamage + fireDamage);
                return "MagmaMephit uses Claws against " + def.Name +
                        " for " + slashDamage + " slashing and " + fireDamage + " fire damage!";
            }
            else
            {
                return "MagmaMephit fails to hit " + def.Name + "!";
            }
        }

        // Recharge 6. Defendant must succeed on a DC 11 Dexterity saving throw, taking 2d6 fire damage on
        // a failed save, or half as much on a successful one. Saving throw ignores modifiers for simplicity.
        public string FireBreath(Creature def)
        {
            int fireDamage = Dice.Roll(2, 6);
            int savingThrow = Dice.Roll(20);
            if (savingThrow >= 11)
            {
                def.CurrentHP -= (fireDamage / 2);
                return "MagmaMephit uses Fire Breath against " + def.Name + ".\n  " + def.Name +
                    " successfully dodges and only takes " + (fireDamage / 2) + " fire damage!";
            }
            else
            {
                def.CurrentHP -= fireDamage;
                return "MagmaMephit uses Fire Breath against " + def.Name + ".\n  " + def.Name +
                    " fails to dodge and takes " + (fireDamage) + " fire damage!";
            }
        }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Claws\n  Fire Breath";
        }

        public override string Attack(Creature c)
        {
            if (Dice.Roll(6) == 6)
            {
                return FireBreath(c);
            }
            else
            {
                return Claws(c);
            }
        }
    }
}

