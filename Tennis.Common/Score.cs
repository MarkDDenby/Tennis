using System;
using Tennis.Contracts;

namespace Tennis.Common
{
    public class Score : IScore
    {
        public Score(IPlayer playerA, IPlayer playerB)
        {
            if (playerA == null)
            {
                throw new ArgumentNullException("playerA");
            }
            if (playerB == null)
            {
                throw new ArgumentNullException("playerB");
            }
            this.PlayerA = playerA;
            this.PlayerB = playerB;
            this.PlayerAScore = 0;
            this.PlayerBScore = 0;
        }

        public IPlayer PlayerA { get; private set; }
        public IPlayer PlayerB { get; private set; }
        public int PlayerAScore { get; set; }
        public int PlayerBScore { get; set; }
    }
}
