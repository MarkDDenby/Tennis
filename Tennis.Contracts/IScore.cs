using System;

namespace Tennis.Contracts
{
    public interface IScore
    {
        IPlayer PlayerA { get; }
        IPlayer PlayerB { get; }
        int PlayerAScore { get; set; }
        int PlayerBScore { get; set; }
    }
}
