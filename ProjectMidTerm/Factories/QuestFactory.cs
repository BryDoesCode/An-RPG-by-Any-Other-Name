using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Models;

namespace ProjectMidTerm.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory()
        {
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            _quests.Add(new Quest(1, "How does your garden grow?", "Defeat Awakened Shrubs.", new List<ItemQuantity> { new ItemQuantity(3001, 5) }, 25, 10, new List<ItemQuantity> { new ItemQuantity(1001, 1) }));
            _quests.Add(new Quest(2, "Don't blink.", "Defeat Blink Dogs.", new List<ItemQuantity> { new ItemQuantity(3005, 10) }, 40, 20, new List<ItemQuantity> { new ItemQuantity(1003, 1) }));

        }

        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }

    
}
