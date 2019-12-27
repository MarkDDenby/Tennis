using System;

namespace Tennis.Contracts
{
    public interface IPlayer
    {
        String Name { get; }
        Boolean Advantage { get; set; } // this doesn't belong here, should be part of the scoring system
    }
}
