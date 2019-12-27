using System;
using Tennis.Contracts;

namespace Tennis.Common.ScoreState
{
    public abstract class ScoreState : IScoringState
    {
        protected IPlayer _player;
        protected IGame _game;

        public virtual void ScorePoint(IPlayer player, IGame game)
        {
            if(player == null)
            {
                throw new ArgumentNullException("player");
            }
            if(game == null)
            {
                throw new ArgumentNullException("game");
            }
                           
            this._player = player;
            this._game = game;
        }

        public virtual void StateChange()
        {
        }
    }
}
