using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Models.Creatures;
using ProjectMidTerm.Models.Items;
using ProjectMidTerm.Factories;

namespace ProjectMidTerm.Models
{
    public class World
    {
        private readonly List<Location> _locations = new List<Location>();
        private readonly List<Creature> _monstersAvailable = new List<Creature>();
        private readonly List<Item> _itemsAvailable = new List<Item>();

        internal void AddLocation(int x, int y, string name, string description, string imageName)
        {
            Location loc = new Location
            {
                X = x,
                Y = y,
                Name = name,
                Description = description,
                ImageName = $"/ProjectMidTerm;component/Images/Locations/{imageName}"
            };

            _locations.Add(loc);
        }
        internal void AddMonsters()
        {
            AddVOIDMonster();
            AddForestMonster();
            AddInfernoMonster();
            AddFieldMonster();
            AddLakeMonster();
            AddHorizonMonster();

        }

        internal void AddItems()
        {
            AddItemAtLocation(1, 0);
            AddItemAtLocation(1, 0);
            AddItemAtLocation(0, 0);

            if (Dice.Roll(3) == 1)
            {
                AddItemAtLocation(-1, 0);
            }
            else if (Dice.Roll(5) == 1)
            {
                AddItemAtLocation(0, 1);
            }

        }

        internal void AddMonsterAtLocation(int x, int y)
        {
            if(x == -1 && y == 0)
            {
                AddVOIDMonster();
            }
            else if(x== 0 && y == 0)
            {
                AddForestMonster();
            }
            else if(x == 1 && y == -1)
            {
                AddInfernoMonster();
            }
            else if(x == 1 && y == 0)
            {
                AddFieldMonster();
            }
            else if(x == 0 && y == 1)
            {
                AddLakeMonster();
            }
            else if(x == 1 && y == 1)
            {
                AddHorizonMonster();
            }
        }

        public void AddItemAtLocation(int itemID, int x, int y)
        {
            _itemsAvailable.Add(ItemFactory.CreateItem(itemID, x, y));
        }

        internal void AddItemAtLocation(int x, int y)
        {
            _itemsAvailable.Add(ItemFactory.CreateItem(1003, x, y));
            
            if (Dice.Roll(3) == 1)
            {
                _itemsAvailable.Add(ItemFactory.CreateItem(1001, x, y));
            }
            else if (Dice.Roll(2) == 1)
            {
                _itemsAvailable.Add(ItemFactory.CreateItem(1002, x, y));
            }            
        }

        public Creature MonsterAt(int x, int y)
        {
            foreach (Creature mob in _monstersAvailable)
            {
                if (mob.X == x && mob.Y == y)
                {
                    return mob;
                }
            }
            return null;
        }

        public void RemoveMonster(Creature creature)
        {
            int atLocationX = creature.X;
            int atLocationY = creature.Y;
            _monstersAvailable.Remove(creature);
            AddMonsterAtLocation(atLocationX, atLocationY);

        }

        public ObservableCollection<Item> ItemsAt(int x, int y)
        {
            ObservableCollection<Item> itemList = new ObservableCollection<Item>();

            foreach (Item item in _itemsAvailable)
            {
                if (item.X == x && item.Y == y)
                {
                    itemList.Add(item);
                }
            }
            return itemList;
        }

        public void RemoveItem(Item item)
        {
            int atLocationX = item.X;
            int atLocationY = item.Y;
            _itemsAvailable.Remove(item);
            AddItemAtLocation(atLocationX, atLocationY);

        }

        internal void AddVOIDMonster()
        {
            if (Dice.Roll(3) == 1)
            {
                _monstersAvailable.Add(new InvisibleStalker(-1, 0, Direction.NORTH));
            }
            else
            {
                _monstersAvailable.Add(new BlinkDog(-1, 0, Direction.NORTH));

            }
        }

        internal void AddForestMonster()
        {
            if (Dice.Roll(3) == 1)
            {
                _monstersAvailable.Add(new Sprite(0, 0, Direction.NORTH));
            }
            else if (Dice.Roll(2) == 1)
            {
                _monstersAvailable.Add(new Dryad(0, 0, Direction.NORTH));
            }
            else
            {
                _monstersAvailable.Add(new AwakenedTree(0, 0, Direction.NORTH));
            }
        }

        internal void AddInfernoMonster()
        {
            if (Dice.Roll(2) == 1)
            {
                _monstersAvailable.Add(new MagmaMephit(1, -1, Direction.NORTH));
            }
            else
            {
                _monstersAvailable.Add(new DustMephit(1, -1, Direction.NORTH));
            }
        }
        internal void AddFieldMonster()
        {
            if (Dice.Roll(6) == 1)
            {
                _monstersAvailable.Add(new ShamblingMound(1, 0, Direction.NORTH));
            }
            else
            {
                _monstersAvailable.Add(new AwakenedShrub(1, 0, Direction.NORTH));
            }
        }
        internal void AddLakeMonster()
        {
            if (Dice.Roll(2) == 1)
            {
                _monstersAvailable.Add(new SteamMephit(0, 1, Direction.NORTH));
            }
            else
            {
                _monstersAvailable.Add(new IceMephit(0, 1, Direction.NORTH));

            }
        }
        internal void AddHorizonMonster()
        {
            if (Dice.Roll(3) == 1)
            {
                _monstersAvailable.Add(new Satyr(1, 1, Direction.NORTH));
            }
            else
            {
                _monstersAvailable.Add(new VioletFungus(1, 1, Direction.NORTH));

            }
        }

        public Location LocationAt(int x, int y)
        {
            foreach(Location loc in _locations)
            {
                if(loc.X == x && loc.Y == y)
                {
                    return loc;
                }
            }

            return null;
        }
    }
}
