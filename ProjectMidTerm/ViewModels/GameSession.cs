using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Models.Creatures;
using ProjectMidTerm.Factories;
using ProjectMidTerm.Enums;
using ProjectMidTerm.Models;
using ProjectMidTerm.Models.Items;
using ProjectMidTerm.EventArgs;

namespace ProjectMidTerm.ViewModels
{
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;

        #region Properties

        private Races _selectedRaces;
        private Location _currentLocation;
        private Creature _currentMonster;        
        private bool _strEnabled;
        private bool _dexEnabled;
        private bool _conEnabled;
        private bool _intEnabled;
        private bool _wisEnabled;
        private bool _chaEnabled;
        private bool _isTiefling;
        private bool _isHalfElf;
        private bool _hasCreationError;
        private bool _attackEnabled;
        private string _creationErrorMessage;

        
        public World CurrentWorld { get; set; }
        public static PCharacter CurrentPlayer { get; set; }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged("CurrentLocation");
                OnPropertyChanged("HasLocationToNorth");
                OnPropertyChanged("HasLocationToEast");
                OnPropertyChanged("HasLocationToWest");
                OnPropertyChanged("HasLocationToSouth");
                
            }
        }

        public Creature CurrentMonster
        {
            get { return _currentMonster; }
            set
            {
                _currentMonster = value;
                OnPropertyChanged("CurrentMonster");
            }
        }

        public ObservableCollection<Item> CurrentItems { get; set; }
        
        public ObservableCollection<int> STRStats { get; set; }
        public ObservableCollection<int> DEXStats { get; set; }
        public ObservableCollection<int> CONStats { get; set; }
        public ObservableCollection<int> INTStats { get; set; }
        public ObservableCollection<int> WISStats { get; set; }
        public ObservableCollection<int> CHAStats { get; set; }

        public ObservableCollection<int> StatsRolled { get; set; }

        public ObservableCollection<string> TieflingBonusStatOptions { get; set; }
        public ObservableCollection<string> HalfElfBonusStatOptionsOne { get; set; }
        public ObservableCollection<string> HalfElfBonusStatOptionsTwo { get; set; }

        public string TieflingBonusStat { get; set; }
        public string HalfElfBonusStatOne { get; set; }
        public string HalfElfBonusStatTwo { get; set; }

        public string CreationErrorMessage
        {
            get { return _creationErrorMessage; }
            set
            {
                _creationErrorMessage = value;
                OnPropertyChanged("CreationErrorMessage");
            }
        }

        public bool STREnabled
        {
            get { return _strEnabled; }
            set
            {
                _strEnabled = value;
                OnPropertyChanged("STREnabled");
            }
        }

        public bool DEXEnabled
        {
            get { return _dexEnabled; }
            set
            {
                _dexEnabled = value;
                OnPropertyChanged("DEXEnabled");
            }
        }

        public bool CONEnabled
        {
            get { return _conEnabled; }
            set
            {
                _conEnabled = value;
                OnPropertyChanged("CONEnabled");
            }
        }

        public bool INTEnabled
        {
            get { return _intEnabled; }
            set
            {
                _intEnabled = value;
                OnPropertyChanged("intEnabled");
            }
        }

        public bool WISEnabled
        {
            get { return _wisEnabled; }
            set
            {
                _wisEnabled = value;
                OnPropertyChanged("WISEnabled");
            }
        }

        public bool CHAEnabled
        {
            get { return _chaEnabled; }
            set
            {
                _chaEnabled = value;
                OnPropertyChanged("CHAEnabled");
            }
        }

        public bool IsTiefling
        {
            get { return _isTiefling; }
            set
            {
                _isTiefling = (CurrentPlayer.Race == Races.Tiefling);
                OnPropertyChanged("IsTiefling");
            }
        }

        public bool IsHalfElf
        {
            get { return _isHalfElf; }
            set
            {
                _isHalfElf = (CurrentPlayer.Race == Races.HalfElf);
                OnPropertyChanged("IsHalfElf");
            }
        }

        public bool HasCreationError
        {
            get { return _hasCreationError; }
            set
            {
                _hasCreationError = value;
                OnPropertyChanged("HasCreationError");
            }
        }
        
        public bool AttackEnabled
        {
            get { return _attackEnabled; }
            set
            {
                _attackEnabled = value;
                OnPropertyChanged("AttackEnabled");
            }
        }

        public bool HasLocationToNorth =>
            CurrentWorld.LocationAt(CurrentLocation.X, CurrentLocation.Y + 1) != null;
        public bool HasLocationToEast =>
            CurrentWorld.LocationAt(CurrentLocation.X + 1, CurrentLocation.Y) != null;
        public bool HasLocationToSouth =>
            CurrentWorld.LocationAt(CurrentLocation.X, CurrentLocation.Y - 1) != null;
        public bool HasLocationToWest =>
            CurrentWorld.LocationAt(CurrentLocation.X - 1, CurrentLocation.Y) != null;

        public int SelectedStat { get; set; }

        public Races SelectedRaces
        {
            get { return _selectedRaces; }
            set
            {
                _selectedRaces = value;
                CurrentPlayer.Race = value;
                IsTiefling = (CurrentPlayer.Race == Races.Tiefling);
                IsHalfElf = (CurrentPlayer.Race == Races.HalfElf);
                OnPropertyChanged("SelectedRaces");
            }
        }

        public IEnumerable<Races> RacesValues
        {
            get
            {
                return Enum.GetValues(typeof(Races))
                    .Cast<Races>();
            }
        }
        #endregion

        public GameSession()
        {
            STREnabled = true;
            STRStats = new ObservableCollection<int>();
            DEXStats = new ObservableCollection<int>();
            CONStats = new ObservableCollection<int>();
            INTStats = new ObservableCollection<int>();
            WISStats = new ObservableCollection<int>();
            CHAStats = new ObservableCollection<int>();
            StatsRolled = new ObservableCollection<int>();
            TieflingBonusStatOptions = new ObservableCollection<string> { "CHA", "DEX" };
            TieflingBonusStat = "";
            HalfElfBonusStatOptionsOne = new ObservableCollection<string> { "STR", "DEX", "CON", "INT", "WIS" };
            HalfElfBonusStatOptionsTwo = new ObservableCollection<string> { "STR", "DEX", "CON", "INT", "WIS" };
            HalfElfBonusStatOne = "";
            HalfElfBonusStatTwo = "";
            HasCreationError = false;
            CreationErrorMessage = "Something went wrong!\n\n" +
                "Do you have a name? Is it less than 15 characters?\nDid you forget to assign all your stats?\nDid you select duplicate bonus options for HalfElf?\n(Don't do that. Any of that.)" +
                "\n\nDouble check that you've entered all your character information correctly.\nFix it and try to save again.";
            RollStats();

            CurrentPlayer = new PCharacter
            {
                Name = "",
                Gold = 0,
                ExperiencePoints = 0,
                Level = 1,
                Race = _selectedRaces
            };

            CurrentPlayer.ChooseWeapon(ItemFactory.CreateItem(2000, 99, 99) as Weapon);

            CurrentWorld = WorldFactory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(CurrentPlayer.X, CurrentPlayer.Y);
            CurrentMonster = CurrentWorld.MonsterAt(CurrentPlayer.X, CurrentPlayer.Y);
            CurrentItems = CurrentWorld.ItemsAt(CurrentPlayer.X, CurrentPlayer.Y);

            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1001, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1001, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1001, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1002, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1002, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1003, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1003, 99, 99));

            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1101, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1101, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1102, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1102, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1103, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1103, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1103, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(1104, 99, 99));

            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(2001, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(2002, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(2003, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(2004, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(2006, 99, 99));

            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(2101, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(2102, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(2103, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(2105, 99, 99));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateItem(2106, 99, 99));

            AttackEnabled = true;
        }
               

        #region Movement
        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentPlayer.MoveNorth();
                CurrentLocation = CurrentWorld.LocationAt(CurrentPlayer.X, CurrentPlayer.Y);
                CurrentMonster = CurrentWorld.MonsterAt(CurrentPlayer.X, CurrentPlayer.Y);
                CurrentItems = CurrentWorld.ItemsAt(CurrentPlayer.X, CurrentPlayer.Y);
                RaiseMessage($"\n{CurrentPlayer.Name} has moved NORTH to the {CurrentLocation.Name}.");
                RaiseMessage(CurrentLocation.Description);
                if (CurrentMonster != null)
                {
                    RaiseMessage($"     There is a wild {CurrentMonster.Name} here.");
                }
                if (CurrentItems.Count > 0)
                {
                    foreach (Item item in CurrentItems)
                    {
                        RaiseMessage($"     There is a {item.Name} on the ground.");
                    }
                }
                CompleteQuestAtLocation();
                GivePlayerQuestsAtLocation();
            }
        }

        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentPlayer.MoveEast();
                CurrentLocation = CurrentWorld.LocationAt(CurrentPlayer.X, CurrentPlayer.Y);
                CurrentMonster = CurrentWorld.MonsterAt(CurrentPlayer.X, CurrentPlayer.Y);
                CurrentItems = CurrentWorld.ItemsAt(CurrentPlayer.X, CurrentPlayer.Y);
                RaiseMessage($"\n{CurrentPlayer.Name} has moved EAST to the {CurrentLocation.Name}.");
                RaiseMessage(CurrentLocation.Description);
                if (CurrentMonster != null)
                {
                    RaiseMessage($"     There is a wild {CurrentMonster.Name} here.");
                }
                if (CurrentItems.Count > 0)
                {
                    foreach (Item item in CurrentItems)
                    {
                        RaiseMessage($"     There is a {item.Name} on the ground.");
                    }
                }

                CompleteQuestAtLocation();
                GivePlayerQuestsAtLocation();
            }
        }

        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentPlayer.MoveSouth();
                CurrentLocation = CurrentWorld.LocationAt(CurrentPlayer.X, CurrentPlayer.Y);
                CurrentMonster = CurrentWorld.MonsterAt(CurrentPlayer.X, CurrentPlayer.Y);
                CurrentItems = CurrentWorld.ItemsAt(CurrentPlayer.X, CurrentPlayer.Y);
                RaiseMessage($"\n{CurrentPlayer.Name} has moved SOUTH to the {CurrentLocation.Name}.");
                RaiseMessage(CurrentLocation.Description);
                if (CurrentMonster != null)
                {
                    RaiseMessage($"     There is a wild {CurrentMonster.Name} here.");
                }
                if (CurrentItems.Count > 0)
                {
                    foreach (Item item in CurrentItems)
                    {
                        RaiseMessage($"     There is a {item.Name} on the ground.");
                    }
                }
                CompleteQuestAtLocation();
                GivePlayerQuestsAtLocation();
            }
        }

        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                CurrentPlayer.MoveWest();
                CurrentLocation = CurrentWorld.LocationAt(CurrentPlayer.X, CurrentPlayer.Y);
                CurrentMonster = CurrentWorld.MonsterAt(CurrentPlayer.X, CurrentPlayer.Y);
                CurrentItems = CurrentWorld.ItemsAt(CurrentPlayer.X, CurrentPlayer.Y);
                RaiseMessage($"\n{CurrentPlayer.Name} has moved WEST to the {CurrentLocation.Name}.");
                RaiseMessage(CurrentLocation.Description);
                if (CurrentMonster != null)
                {
                    RaiseMessage($"     There is a wild {CurrentMonster.Name} here.");
                }
                if (CurrentItems.Count > 0)
                {
                    foreach (Item item in CurrentItems)
                    {
                        RaiseMessage($"     There is a {item.Name} on the ground.");
                    }
                }
                CompleteQuestAtLocation();
                GivePlayerQuestsAtLocation();
            }
        }

        public void TurnLeft()
        {
            CurrentPlayer.Turn(-90);
            RaiseMessage($"{CurrentPlayer.Name} turned to the left.");
            RaiseMessage($"{CurrentPlayer.Name} is now facing {Enum.GetName(typeof(Direction), CurrentPlayer.Facing)}.");
        }

        public void TurnRight()
        {
            CurrentPlayer.Turn(90);
            RaiseMessage($"{CurrentPlayer.Name} turned to the right.");
            RaiseMessage($"{CurrentPlayer.Name} is now facing {Enum.GetName(typeof(Direction), CurrentPlayer.Facing)}.");
        }

        public void MoveForward()
        {
            switch(CurrentPlayer.Facing)
            {
                case Direction.NORTH:
                    if(HasLocationToNorth)
                    {
                        MoveNorth();
                    }
                    else
                    {
                        RaiseMessage("There's nothing in that direction. The world just ends.");
                    }
                    break;
                case Direction.EAST:
                    if (HasLocationToEast)
                    {
                        MoveEast();
                    }
                    else
                    {
                        RaiseMessage("There's nothing in that direction. The world just ends.");
                    }
                    break;
                case Direction.SOUTH:
                    if (HasLocationToSouth)
                    {
                        MoveSouth();
                    }
                    else
                    {
                        RaiseMessage("There's nothing in that direction. The world just ends.");
                    }
                    break;
                case Direction.WEST:
                    if (HasLocationToWest)
                    {
                        MoveWest();
                    }
                    else
                    {
                        RaiseMessage("There's nothing in that direction. The world just ends.");
                    }
                    break;
                default:
                    RaiseMessage("{CurrentPlayer.Name} can't do that.");
                    break;
            }
        }

        public void TeleportTo(int x, int y)
        {
            CurrentPlayer.GoTo(x, y);
            CurrentLocation = CurrentWorld.LocationAt(CurrentPlayer.X, CurrentPlayer.Y);
            CurrentMonster = CurrentWorld.MonsterAt(CurrentPlayer.X, CurrentPlayer.Y);
            CurrentItems = CurrentWorld.ItemsAt(CurrentPlayer.X, CurrentPlayer.Y);
            RaiseMessage($"\n{CurrentPlayer.Name} has teleported to the {CurrentLocation.Name}.");
            RaiseMessage(CurrentLocation.Description);
            if (CurrentMonster != null)
            {
                RaiseMessage($"     There is a wild {CurrentMonster.Name} here.");
            }
            if (CurrentItems.Count > 0)
            {
                foreach (Item item in CurrentItems)
                {
                    RaiseMessage($"     There is a {item.Name} on the ground.");
                }
            }
            CompleteQuestAtLocation();
            GivePlayerQuestsAtLocation();
        }
        #endregion

        #region Quests

        private void GivePlayerQuestsAtLocation()
        {
            foreach(Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                if(!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                    RaiseMessage($"{CurrentPlayer.Name} received the quest: {quest.Name}");
                    RaiseMessage($"     {quest.Description}");
                    RaiseMessage($"     Collect: ");
                    
                    foreach(ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        RaiseMessage($"          {itemQuantity.Quantity} {ItemFactory.CreateItem(itemQuantity.ItemID, -99, -99).Name}");
                    }

                    RaiseMessage("     Rewards: ");
                    RaiseMessage($"          {quest.RewardXP} XP\n          {quest.RewardGold} Gold");
                    foreach(ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($"          {itemQuantity.Quantity} {ItemFactory.CreateItem(itemQuantity.ItemID, -99, -99).Name}");
                    }

                }
            }
        }

        private void CompleteQuestAtLocation()
        {
            foreach(Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                QuestStatus questToComplete = CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID && !q.IsCompleted);

                if(questToComplete != null){
                    if(CurrentPlayer.HasItems(quest.ItemsToComplete))
                    {
                        foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                        {
                            for(int i = 0; i < itemQuantity.Quantity; i++)
                            {
                                CurrentPlayer.RemoveItemFromInventory(CurrentPlayer.Inventory.First(item => item.ItemID == itemQuantity.ItemID));
                            }
                        }

                        RaiseMessage($"\n{CurrentPlayer.Name} completed the '{quest.Name}' quest. Congratulations!");
                        RaiseMessage($"{CurrentPlayer.Name} receives:\n     {quest.RewardXP} XP\n     {quest.RewardGold} Gold");
                        CurrentPlayer.GainGold(quest.RewardGold);
                        CurrentPlayer.GainXP(quest.RewardXP);
                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            Item rewardItem = ItemFactory.CreateItem(itemQuantity.ItemID, 99, 99);
                            CurrentPlayer.AddItemToInventory(rewardItem);
                            RaiseMessage($"     {itemQuantity.Quantity} {rewardItem.Name}");
                        }

                        questToComplete.IsCompleted = true;
                    }
                }
            }
        }
        #endregion

        #region Items (Loot, Use, Drop, Etc.)

        public void UseItem(Item item)
        {
            RaiseMessage(CurrentPlayer.Use(item));
        }

        public void DropItem(Item item)
        {
            RaiseMessage($"{CurrentPlayer.Name} dropped {item.Name}.");
            CurrentWorld.AddItemAtLocation(item.ItemID, CurrentPlayer.X, CurrentPlayer.Y);
            //CurrentItems.Add(item);
            CurrentPlayer.RemoveItemFromInventory(item);
            CurrentItems = CurrentWorld.ItemsAt(CurrentPlayer.X, CurrentPlayer.Y);
            RaiseMessage($"There is a {item.Name} on the ground.");              
            
        }

        public void LootItem(Item item)
        {            
            CurrentPlayer.AddItemToInventory(item);
            item.UpdateLocation(99, 99);
            RaiseMessage($"{CurrentPlayer.Name} looted {item.Name}.");
            CurrentItems.Remove(item);
        }

        #endregion

        public void Rest()
        {
            RaiseMessage(CurrentPlayer.Rest());
        }

        #region Original Character Creation

        public void RollStats()
        {
            int[] dice = new int[4];

            for (int i = 0; i < 6; i++)
            {
                for (int die = 0; die < dice.Length; die++)
                {
                    dice[die] = Dice.Roll(6);
                }
                StatsRolled.Add(dice.Sum() - dice.Min());
                STRStats.Add(StatsRolled[i]); DEXStats.Add(StatsRolled[i]); CONStats.Add(StatsRolled[i]);
                INTStats.Add(StatsRolled[i]); WISStats.Add(StatsRolled[i]); CHAStats.Add(StatsRolled[i]);
            }
        }

        public void StatSelect(string tag)
        {
            if (SelectedStat != 0)
            {
                switch (tag)
                {
                    case "STR":
                        STREnabled = false;
                        DEXEnabled = true;
                        CurrentPlayer.Strength = SelectedStat;
                        DEXStats.Remove(SelectedStat);
                        CONStats.Remove(SelectedStat);
                        INTStats.Remove(SelectedStat);
                        WISStats.Remove(SelectedStat);
                        CHAStats.Remove(SelectedStat);
                        SelectedStat = 0;
                        break;
                    case "DEX":
                        DEXEnabled = false;
                        CONEnabled = true;
                        CurrentPlayer.Dexterity = SelectedStat;
                        CONStats.Remove(SelectedStat);
                        INTStats.Remove(SelectedStat);
                        WISStats.Remove(SelectedStat);
                        CHAStats.Remove(SelectedStat);
                        SelectedStat = 0;
                        break;
                    case "CON":
                        CONEnabled = false;
                        INTEnabled = true;
                        CurrentPlayer.Constitution = SelectedStat;
                        INTStats.Remove(SelectedStat);
                        WISStats.Remove(SelectedStat);
                        CHAStats.Remove(SelectedStat);
                        SelectedStat = 0;
                        break;
                    case "INT":
                        INTEnabled = false;
                        WISEnabled = true;
                        CurrentPlayer.Intelligence = SelectedStat;
                        WISStats.Remove(SelectedStat);
                        CHAStats.Remove(SelectedStat);
                        SelectedStat = 0;
                        break;
                    case "WIS":
                        WISEnabled = false;
                        CHAEnabled = true;
                        CurrentPlayer.Wisdom = SelectedStat;
                        CHAStats.Remove(SelectedStat);
                        SelectedStat = 0;
                        break;
                    case "CHA":
                        CHAEnabled = false;
                        CurrentPlayer.Charisma = SelectedStat;
                        SelectedStat = 0;
                        break;
                    default:
                        break;
                }
            }
        }

        public bool InvalidInput()
        {
            if (CurrentPlayer.Race == Races.Tiefling)
            {
                HasCreationError = (STREnabled || DEXEnabled || CONEnabled || INTEnabled && WISEnabled || CHAEnabled
                    || TieflingBonusStat.Length <= 0 || CurrentPlayer.Name.Length > 15 || CurrentPlayer.Name.Length <= 0);
                return HasCreationError;
            }
            if (CurrentPlayer.Race == Races.HalfElf)
            {
                HasCreationError = (STREnabled || DEXEnabled || CONEnabled || INTEnabled || WISEnabled || CHAEnabled
                    || (HalfElfBonusStatOne.Length <= 0 || HalfElfBonusStatTwo.Length <= 0 || HalfElfBonusStatOne.Contains(HalfElfBonusStatTwo))
                    || CurrentPlayer.Name.Length > 15 || CurrentPlayer.Name.Length <= 0);
                return HasCreationError;
            }
            HasCreationError = (STREnabled || DEXEnabled || CONEnabled || INTEnabled || WISEnabled || CHAEnabled
                || CurrentPlayer.Name.Length > 15 || CurrentPlayer.Name.Length <= 0);
            return HasCreationError;
        }

        public void ResetUI()
        {
            STRStats.Clear();
            DEXStats.Clear();
            CONStats.Clear();
            INTStats.Clear();
            WISStats.Clear();
            CHAStats.Clear();

            STREnabled = true;
            DEXEnabled = false;
            CONEnabled = false;
            INTEnabled = false;
            WISEnabled = false;
            CHAEnabled = false;
        }

        public void ResetValues()
        {
            ResetUI();
            StatsRolled.ToList().ForEach(item =>
            {
                STRStats.Add(item); DEXStats.Add(item); CONStats.Add(item);
                INTStats.Add(item); WISStats.Add(item); CHAStats.Add(item);
            });

        }

        public void RerollStats()
        {
            StatsRolled.Clear();
            ResetUI();
            RollStats();
        }

        public void CalculateRaceBonus()
        {
            if (CurrentPlayer.Race == Races.Tiefling)
            {
                CurrentPlayer.AddRaceStatBonus(TieflingBonusStat, "");
            }

            else if (CurrentPlayer.Race == Races.HalfElf)
            {
                CurrentPlayer.AddRaceStatBonus(HalfElfBonusStatOne, HalfElfBonusStatTwo);
            }
            else
            {
                CurrentPlayer.AddRaceStatBonus("", "");
            }

        }
        #endregion

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }

        public void RaiseGameStartMessage()
        {
            RaiseMessage("Welcome to the Titleless RPG demo!");
            RaiseMessage("You may navigate the world by using the arrows on the sides of the screen, by turning and moving forward, or by clicking on a place in the map.");
            RaiseMessage("This is a demo build, so you are given a variety of items to start out with.");
            RaiseMessage("Enjoy!");
            RaiseMessage("\n");
            RaiseMessage($"The {CurrentLocation.Name}.");
            RaiseMessage($"{CurrentLocation.Description}");
            if (CurrentMonster != null)
            {
                RaiseMessage($"     There is a wild {CurrentMonster.Name} here.");
            }
            if (CurrentItems.Count > 0)
            {
                foreach (Item item in CurrentItems)
                {
                    RaiseMessage($"     There is a {item.Name} on the ground.");
                }
            }
        }

        #region Battle Screen

        public void CreatureAppeared()
        {
            RaiseMessage($"A wild {CurrentMonster.Name} has appeared!");
        }

        public void AttackCurrentMonster()
        {
            if (CurrentPlayer.CurrentWeapon == null)
            {
                RaiseMessage($"{CurrentPlayer.Name} must select a weapon to attack!");
            }

            else if (CurrentPlayer.Initiative > CurrentMonster.Initiative)
            {
                RaiseMessage(CurrentPlayer.Attack(CurrentMonster));
                CheckHP();
                if (AttackEnabled)
                {
                    RaiseMessage(CurrentMonster.Attack(CurrentPlayer));
                    CheckHP();
                }
            }
            else
            {
                RaiseMessage(CurrentMonster.Attack(CurrentPlayer));
                CheckHP();
                if (AttackEnabled)
                {
                    RaiseMessage(CurrentPlayer.Attack(CurrentMonster));
                    CheckHP();
                }
            }

        }
        
        public void DefendAgainstCurrentMonster()
        {
            RaiseMessage(CurrentPlayer.DefendAgainst(CurrentMonster));
            RaiseMessage(CurrentMonster.Attack(CurrentPlayer));
            RaiseMessage(CurrentPlayer.DropDefense());
            CheckHP();
        }

        public void CheckHP()
        {            
            if (CurrentPlayer.CurrentHP <= 0)
            {
                CurrentPlayer.CurrentHP = 0;
                int xpLoss = CurrentPlayer.ExperiencePoints / 10; // Lose 10% XP
                int goldLoss = CurrentPlayer.Gold / 20; // Lose 5% Gold

                RaiseMessage($"{CurrentPlayer.Name} died!");
                RaiseMessage(CurrentPlayer.LoseXP(xpLoss));
                RaiseMessage(CurrentPlayer.LoseGold(goldLoss));                

                CurrentPlayer.isDead = true;
                AttackEnabled = false;
            }
            if (CurrentMonster.CurrentHP <= 0)
            {
                CurrentMonster.CurrentHP = 0;
                
                RaiseMessage($"Defender {CurrentMonster.Name} died!");
                RaiseMessage(CurrentPlayer.GainXP(CurrentMonster.ExperiencePoints));
                RaiseMessage(CurrentPlayer.GainGold(CurrentMonster.Gold));

                for(int i = 0; i < (CurrentMonster as Monster).DropableItems.Length(); i++)
                {
                    ItemQuantity itemQuantity = (CurrentMonster as Monster).DropableItems[i];
                    if (itemQuantity != null)
                    {
                        Item item = ItemFactory.CreateItem(itemQuantity.ItemID, 99, 99);
                        CurrentPlayer.AddItemToInventory(item);
                        RaiseMessage($"{CurrentPlayer.Name} received {itemQuantity.Quantity} {item.Name}.");
                    }
                }

                CurrentMonster.isDead = true;
                AttackEnabled = false;
            }
        }
        public void CheckDeath()
        {
            if (CurrentPlayer.isDead)
            {
                CurrentPlayer.Revive();
                CurrentLocation = CurrentWorld.LocationAt(CurrentPlayer.X, CurrentPlayer.Y);
                CurrentMonster = CurrentWorld.MonsterAt(CurrentPlayer.X, CurrentPlayer.Y);
                RaiseMessage($"\n{CurrentPlayer.Name} wakes up in the {CurrentLocation.Name} with no memory of what happened.");
            }
            else if (CurrentMonster.isDead)
            {
                CurrentWorld.RemoveMonster(CurrentMonster);
                CompleteQuestAtLocation();
                CurrentMonster = CurrentWorld.MonsterAt(CurrentPlayer.X, CurrentPlayer.Y);
                CurrentMonster.isDead = false;
                if (CurrentMonster != null)
                {
                    RaiseMessage($"\nThere is a wild {CurrentMonster.Name} here.");
                }
            }
        }  
        
        // Temp Sparing match as proof of concept. 
        public void SparringMatch()
        {
            RaiseMessage($"Pretend Fight between {CurrentPlayer.Name} and {CurrentMonster.Name}.");
            while ((CurrentPlayer.CurrentHP > 0) && (CurrentMonster.CurrentHP > 0))
            {
                RaiseMessage(CurrentPlayer.Attack(CurrentMonster));
                RaiseMessage(CurrentMonster.Attack(CurrentPlayer));
            }

            if (CurrentPlayer.CurrentHP <= 0)
            {
                RaiseMessage($"{CurrentPlayer.Name} died!");
            }
            if (CurrentMonster.CurrentHP <= 0)
            {
                RaiseMessage($"Defender {CurrentMonster.Name} died!");
            }
            #endregion

        }
    }
}
