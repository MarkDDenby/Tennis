using Tennis.Contracts;

namespace Tennis.Common.ScoreState
{
    public class AdvantageScoringState : ScoreState
    {
        private IPlayer _winner;

        public override void ScorePoint(IPlayer player, IGame game)
        {
            base.ScorePoint(player, game);

            game.Match.ScoringSystem.AwardPoint(player);

            if (player == game.Match.PlayerA)
            {
                if (game.Match.PlayerA.Advantage)
                {
                    _winner = game.Match.PlayerA;
                }
            }
            if (player == game.Match.PlayerB)
            {
                if (game.Match.PlayerB.Advantage)
                {
                    _winner = game.Match.PlayerB;
                }
            }
            this.StateChange();
        }

        public override void StateChange()
        {
            base.StateChange();

            _game.Match.PlayerA.Advantage = false;
            _game.Match.PlayerB.Advantage = false;

            if (_winner != null)
            {
                _game.Match.ScoringSystem.State = new GameOverState();
                _game.Match.ScoringSystem.AwardGame(_winner);
            }
            else
            {
                _game.Match.ScoringSystem.ResetGameScore(3, 3);
                _game.Match.ScoringSystem.State = new DeuceScoringState();
            }
        }
    }
}
