using System;
using System.Collections.Generic;

namespace Tennis.Contracts
{
    public interface IScoringSystem
    {
        List<IScore> CompletedSets { get; }
        IScore CurrentSet { get;  }
        IScore GameScore { get;  }
        IScoringState State { get; set; }
        IPlayer Server { get; set; }
        IPlayer PlayerA { get; }
        IPlayer PlayerB { get; }

        void PointScored(IPlayer player, IGame game);
        void AwardPoint(IPlayer player);
        void AwardGame(IPlayer player);
        void AwardSet(IPlayer player);
        void ResetGameScore(int playerAScore, int playerBScore);
        void AlternateServer();

        IPlayer GetSetWinner();
        IPlayer GetGameWinner();

        String MatchScore();
        int GetCurrentSetScore(IPlayer player);
        int GetCurrentGameScore(IPlayer player);
    }
}
