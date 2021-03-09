namespace ConversationalWeed.Models
{
    public class PlayerStats
    {
        public ulong PlayerId { get; set; }
        public string Name { get; set; }
        public int TotalMatches { get; set; }
        public int TotalWins { get; set; }
        public int TotalSmokedPoints { get; set; }
        public int TotalWeedPoints { get; set; }
        public ulong WeedCoins { get; set; }
        public string CurrentSkin { get; set; }

        public double WinRatio
        {
            get
            {
                if (TotalMatches == 0)
                {
                    return 0.0;
                }
                else
                {
                    double totalWins = TotalWins;
                    double totalMatches = TotalMatches;
                    double result = totalWins / totalMatches;
                    return result;
                }

            }
        }

        public int TotalPoints
        {
            get
            {
                return TotalSmokedPoints + TotalWeedPoints;
            }
        }

        public double PointsAverage
        {
            get
            {
                if (TotalMatches == 0)
                {
                    return 0.0;
                }
                else
                {
                    double totalPoints = TotalPoints;
                    double totalMatches = TotalMatches;
                    double result = totalPoints / totalMatches;
                    return result;
                }
            }
        }

        public double WeedPointsAverage
        {
            get
            {
                if (TotalMatches == 0)
                {
                    return 0.0;
                }
                else
                {
                    double totalWeedPoints = TotalWeedPoints;
                    double totalMatches = TotalMatches;
                    double result = totalWeedPoints / totalMatches;
                    return result;
                }
            }
        }

        public double SmokedPointsAverage
        {
            get
            {
                if (TotalMatches == 0)
                {
                    return 0.0;
                }
                else
                {
                    double totalSmokedPoints = TotalSmokedPoints;
                    double totalMatches = TotalMatches;
                    double result = totalSmokedPoints / totalMatches;
                    return result;
                }
            }
        }
    }
}
