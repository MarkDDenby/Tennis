using Tennis.Contracts;

namespace Tennis.Common.ScoreState
{
    public class PointScoringState : ScoreState
    {
        public override void ScorePoint(IPlayer player, IGame game)
        {
            base.ScorePoint(player, game);

            game.Match.ScoringSystem.AwardPoint(player);

            this.StateChange();
        }

        public override void StateChange()
        {
            base.StateChange();

            IPlayer winner = _game.Match.ScoringSystem.GetGameWinner();
            if (winner != null)
            {
                _game.Match.ScoringSystem.State = new GameOverState();
                _game.Match.ScoringSystem.AwardGame(winner);
            }
            else
            {
                int playerAScore = _game.Match.ScoringSystem.GetCurrentGameScore(_game.Match.PlayerA);
                int playerBScore = _game.Match.ScoringSystem.GetCurrentGameScore(_game.Match.PlayerB);
                if (playerAScore == 3 && playerBScore == 3)
                {
                    _game.Match.ScoringSystem.State = new DeuceScoringState();
                }
            }
        }
    }
}
