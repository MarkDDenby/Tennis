using System;
using Tracsis.Tennis.Contracts;

namespace Tracsis.Tennis.Common.MatchState
{
    public class NormalState : IMatchState
    {
        public NormalState(IMatch match)
        {
            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            this.Match = match;
        }

        public IMatch Match
        {
            get; private set;
        }

        public void StateChange()
        {
            
        }
    }
}
