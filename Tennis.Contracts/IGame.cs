using System;

namespace Tennis.Contracts
{
    public interface IGame
    {
        IMatch Match { get; }
        void ScorePoint(IPlayer player);
    }
}
