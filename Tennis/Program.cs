using System;
using System.IO;
using Tennis.Common;

namespace Tennis
{
    class MainClass
    {
        public static int Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Please supply a input and output filename");
                return 1;
            }

            string inputFilename = args[0];
            string outputFilename = args[1];

            using (var inputFile = new StreamReader(inputFilename))
            {
                using (var outputFile = new StreamWriter(outputFilename))
                {
                    string points;
                    while ((points = inputFile.ReadLine()) != null)
                    {
                        PointsProcessor pointsProcessor = new PointsProcessor();
                        Match match = pointsProcessor.Process(points);

                        outputFile.WriteLine(match.ScoringSystem.MatchScore());
                    }
                }
            }

            return 0;
        }
    }
}