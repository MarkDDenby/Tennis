using System;

namespace Tennis.Contracts
{
    public interface IMatch
    {
        IPlayer PlayerA { get; }
        IPlayer PlayerB { get; }
        IScoringSystem ScoringSystem { get; set; }
        IGame CurrentGame { get; }

        void NewGame();
    }
}
