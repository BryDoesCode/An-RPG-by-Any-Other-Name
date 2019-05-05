using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Interfaces;

namespace ProjectMidTerm.Models
{
    public class Location : ILocatable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Facing { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public List<Quest> QuestsAvailableHere { get; set; } = new List<Quest>();
    }
}
