using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Models.Items;
using ProjectMidTerm.ViewModels;
using ProjectMidTerm.Enums;

namespace ProjectMidTerm.Factories
{
    public static class ItemFactory
    {
        private static List<Item> _standardItems;

        static ItemFactory()
        {
            _standardItems = new List<Item>();
            // -99 x, -99 y considered null "holding" location.
            // 99 x, 99 y considered in the player's inventory.  
            // Direction.NORTH considered a placeholder when facing doesn't matter. 
            _standardItems.Add(new HealingPotion(1001, "Lesser Health Potion", 5, "Heals for 5 HP", 5, 1, 1.0f ));
            _standardItems.Add(new HealingPotion(1002, "Minor Health Potion", 10, "Heals for 10 HP", 10, 1, .8f));
            _standardItems.Add(new HealingPotion(1003, "Regular Health Potion", 15, "Heals for 15 HP", 10, 1, .8f));

            _standardItems.Add(new ExperiencePotion(1101, "Lesser XP Potion", 5, "Gives 5 XP", 5, 1, 1.0f));
            _standardItems.Add(new ExperiencePotion(1102, "Minor XP Potion", 10, "Gives 10 XP", 10, 1, .5f));
            _standardItems.Add(new ExperiencePotion(1103, "Giant XP Potion", 30, "Gives 30 XP", 33, 1, .3f));
            _standardItems.Add(new ExperiencePotion(1104, "MASSIVE XP Potion", 50, "Gives 50 XP", 33, 1, .2f));

            _standardItems.Add(new Sword(2000, "Bare Hands", 0, 0, "bludgeoning", "Just bare hands.", 0, 1, 1.0f));
            _standardItems.Add(new Sword(2001, "Sharpened Stick", 1, 2, "slashing", "A sharpened stick.", 1, 10, 1.0f));
            _standardItems.Add(new Sword(2002, "Wooden Sword", 2, 4, "slashing", "At least it has sword in its name.", 1, 10, 1.0f));
            _standardItems.Add(new Sword(2003, "Kitchen Knife", 3, 6, "slashing", "The onions won't know what hit them.", 1, 10, 1.0f));
            _standardItems.Add(new Sword(2004, "An Actual Sword", 7, 11, "slashing", "Like the professionals use.", 1, 10, 1.0f));
            _standardItems.Add(new Sword(2006, "Excalibur Replica", 15, 19, "slashing", "It's just like the real thing and half the price.", 1, 10, 1.0f));

            _standardItems.Add(new Orb(2101, "Oversized Marble", 1, 2, "force", "Just a big, clear marble.", 1, 10, 1.0f));
            _standardItems.Add(new Orb(2102, "Fish Bowl", 2, 4, "force", "Don't forget to feed them.", 1, 10, 1.0f));
            _standardItems.Add(new Orb(2103, "Plant Terrarium", 3, 5, "force", "The succulents want you to win.", 1, 10, 1.0f));
            _standardItems.Add(new Orb(2105, "Minature Planet", 8, 12, "force", "You have the lives of millions in your hands. Literally.", 1, 10, 1.0f));
            _standardItems.Add(new Orb(2106, "Contained Black Hole", 14, 18, "force", "Just don't break the container.", 1, 10, 1.0f));

            _standardItems.Add(new MonsterDrop(3001, "Fertilizer", "Makes plants wake up. Does nothing for you.", 5));
            _standardItems.Add(new MonsterDrop(3002, "Knee", "They stole it from the knights they killed.", 10));
            _standardItems.Add(new MonsterDrop(3003, "Tall Fertilizer", "Makes taller plants wake up. Does nothing for you--you aren't tall.", 10));
            _standardItems.Add(new MonsterDrop(3004, "Leaf", "Because you didn't leave it alone.", 15));
            _standardItems.Add(new MonsterDrop(3005, "Eyedrops", "Stay moisturized.", 15));
            _standardItems.Add(new MonsterDrop(3006, "Bone", "We don't know whose.", 20));
            _standardItems.Add(new MonsterDrop(3007, "Autograph", "Dryads are famous.", 50));
            _standardItems.Add(new MonsterDrop(3008, "Wet Minus", "Because it's its foil.", 20));
            _standardItems.Add(new MonsterDrop(3009, "Dust", "What did you expect?", 5));
            _standardItems.Add(new MonsterDrop(3010, "Ice", "What did you expect?", 5));
            _standardItems.Add(new MonsterDrop(3011, "Something", "You can't see it anyway.", 50));
            _standardItems.Add(new MonsterDrop(3012, "Rocks", "The magma cools fast.", 5));
            _standardItems.Add(new MonsterDrop(3013, "Pipe", "Just like all the old stories.", 15));
            _standardItems.Add(new MonsterDrop(3014, "Wine", "It's a vintage '47", 40));
            _standardItems.Add(new MonsterDrop(3015, "Dirt", "You took the ground right out from under it.", 5));
            _standardItems.Add(new MonsterDrop(3016, "Wing", "It flies no more.", 10));
            _standardItems.Add(new MonsterDrop(3017, "Jar of Air", "I've got a jar of air. I've got a jar of air.", 5));
            _standardItems.Add(new MonsterDrop(3018, "Poison", "Except it isn't poisonous.", 5));
            _standardItems.Add(new MonsterDrop(3019, "Dye", "Because as chance would have it, it's dead.", 10));



        }

        public static Item CreateItem(int itemID, int x, int y)
        {
            Item standardItem = _standardItems.FirstOrDefault(item => item.ItemID == itemID);

            if(standardItem != null)
            {
                
                if (standardItem is Weapon)
                {
                    if (standardItem is Sword)
                    {
                        Sword item = (standardItem as Sword).Clone();
                        item.UpdateLocation(x, y);
                        return item;
                    }
                    else if (standardItem is Orb)
                    {
                        Orb item = (standardItem as Orb).Clone();
                        item.UpdateLocation(x, y);
                        return item;
                    }

                }
                if (standardItem is Potion)
                {
                    if (standardItem is HealingPotion)
                    {
                        HealingPotion item = (standardItem as HealingPotion).Clone();
                        item.UpdateLocation(x, y);
                        return item;
                    }
                    else if (standardItem is ExperiencePotion)
                    {
                        ExperiencePotion item = (standardItem as ExperiencePotion).Clone();
                        item.UpdateLocation(x, y);
                        return item;
                    }

                }
                if (standardItem is MonsterDrop)
                {
                    MonsterDrop item = (standardItem as MonsterDrop).Clone();
                    item.UpdateLocation(x, y);
                    return item;
                }
                
            }

            return null;
        }
    }
}
