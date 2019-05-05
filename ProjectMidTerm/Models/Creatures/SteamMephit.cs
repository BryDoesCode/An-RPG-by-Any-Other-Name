using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectMidTerm.Models.Creatures
{
    class SteamMephit : Elemental
    {
        public SteamMephit(int x, int y, Direction facing) : base()
        {
            this.Strength = 5;
            this.Dexterity = 11;
            this.Constitution = 10;
            this.Intelligence = 11;
            this.Wisdom = 10;
            this.Charisma = 12;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            this.ResistanceToFire = 100;

            //HP 6d6
            this.CurrentHP = Dice.Roll(6, 6);
            this.MaxHP = CurrentHP;

            this.ArmorClass = 10;

            this.ExperiencePoints = 5;
            this.Gold = 10;

            this.ImageName = "SteamMephit.PNG";
            this.Name = "Steam Mephit";

            DropableItems = new Container<ItemQuantity>(2);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(3) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3017, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1001, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1002, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +2 to hit, reach 5 ft., one creature. 
        //   Hit: 1d4 slashing damage plus 1d4 fire damage.
        public string Claws(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 2);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int slashDamage = Dice.Roll(4), fireDamage = Dice.Roll(4);
                def.CurrentHP -= (slashDamage + fireDamage);
                return "SteamMephit uses Claws against " + def.Name +
                        " for " + slashDamage + " slashing and " + fireDamage + " fire damage!";
            }
            else
            {
                return "SteamMephit fails to hit " + def.Name + "!";
            }
        }

        // Recharge 6. Defendant must succeed on a DC 10 Dexterity saving throw, taking 4 1d8 fire damage on
        // a failed save, or half as much on a successful one. Saving throw ignores modifiers for simplicity.
        public string SteamBreath(Creature def)
        {
            int fireDamage = Dice.Roll(4, 8);
            int savingThrow = Dice.Roll(20);
            if (savingThrow >= 10)
            {
                def.CurrentHP -= (fireDamage / 2);
                return "SteamMephit uses Steam Breath against " + def.Name + ".\n  " + def.Name +
                    " successfully dodges and only takes " + (fireDamage / 2) + " fire damage!";
            }
            else
            {
                def.CurrentHP -= fireDamage;
                return "SteamMephit uses Steam Breath against " + def.Name + ".\n  " + def.Name +
                    " fails to dodge and takes " + (fireDamage) + " fire damage!";
            }
        }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Claws\n  Steam Breath";
        }

        public override string Attack(Creature c)
        {
            if (Dice.Roll(6) == 6)
            {
                return SteamBreath(c); 
            }
            else
            {
                return Claws(c);
            }
        }
    }
}

