using System;

namespace Tennis.Contracts
{
    public interface IScoringState
    {
        void ScorePoint(IPlayer player, IGame game);
        void StateChange();
    }
}
