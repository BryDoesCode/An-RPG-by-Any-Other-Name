using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Models.Creatures
{
    class VioletFungus : Plant
    {
        public VioletFungus(int x, int y, Direction facing) : base()
        {
            this.Strength = 3;
            this.Dexterity = 1;
            this.Constitution = 10;
            this.Intelligence = 1;
            this.Wisdom = 3;
            this.Charisma = 1;

            this.X = x;
            this.Y = y;
            this.Facing = facing;

            //HP 4d8
            this.CurrentHP = Dice.Roll(4, 8);
            this.MaxHP = CurrentHP;

            this.ArmorClass = 5;

            this.ExperiencePoints = 5;
            this.Gold = 12;

            this.ImageName = "VioletFungus.PNG";
            this.Name = "Violet Fungus";

            DropableItems = new Container<ItemQuantity>(3);

            for (int i = 0; i < DropableItems.FixedCapacity; i++)
            {
                if (Dice.Roll(3) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3018, 1));
                }
                else if (Dice.Roll(5) == 1)
                {
                    DropableItems.Add(new ItemQuantity(1002, 1));
                }
                else if (Dice.Roll(4) == 1)
                {
                    DropableItems.Add(new ItemQuantity(3019, 1));
                }
            }
        }

        /* methods */

        //   Melee Weapon Attack: +2 to hit, reach 10 ft., one creature.
        //   Hit: 1d8 necrotic damage.
        public string RottingTouch(Creature def)
        {
            int toHit = Dice.Roll(1, 20, 2);
            if (toHit > def.ArmorClass || toHit == 20)
            {
                int damage = Dice.Roll(8);
                def.CurrentHP -= damage;
                return "VioletFungus uses Rotting Touch against " + def.Name +
                        " for " + damage + " necrotic damage!";
            }
            else
            {
                return "VioletFungus fails to hit " + def.Name + "!";
            }
        }

        public string Multiattack(Creature def)
        {
            int attackAmount = Dice.Roll(4);
            StringBuilder attackString = new StringBuilder("Violet Fungus uses Multiattack.\n");
            for (int i = 0; i < attackAmount; i++)
            {
                attackString.AppendLine(RottingTouch(def));
            }
            return attackString.ToString();
            }

        public override string ToString()
        {
            return base.ToString()
                + "\nAttacks:\n  Rotting Touch\n  Multiattack";
        }

        public override string Attack(Creature c)
        {
            if (Dice.Roll(3) == 1)
            {
                return Multiattack(c);
            }
            else
            {
                return RottingTouch(c);
            }
        }
    }
}
