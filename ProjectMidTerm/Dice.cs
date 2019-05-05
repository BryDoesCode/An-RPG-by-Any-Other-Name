using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm
{
    static class Dice
    {
        /* fields */

        private static Random rng = new Random();

        /* methods */

        public static int Roll(int sides)
        {
            return rng.Next(sides) + 1;
        }

        public static int Roll(int numDice, int sides)
        {
            int total = 0;
            for (int die = 0; die < numDice; die++)
            {
                total += Roll(sides);
            }
            return total;
        }

        public static int Roll(int numDice, int sides, int bonus)
        {
            return Roll(numDice, sides) + bonus;
        }
    }
}
