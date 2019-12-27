using System;
using Tennis.Common.ScoreState;
using Tennis.Contracts;

namespace Tennis.Common
{
    public class Game : IGame
    {
        public IMatch Match { get; private set; }

        public Game(IMatch match)
        {
            if(match == null) 
            {
                throw new ArgumentNullException("match");    
            }
            this.Match = match;
            this.Match.ScoringSystem.State = new PointScoringState();
            this.Match.ScoringSystem.ResetGameScore(0,0);
        }

        public void ScorePoint(IPlayer player)
        {
            this.Match.ScoringSystem.PointScored(player, this);
        }
    }
}
