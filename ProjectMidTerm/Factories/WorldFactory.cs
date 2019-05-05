using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Models;

namespace ProjectMidTerm.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new World();
            

            newWorld.AddLocation(-1, 0, "VOID", "What even is this place? A deep nothingness filled with endless possibilities.",
                "VOID.jpg");

            newWorld.LocationAt(-1, 0).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(2));

            newWorld.AddLocation(0, 0, "Forest", "There are fireflies here and a sense of magic lingers in the air. Who knows what creatures may be lurking.", 
                "Forest.jpg");

            newWorld.AddLocation(1, -1, "Inferno", "It's all on fire. Everything.",
                "Inferno.jpg");

            newWorld.AddLocation(1, 0, "Field", "What a pretty field... wait, what's that in the distance?",
                "Field.jpg");

            newWorld.LocationAt(1, 0).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1));

            newWorld.AddLocation(0, 1, "Lake", "Everything looks so peaceful and serene. Who would want to disturb this?",
                "Lake.jpg");

            newWorld.AddLocation(1, 1, "Horizon", "An unknown world lies in the distance. It's just beyond your reach.",
                "Horizon.jpg");

            newWorld.AddMonsters();

            newWorld.AddItems();

            return newWorld;
        }
    }
}
