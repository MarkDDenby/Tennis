
using Tennis.Common;
using Tennis.Common.ScoreState;

namespace Tennis
{
    public class PointsProcessor
    {
        public Match Process(string points)
        {
            Player playerA = new Player("A");
            Player playerB = new Player("B");
            ScoreFormatter scoreFormatter = new ScoreFormatter();
            ScoringSystem scoringSystem = new ScoringSystem(playerA, playerB, scoreFormatter);

            Match match = new Match(playerA, playerB, scoringSystem);
            match.ScoringSystem.Server = playerA;

            PointsReader pointsReader = new PointsReader(points, playerA, playerB);

            while (pointsReader.HasPoints())
            {
                Player pointTo = pointsReader.ReadPoint();
                match.ScoringSystem.PointScored(pointTo, match.CurrentGame);

                if(match.ScoringSystem.State.GetType() == typeof(GameOverState))
                {
                    match.NewGame();
                }
            }
            return match;
        }
    }
}
