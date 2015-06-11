using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EF_Mappings;

namespace Generate_Random_Matches
{
    class GenerateRandomMatches
    {
        static void Main()
        {
            var xmlDoc = XDocument.Load("../../generate-matches.xml");
            var context = new FootballEntities();

            var generateCount = 10;
            var maxGoals = 5;
            var startDate = "1-Jan-2000";
            var endDate = "31-Dec-2015";
            var leagueName = "";

            var generateMatchesRoot = xmlDoc.Root.Elements();
            var counter = 1;
            foreach (var generateMatch in generateMatchesRoot)
            {
                Console.WriteLine("\nProcessing request #{0} ...", counter++);
                if (generateMatch.Attribute("generate-count") != null)
                {
                    generateCount = int.Parse(generateMatch.Attribute("generate-count").Value);
                }

                if (generateMatch.Attribute("max-goals") != null)
                {
                    maxGoals = int.Parse(generateMatch.Attribute("max-goals").Value);
                }

                if (generateMatch.Element("league") != null)
                {
                    leagueName = generateMatch.Element("league").Value;
                }

                if (generateMatch.Element("start-date") != null)
                {
                    startDate = generateMatch.Element("start-date").Value;
                }

                if (generateMatch.Element("end-date") != null)
                {
                    startDate = generateMatch.Element("end-date").Value;
                }

                Random rand = new Random();
                int randomIndex;

                for (int i = 0; i < generateCount; i++)
                {
                    randomIndex = rand.Next(0, context.Teams.Count());
                    var league = context.Leagues.Where(l => l.LeagueName == leagueName).Select(l => new
                    {
                        Team = l.Teams.Select(t => t.TeamName)
                    });
                    var homeTeam = context.Teams.OrderBy(t => t.Id).Skip(randomIndex).FirstOrDefault();

                    randomIndex = rand.Next(0, context.Teams.Count());
                    var awayTeam = context.Teams.OrderBy(t => t.Id).Skip(randomIndex).FirstOrDefault();

                    Console.WriteLine("{0} - {1}", homeTeam.TeamName, awayTeam.TeamName);
                }

            }
        }
    }
}
