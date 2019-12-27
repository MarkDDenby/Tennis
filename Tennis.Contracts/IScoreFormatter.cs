using System;

namespace Tennis.Contracts
{
    public interface IScoreFormatter
    {
        String MatchScore(IScoringSystem scoreSystem);
    }
}
