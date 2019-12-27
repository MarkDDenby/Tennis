using System;
using System.Text;
using Tennis.Contracts;

namespace Tennis.Common
{
    public class ScoreFormatter : IScoreFormatter
    {
        public String MatchScore(IScoringSystem scoreSystem)
        {
            String format = string.Empty;
            if (scoreSystem.Server == scoreSystem.PlayerA)
            {
                format = "{0}-{1}";
            }
            else
            {
                format = "{1}-{0}";
            }

            String completedSetScores = this.CompletedSets(format, scoreSystem);
            String currentSet = this.CurrentSet(format, scoreSystem);
            String currentGameScore = this.CurrentGameScore(format, scoreSystem);

            string returnString = string.Empty;
            if (completedSetScores != string.Empty)
            {
                returnString = completedSetScores;
            }
            if (currentSet != string.Empty)
            {
                returnString = returnString + " " + currentSet;
            }
            returnString = returnString + " " + currentGameScore;

            return returnString.Trim();
        }

        private String GameScore(int points)
        {
            switch (points)
            {
                case 0:
                    return "0";
                case 1:
                    return "15";
                case 2:
                    return "30";
                case 3:
                    return "40";
                case 4:
                    return "A";
                default:
                    return "Unknown";
            }
        }

        private String CompletedSets(String format, IScoringSystem scoreSystem)
        {
            String completedSetScores = string.Empty;
            StringBuilder setStringBuilder = new StringBuilder();
            foreach (IScore score in scoreSystem.CompletedSets)
            {
                setStringBuilder.AppendFormat(format, score.PlayerAScore, score.PlayerBScore);
                setStringBuilder.Append(" ");
            }
            return setStringBuilder.ToString().TrimEnd();
        }

        private String CurrentSet(String format, IScoringSystem scoreSystem)
        {
            int playerACurrentSetScore = scoreSystem.GetCurrentSetScore(scoreSystem.PlayerA);
            int playerBCurrentSetScore = scoreSystem.GetCurrentSetScore(scoreSystem.PlayerB);
            return String.Format(format, playerACurrentSetScore, playerBCurrentSetScore);
        }

        private String CurrentGameScore(String format, IScoringSystem scoreSystem)
        {
            string currentGameScore = string.Empty;
            int playerAScore = scoreSystem.GetCurrentGameScore(scoreSystem.PlayerA);
            int playerBScore = scoreSystem.GetCurrentGameScore(scoreSystem.PlayerB);
            if (playerAScore != 0 || playerBScore != 0)
            {
                String playerAConvertedScore = this.GameScore(playerAScore);
                String playerBConvertedScore = this.GameScore(playerBScore);
                currentGameScore = string.Format(format, playerAConvertedScore, playerBConvertedScore);
            }
            return currentGameScore;
        }
    }
}
