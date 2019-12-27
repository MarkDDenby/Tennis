using System;
using System.Collections.Generic;
using System.Text;
using Tennis.Contracts;
using Tennis.Common.ScoreState;

namespace Tennis.Common
{
    public class ScoringSystem : IScoringSystem
    {
        public List<IScore> CompletedSets { get; private set; }
        public IScore CurrentSet { get; private set; }
        public IScore GameScore { get; private set; }
        public IPlayer Server { get; set; }
        public IScoringState State { get; set; }
        public IPlayer PlayerA { get; private set; }
        public IPlayer PlayerB { get; private set; }

        private readonly IScoreFormatter _scoreFormatter;

        public ScoringSystem(IPlayer playerA, IPlayer playerB, IScoreFormatter scoreFormatter)
        {
            if (playerA == null)
            {
                throw new ArgumentNullException("playerA");
            }
            if (playerB == null)
            {
                throw new ArgumentNullException("playerB");
            }
            if(scoreFormatter == null)
            {
                throw new ArgumentNullException("scoreFormatter");
            }

            this.PlayerA = playerA;
            this.PlayerB = playerB;

            _scoreFormatter = scoreFormatter;

            this.CompletedSets = new List<IScore>();
            this.CurrentSet = new Score(this.PlayerA, this.PlayerB);
            this.GameScore = new Score(this.PlayerA, this.PlayerB);

            this.State = new PointScoringState();
        }

        public void AlternateServer()
        {
            this.Server = this.Server == this.PlayerA ? this.PlayerB : this.PlayerA;
        }

        public void PointScored(IPlayer player, IGame game)
        {
            this.State.ScorePoint(player, game);
        }

        public void AwardPoint(IPlayer player)
        {
            if (player == this.PlayerA)
            {
                this.GameScore.PlayerAScore++;
            }
            if (player == this.PlayerB)
            {
                this.GameScore.PlayerBScore++;
            }
        }

        public void AwardGame(IPlayer player)
        {
            if (player == this.PlayerA)
            {
                this.CurrentSet.PlayerAScore++;
            }
            if (player == this.PlayerB)
            {
                this.CurrentSet.PlayerBScore++;
            }

            IPlayer setWinner = this.GetSetWinner();
            if (setWinner != null)
            {
                this.AwardSet(setWinner);
            }

            this.AlternateServer();
            this.ResetGameScore(0, 0);
        }

        public void AwardSet(IPlayer player)
        {
            Score setScore = new Score(this.PlayerA, this.PlayerB);
            setScore.PlayerAScore = this.GetCurrentSetScore(this.PlayerA);
            setScore.PlayerBScore = this.GetCurrentSetScore(this.PlayerB);

            this.CompletedSets.Add(setScore);

            this.ResetCurrentSetScore();
        }

        public void ResetGameScore(int playerAScore, int playerBScore)
        {
            this.GameScore.PlayerAScore = playerAScore;
            this.GameScore.PlayerBScore = playerBScore;
        }

        private void ResetCurrentSetScore()
        {
            this.CurrentSet.PlayerAScore = 0;
            this.CurrentSet.PlayerBScore = 0;
        }

        public int GetCurrentSetScore(IPlayer player)
        {
            if (player == this.PlayerA)
            {
                return this.CurrentSet.PlayerAScore;
            }
            if (player == this.PlayerB)
            {
                return this.CurrentSet.PlayerBScore;
            }
            return 0;
        }

        public int GetCurrentGameScore(IPlayer player)
        {
            if (player == this.PlayerA)
            {
                return this.GameScore.PlayerAScore;
            }
            if (player == this.PlayerB)
            {
                return this.GameScore.PlayerBScore;
            }
            return 0;
        }

        public IPlayer GetSetWinner()
        {
            int playerAScore = this.GetCurrentSetScore(this.PlayerA);
            int playerBScore = this.GetCurrentSetScore(this.PlayerB);

            if (PlayerAScoredSixAndHoldsTwoPointsLeadOverPlayerB(playerAScore, playerBScore))
            {
                return this.PlayerA;
            }

            if (PlayerBScoredSixAndHoldsTwoPointsLeadOverPlayerA(playerAScore, playerBScore))
            {
                return this.PlayerB;
            }

            return null;
        }

        private bool PlayerAScoredSixAndHoldsTwoPointsLeadOverPlayerB(int playerAScore, int playerBScore)
        {
            return (playerAScore >= 6 && (playerAScore - playerBScore) >= 2);
        }

        private bool PlayerBScoredSixAndHoldsTwoPointsLeadOverPlayerA(int playerAScore, int playerBScore)
        {
            return (playerBScore >= 6 && (playerBScore - playerAScore) >= 2);
        }

        public IPlayer GetGameWinner()
        {
            int playerAScore = this.GetCurrentGameScore(this.PlayerA);
            int playerBScore = this.GetCurrentGameScore(this.PlayerB);

            if (PlayerAScoredFourAndHoldsTwoPointsLeadOverPlayerB(playerAScore, playerBScore))
            {
                return this.PlayerA;
            }

            if (PlayerBScoredFourAndHoldsTwoPointsLeadOverPlayerA(playerAScore, playerBScore))
            {
                return this.PlayerB;
            }

            return null;
        }

        private bool PlayerAScoredFourAndHoldsTwoPointsLeadOverPlayerB(int playerAScore, int playerBScore)
        {
            return (playerAScore >= 4 && (playerAScore - playerBScore) >= 2);
        }

        private bool PlayerBScoredFourAndHoldsTwoPointsLeadOverPlayerA(int playerAScore, int playerBScore)
        {
            return (playerBScore >= 4 && (playerBScore - playerAScore) >= 2);
        }

        public string MatchScore()
        {
            return this._scoreFormatter.MatchScore(this);
        }
    }
}
