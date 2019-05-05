using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Interfaces;

namespace ProjectMidTerm.Models.Creatures
{
    public class Creature : BaseNotificationClass, IMobile, IActionable
    {
        /* Fields */

        private static string[] resistanceLabels =
         { "Acid",
           "Bludg",
           "Cold",
           "Fire",
           "Force",
           "Lghtn",
           "Necro",
           "Pierc",
           "Poisn",
           "Psych",
           "Radnt",
           "Slash",
           "Thund"
         };

        private int[] resistances;

        public int[] attributes;
        /*
         * Strength
         * Dexterity
         * Constitution
         * Intelligence
         * Wisdom
         * Charisma
         */

        /* Properties */

        private int _maxHP;
        private int _currentHP;
        private int _armorClass;
        private int _gold;
        private int _experiencePoints;
        private string _displayHP;
        private string _imageName;
        private string _name;
        private bool _darkvision;


        public int CurrentHP
        {
            get { return _currentHP; }
            set
            {
                _currentHP = value;
                DisplayHP = "";
                OnPropertyChanged("CurrentHP");
            }
        }

        public int MaxHP {
            get { return _maxHP; }
            set
            {
                _maxHP = value;
                DisplayHP = "";
                OnPropertyChanged("MaxHP");
            }
        }

        public int ArmorClass {
            get { return _armorClass; }
            set
            {
                _armorClass = value;
                OnPropertyChanged("ArmorClass");
            }
        }

        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged("Gold");
            }
        }

        public bool Darkvision
        {
            get { return _darkvision; }
            set
            {
                _darkvision = value;
                OnPropertyChanged("Darkvision");
            }
        }

        public int Strength
        {
            get
            {
                return attributes[0];
            }

            set
            {
                attributes[0] = value;
                OnPropertyChanged("Strength");
            }
        }

        public int Dexterity
        {
            get
            {
                return attributes[1];
            }

            set
            {
                attributes[1] = value;
                OnPropertyChanged("Dexterity");
            }
        }

        public int Constitution
        {
            get
            {
                return attributes[2];
            }

            set
            {
                attributes[2] = value;

                OnPropertyChanged("Constitution");
            }
        }

        public int Intelligence
        {
            get
            {
                return attributes[3];
            }

            set
            {
                attributes[3] = value;

                OnPropertyChanged("Intelligence");
            }
        }

        public int Wisdom
        {
            get
            {
                return attributes[4];
            }

            set
            {
                attributes[4] = value;

                OnPropertyChanged("Wisdom");
            }
        }

        public int Charisma
        {
            get
            {
                return attributes[5];

            }

            set
            {
                attributes[5] = value;

                OnPropertyChanged("Charisma");
            }
        }

        public int ResistanceToAcid
        {
            get
            {
                return resistances[0];
            }
            protected set
            {
                resistances[0] = value;

                OnPropertyChanged("ResistanceToAcid");
            }
        }

        public int ResistanceToBludgeoning
        {
            get
            {
                return resistances[1];
            }
            protected set
            {
                resistances[1] = value;
                OnPropertyChanged("ResistanceToBludgeoning");
            }
        }

        public int ResistanceToCold
        {
            get
            {
                return resistances[2];
            }
            protected set
            {
                resistances[2] = value;
                OnPropertyChanged("ResistanceToCold");
            }
        }

        public int ResistanceToFire
        {
            get
            {
                return resistances[3];
            }
            protected set
            {
                resistances[3] = value;
                OnPropertyChanged("ResistanceToFire");
            }
        }

        public int ResistanceToForce
        {
            get
            {
                return resistances[4];
            }
            protected set
            {
                resistances[4] = value;
                OnPropertyChanged("ResistanceToForce");
            }
        }

        public int ResistanceToLightning
        {
            get
            {
                return resistances[5];
            }
            protected set
            {
                resistances[5] = value;
                OnPropertyChanged("ResistanceToLightning");
            }
        }

        public int ResistanceToNecrotic
        {
            get
            {
                return resistances[6];
            }
            protected set
            {
                resistances[6] = value;
                OnPropertyChanged("ResistanceToNecrotic");
            }
        }

        public int ResistanceToPiercing
        {
            get
            {
                return resistances[7];
            }
            protected set
            {
                resistances[7] = value;
                OnPropertyChanged("ResistanceToPiercing");
            }
        }

        public int ResistanceToPoison
        {
            get
            {
                return resistances[8];
            }
            protected set
            {
                resistances[8] = value;
                OnPropertyChanged("ResistanceToPoison");
            }
        }

        public int ResistanceToPsychic
        {
            get
            {
                return resistances[9];
            }
            protected set
            {
                resistances[9] = value;
                OnPropertyChanged("ResistanceToPsychic");
            }
        }

        public int ResistanceToRadiant
        {
            get
            {
                return resistances[10];
            }
            protected set
            {
                resistances[10] = value;
                OnPropertyChanged("ResistanceToRadiant");
            }
        }

        public int ResistanceToSlashing
        {
            get
            {
                return resistances[11];
            }
            protected set
            {
                resistances[11] = value;
                OnPropertyChanged("ResistanceToSlashing");
            }
        }

        public int ResistanceToThunder
        {
            get
            {
                return resistances[12];
            }
            protected set
            {
                resistances[12] = value;
                OnPropertyChanged("ResistanceToThunder");
            }
        }

        public int Initiative { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string DisplayHP
        {
            get { return _displayHP; }
            protected set
            {
                _displayHP = $"{this.CurrentHP} / {this.MaxHP}";
                OnPropertyChanged("DisplayHP");
                OnPropertyChanged("MaxHP");
                OnPropertyChanged("CurrentHP");
            }
        }

        public string ImageName
        {
            get { return _imageName; }
            protected set
            {
                _imageName = $"/ProjectMidTerm;component/Images/Creatures/{value}";
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public Direction Facing { get; set; }

        public bool isDead { get; set; }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged("ExperiencePoints");
            }
        }

        /* Constructors */

        public Creature()
        {
            this.attributes = new int[6];
            this.resistances = new int[13];
            this.Darkvision = false;
            this.CurrentHP = 0;
            this.MaxHP = 0;
            this.ExperiencePoints = 0;
            this.Initiative = (int)Math.Floor((this.Dexterity - 10.0d) / 2);
            this.isDead = false;

        }


        /* methods */

        public override string ToString()
        {
            string output =
                this.GetType().Name +
                "\n============" +
                "\nSTR: " + this.Strength +
                "\nDEX: " + this.Dexterity +
                "\nCON: " + this.Constitution +
                "\nINT: " + this.Intelligence +
                "\nWIS: " + this.Wisdom +
                "\nCHA: " + this.Charisma +
                "\n============" +
                "\n HP: " + this.CurrentHP + " / " + this.MaxHP +
                "\n AC: " + this.ArmorClass +
                "\n============" +
                "\nResistances:" +
                "\n";
            for (int idx = 0; idx < resistanceLabels.Length / 2; idx++)
            {
                output += string.Format("{0,6}", resistanceLabels[idx]);
            }
            output += "\n";
            for (int idx = 0; idx < resistanceLabels.Length / 2; idx++)
            {
                output += string.Format("{0,6}", resistances[idx]);
            }
            output += "\n";
            for (int idx = resistanceLabels.Length / 2; idx < resistanceLabels.Length; idx++)
            {
                output += string.Format("{0,6}", resistanceLabels[idx]);
            }
            output += "\n";
            for (int idx = resistanceLabels.Length / 2; idx < resistanceLabels.Length; idx++)
            {
                output += string.Format("{0,6}", resistances[idx]);
            }
            output += "\n============\n";
            output += "Darkvision: " + this.Darkvision + "\n";
            return output;
        }

        public virtual string Attack(Creature c)
        {
            return null;  // specify attacks in subclass
        }

        public virtual string Defend()
        {
            return null; 
        }

        public virtual string DefendAgainst(Creature attacker)
        {
            return null;
        }


        public virtual string Use(IUsable used)
        {
            return null;
        }

        public virtual string Rest()
        {
            return null;
        }

        public virtual void GoTo(int x, int y)
        {
            
        }

        public virtual void MoveNorth()
        {
            
        }

        public virtual void MoveEast()
        {
            
        }

        public virtual void MoveSouth()
        {
            
        }

        public virtual void MoveWest()
        {
            
        }

        public virtual void Face(Direction dir)
        {
            
        }

        public virtual void MoveForward()
        {
            
        }

        public virtual void Turn(int degrees)
        {
            
        }
    }
}