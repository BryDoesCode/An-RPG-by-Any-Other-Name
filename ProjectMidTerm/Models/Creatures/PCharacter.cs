using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Enums;
using System.Collections.ObjectModel;
using ProjectMidTerm.Models.Items;
using ProjectMidTerm.Interfaces;
using ProjectMidTerm.Models;

namespace ProjectMidTerm.Models.Creatures
{
    public class PCharacter : Creature
    {
        
        private Races _race;
        private Weapon _currentWeapon;
        private int _level;
        private int _experienceToLevel;

        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }
        public ObservableCollection<Item> Inventory { get; set; }
        public ObservableCollection<QuestStatus> Quests { get; set; }

        public List<GroupedInventoryItem> Weapons =>
             GroupedInventory.Where(i => i.Item.GetType() == typeof(Sword) || i.Item.GetType() == typeof(Orb)).ToList();
        public List<GroupedInventoryItem> Potions => 
            GroupedInventory.Where(i => i.Item.GetType() == typeof(HealingPotion) || i.Item.GetType() == typeof(ExperiencePotion)).ToList();
        public List<GroupedInventoryItem> 
            Collectables =>
           GroupedInventory.Where(i => i.Item.GetType() == typeof(MonsterDrop)).ToList();

        public Weapon CurrentWeapon
        {
            get { return _currentWeapon; }
            protected set
            {
                _currentWeapon = value;
                OnPropertyChanged();
            }
        }
        
        public new int Constitution
        {
            get
            {
                return attributes[2];
            }

            set
            {
                attributes[2] = value;
                UpdateMaxHP();
                OnPropertyChanged();
            }
        }
        public new int Dexterity
        {
            get
            {
                return attributes[1];
            }

            set
            {
                attributes[1] = value;
                UpdateArmor();
                UpdateInitiative();
                OnPropertyChanged();
            }
        }        

        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged();
            }
        }
        
        public Races Race
        {
            get { return _race; }
            set
            {
                _race = value;
                switch(value)
                {
                    case Races.Dragonborn:
                        this.ImageName = "Dragonborn.PNG";
                        break;
                    case Races.HillDwarf:
                    case Races.MountainDwarf:
                        this.ImageName = "Dwarf.PNG";
                        break;
                    case Races.DarkElf:
                    case Races.HighElf:
                    case Races.WoodElf:
                        this.ImageName = "Elf.PNG";
                        break;
                    case Races.DeepGnome:
                    case Races.ForestGnome:
                    case Races.RockGnome:
                        this.ImageName = "Gnome.PNG";
                        break;
                    case Races.HalfElf:
                        this.ImageName = "HalfElf.PNG";
                        break;
                    case Races.LightfootHalfling:
                    case Races.StoutHalfling:
                        this.ImageName = "Halfling.PNG";
                        break;
                    case Races.HalfOrc:
                        this.ImageName = "HalfOrc.PNG";
                        break;
                    case Races.Human:
                        this.ImageName = "Human.PNG";
                        break;
                    case Races.Tiefling:
                        this.ImageName = "Tiefling.PNG";
                        break;
                }
                
                OnPropertyChanged();
                OnPropertyChanged("ImageName");
            }
        }

        public PCharacter() : base()
        {           

            this.CurrentHP = 10 + (int)Math.Floor((this.Constitution - 10.0d) / 2);
            this.MaxHP = CurrentHP;
            this.DisplayHP = "";
            this.ArmorClass = 10 + (int)Math.Floor((this.Dexterity - 10.0d) / 2);
            this.Initiative = (int)Math.Floor((this.Dexterity - 10.0d) / 2);            
            Inventory = new ObservableCollection<Item>();
            Quests = new ObservableCollection<QuestStatus>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
            _experienceToLevel = 10;

        }

        public void UpdateMaxHP()
        {            
            this.MaxHP = 10 + (int)Math.Floor((this.Constitution - 10.0d) / 2);
            this.CurrentHP = this.MaxHP;
            this.DisplayHP = "";
        }
        
        public void UpdateArmor()
        {
            this.ArmorClass = 10 + (int)Math.Floor((this.Dexterity - 10.0d) / 2);
        }

        public void UpdateInitiative()
        {
            this.Initiative = (int)Math.Floor((this.Dexterity - 10.0d) / 2); 
        }

        public void ChooseWeapon(Weapon weapon)
        {
            this.CurrentWeapon = weapon;
        }

        public string UseWeapon(Creature def)
        {
            if (this.CurrentWeapon == null)
            {
                return $"{this.Name} doesn't have a weapon equipped!";
            }
            int toHit = Dice.Roll(20);
            if (toHit == 20)
            {
                int damage = 2 *(Dice.Roll(1, this.CurrentWeapon.MaxDamage, this.CurrentWeapon.MinDamage - 1) + (this.Strength - (def.Constitution / 2)));
                if (damage <= 0)
                {
                    damage = 1;
                }
                def.CurrentHP -= damage;
                return $"Critical hit! {this.Name} used {this.CurrentWeapon.Name} against {def.Name} for {damage} {CurrentWeapon.DamageType} damage.";
            }
            else if ((toHit + this.Dexterity / 2) > def.Dexterity)
            {
                int damage = (Dice.Roll(1, this.CurrentWeapon.MaxDamage, this.CurrentWeapon.MinDamage -1) + (this.Strength - (def.Constitution / 2)));
                if (damage <= 0)
                {
                    damage = 1;
                }
                def.CurrentHP -= damage;
                return $"{this.Name} used {this.CurrentWeapon.Name} against {def.Name} for {damage} {this.CurrentWeapon.DamageType} damage.";

            }
            else
            {
                return $"{this.Name} failed to hit {def.Name} with {this.CurrentWeapon.Name}.";
            }
        }

        public override string Attack(Creature c)
        {
            return UseWeapon(c);  
        }

        public override string Defend()
        {            
            return $"{this.Name} defends against the air. It was not very effective.";
        }

        public override string DefendAgainst(Creature attacker)
        {
            this.ArmorClass += 4;
            return $"{this.Name} defends against {attacker.Name}. Their armor stat increases.";
        }

        public string DropDefense()
        {
            this.ArmorClass -= 4;
            return $"{this.Name} drops their defense. Their armor returns to normal.";
        }

        public override string Use(IUsable used)
        {                  
            if (used.UsesLeft >= 1)
            {                
                if (used.UseChance < 1.0 && used.UseChance > 0.0)
                {
                    if (Dice.Roll(100) < 100 * used.UseChance)
                    {
                        return used.SuccessMessage();
                    }
                }
                else if (used.UseChance == 1.0)
                {
                    return used.SuccessMessage();
                }
            }            
            return used.FailureMessage();
        }

        public void Heal(int amount)
        {
            this.CurrentHP += amount;
            if (this.CurrentHP > this.MaxHP)
            {
                this.CurrentHP = this.MaxHP;
            }
        }

        public string GainGold(int amount)
        {
            this.Gold += amount;

            return $"{this.Name} gained {amount} Gold!";
        }

        public string LoseGold(int amount)
        {
            this.Gold -= amount;
            return $"{this.Name} lost {amount} Gold.";
        }

        public string GainXP(int amount)
        {
            this.ExperiencePoints += amount;

            string xpGain = $"{this.Name} gained {amount} XP!";

            while (this.ExperiencePoints >= _experienceToLevel)
            {
                this.ExperiencePoints -= _experienceToLevel;
                xpGain += LevelUp();
                this._experienceToLevel += 5;
            }

            return xpGain;
        }

        public string LoseXP(int amount)
        {
            this.ExperiencePoints -= amount;

            return $"{this.Name} lost {amount} XP.";
        }

        public void Revive()
        {
            this.GoTo(0, 0);
            this.Heal(this.MaxHP);
            this.isDead = false;
        }

        public string LevelUp()
        {
            this.Level += 1;
            this.MaxHP += 1;
            this.Strength += Dice.Roll(2);
            this.Dexterity += Dice.Roll(2);
            this.Constitution += Dice.Roll(2);
            this.Intelligence += Dice.Roll(2);
            this.Wisdom += Dice.Roll(2);
            this.Charisma += Dice.Roll(2);
            return $"\n{this.Name} gained a level! {this.Name}'s stats have increased!";
        }

        public override string Rest()
        {
            this.CurrentHP = this.MaxHP;
            return $"{this.Name} has a rest and returns to full health.";
        }

        public override void GoTo(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override void MoveNorth()
        {
            this.Y += 1;
        }

        public override void MoveEast()
        {
            this.X += 1;
        }

        public override void MoveSouth()
        {
            this.Y -= 1;
        }

        public override void MoveWest()
        {
            this.X -= 1;
        }

        public override void Face(Direction dir)
        {
            this.Facing = dir; 
        }

        public override void MoveForward()
        {
            switch(this.Facing)
            {
                
                case Direction.NORTH:
                    MoveNorth();
                    break;
                case Direction.EAST:
                    MoveEast();
                    break;
                case Direction.SOUTH:
                    MoveSouth();
                    break;
                case Direction.WEST:
                    MoveWest();
                    break;

            }
        }

        
        public override void Turn(int degrees)
        {
            if ((int)this.Facing + degrees >= 360)
            {
                this.Facing = Direction.NORTH;
            }
            else if ((int)this.Facing + degrees < 0)
            {
                this.Facing = Direction.WEST;
            }
            else
            {
                this.Facing += degrees;
            }
        }

        public void AddRaceStatBonus(string BonusStatOne, string BonusStatTwo)
        {
            switch (this.Race)
            {
                case Races.Dragonborn:
                    this.Strength += 2;
                    this.Charisma += 1;
                    this.Darkvision = false;
                    break;
                case Races.HillDwarf:
                    this.Constitution += 2;
                    this.Wisdom += 1;
                    this.Darkvision = true;
                    this.ResistanceToPoison += 50;
                    break;
                case Races.MountainDwarf:
                    this.Constitution += 2;
                    this.Strength += 2;
                    this.Darkvision = true;
                    this.ResistanceToPoison += 50;
                    break;
                case Races.HighElf:
                    this.Dexterity += 2;
                    this.Intelligence += 1;
                    this.Darkvision = true;
                    break;
                case Races.WoodElf:
                    this.Dexterity += 2;
                    this.Wisdom += 1;
                    this.Darkvision = true;
                    break;
                case Races.DarkElf:
                    this.Dexterity += 2;
                    this.Charisma += 1;
                    this.Darkvision = true;
                    break;
                case Races.DeepGnome:
                    this.Intelligence += 2;
                    this.Dexterity += 1;
                    this.Darkvision = true;
                    break;
                case Races.ForestGnome:
                    this.Intelligence += 2;
                    this.Dexterity += 1;
                    this.Darkvision = true;
                    break;
                case Races.RockGnome:
                    this.Intelligence += 2;
                    this.Constitution += 1;
                    this.Darkvision = true;
                    break;
                case Races.HalfElf:
                    this.Charisma += 2;
                    switch (BonusStatOne)
                    {
                        case "STR":
                            this.Strength += 1;
                            break;
                        case "DEX":
                            this.Dexterity += 1;
                            break;
                        case "CON":
                            this.Constitution += 1;
                            break;
                        case "INT":
                            this.Intelligence += 1;
                            break;
                        case "WIS":
                            this.Wisdom += 1;
                            break;
                    }
                    switch (BonusStatTwo)
                    {
                        case "STR":
                            this.Strength += 1;
                            break;
                        case "DEX":
                            this.Dexterity += 1;
                            break;
                        case "CON":
                            this.Constitution += 1;
                            break;
                        case "INT":
                            this.Intelligence += 1;
                            break;
                        case "WIS":
                            this.Wisdom += 1;
                            break;
                    }
                    this.Darkvision = true;
                    break;
                case Races.Tiefling:
                    this.Intelligence += 1;
                    if (BonusStatOne.Contains("DEX"))
                    {
                        this.Dexterity += 2;
                    }
                    else
                    {
                        this.Charisma += 2;
                    }
                    this.Darkvision = true;
                    this.ResistanceToFire += 50;
                    break;
                case Races.HalfOrc:
                    this.Strength += 2;
                    this.Constitution += 1;
                    this.Darkvision = true;
                    break;
                case Races.LightfootHalfling:
                    this.Dexterity += 2;
                    this.Charisma += 1;
                    this.Darkvision = false;
                    break;
                case Races.StoutHalfling:
                    this.Dexterity += 2;
                    this.Constitution += 1;
                    this.Darkvision = false;
                    this.ResistanceToPoison += 50;
                    break;
                case Races.Human:
                    this.Strength += 1;
                    this.Dexterity += 1;
                    this.Constitution += 1;
                    this.Intelligence += 1;
                    this.Wisdom += 1;
                    this.Charisma += 1;
                    this.Darkvision = false;
                    break;
            }
        }

        public void AddItemToInventory(Item item)
        {
            Inventory.Add(item);

            if(item.IsUnique)
            {
                GroupedInventory.Add(new GroupedInventoryItem(item, 1));
            }
            else
            {
                if(!GroupedInventory.Any(gi => gi.Item.ItemID == item.ItemID))
                {
                    GroupedInventory.Add(new GroupedInventoryItem(item, 0));
                }

                GroupedInventory.First(gi => gi.Item.ItemID == item.ItemID).Quantity++;
            }


            OnPropertyChanged("Weapons");
            OnPropertyChanged("Potions");
            OnPropertyChanged("Collectables");
        }

        public void RemoveItemFromInventory(Item item)
        {
            Inventory.Remove(item);

            GroupedInventoryItem groupedInventoryItemToRemove = item.IsUnique ?
                GroupedInventory.FirstOrDefault(gi => gi.Item == item) :
                GroupedInventory.FirstOrDefault(gi => gi.Item.ItemID == item.ItemID);                

            if(groupedInventoryItemToRemove != null)
            {
                if(groupedInventoryItemToRemove.Quantity == 1)
                {
                    GroupedInventory.Remove(groupedInventoryItemToRemove);
                }
                else
                {
                    groupedInventoryItemToRemove.Quantity--;
                }
            }

            OnPropertyChanged("Weapons");
            OnPropertyChanged("Potions");
            OnPropertyChanged("Collectables");
        }

        public bool HasItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.ItemID == item.ItemID) < item.Quantity)
                {
                    return false;
                }
            }

            return true;
        }



    }
}
