using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Interfaces
{
    public interface ILocatable
    {
        int X { get; set; }
        int Y { get; set; }
        Direction Facing { get; set; }
    }
}
