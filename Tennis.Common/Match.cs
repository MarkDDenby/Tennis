using System;
using Tennis.Contracts;

namespace Tennis.Common
{
    public class Match : IMatch
    {
        public IPlayer PlayerA { get; private set; }
        public IPlayer PlayerB { get; private set; }
        public IGame CurrentGame { get; private set; }
        public IScoringSystem ScoringSystem { get; set; }

        public Match(IPlayer playerA, IPlayer playerB, IScoringSystem scoringSystem)
        {
            if(playerA == null) 
            {
                throw new ArgumentNullException("playerA");    
            }
            if(playerB == null)
            {
                throw new ArgumentNullException("playerB");
            }
            if(scoringSystem == null) 
            {
                throw new ArgumentNullException("scoringSystem");   
            }
            this.PlayerA = playerA;
            this.PlayerB = playerB;
            this.ScoringSystem = scoringSystem;
            this.NewGame();
        }

        public void NewGame()
        {
            this.CurrentGame = new Game(this);
        }
    }
}
