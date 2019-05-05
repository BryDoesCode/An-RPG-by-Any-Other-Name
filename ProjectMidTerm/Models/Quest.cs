using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Interfaces;

namespace ProjectMidTerm.Models
{
    public class Quest : ILocatable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ItemQuantity> ItemsToComplete { get; set; }

        public List<ItemQuantity> RewardItems { get; set; }
        public int RewardGold { get; set; }
        public int RewardXP { get; set; }

        public Quest(int id, string name, string description, List<ItemQuantity> itemsToComplete, 
            int rewardXP, int rewardGold, List<ItemQuantity> rewardItems)
        {
            ID = id;
            Name = name;
            Description = description;
            ItemsToComplete = itemsToComplete;
            RewardXP = rewardXP;
            RewardGold = rewardGold;
            RewardItems = rewardItems;
        }

    }
}
