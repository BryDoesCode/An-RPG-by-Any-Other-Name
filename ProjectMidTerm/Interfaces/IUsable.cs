using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMidTerm.Interfaces
{
    public interface IUsable
    {
        int UsesLeft { get; set; }
        float UseChance { get; set; }   // range of 0.0 to 1.0

        string SuccessMessage();
        string FailureMessage();
    }
}
