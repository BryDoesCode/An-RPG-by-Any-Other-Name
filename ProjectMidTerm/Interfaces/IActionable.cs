﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectMidTerm.Models.Creatures;

namespace ProjectMidTerm.Interfaces
{
    public interface IActionable
    {
        int Initiative { get; set; }

        string Attack(Creature defender);
        string Defend();
        string DefendAgainst(Creature attacker);
        string Use(IUsable used);
        string Rest();
    }
}
