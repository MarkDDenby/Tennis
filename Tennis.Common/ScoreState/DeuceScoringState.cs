using System;
using Tennis.Contracts;

namespace Tennis.Common.ScoreState
{
    public class DeuceScoringState : ScoreState
    {
        public override void ScorePoint(IPlayer player, IGame game)
        {
            base.ScorePoint(player, game);

             game.Match.ScoringSystem.AwardPoint(player);

            if (player == _game.Match.PlayerA)
            {
                game.Match.PlayerA.Advantage = true;
                game.Match.PlayerB.Advantage = false;
            }
            else
            {
                game.Match.PlayerA.Advantage = false;
                game.Match.PlayerB.Advantage = true;
            }

            this.StateChange();
        }

        public override void StateChange()
        {
            base.StateChange();
            _game.Match.ScoringSystem.State = new AdvantageScoringState();
        }
    }
}
