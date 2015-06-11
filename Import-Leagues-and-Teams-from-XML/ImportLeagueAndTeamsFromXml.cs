namespace Import_Leagues_and_Teams_from_XML
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using EF_Mappings;

    class ImportLeagueAndTeamsFromXml
    {
        static void Main()
        {
            var xmlDoc = XDocument.Load("../../leagues-and-teams.xml");
            var context = new FootballEntities();
            var leagueElements = xmlDoc.Root.Elements();

            var counter = 1;
            foreach (var leagueElement in leagueElements)
            {
                Console.WriteLine("\nProcessing league #{0}...", counter++);
                var leagueNameElement = leagueElement.Element("league-name");
                var teamsElements = leagueElement.Element("teams");

                League league = null;
                if (leagueNameElement != null)
                {
                    league = context.Leagues.FirstOrDefault(l => l.LeagueName == leagueNameElement.Value);
                    if (league == null)
                    {
                        league = new League();
                        league.LeagueName = leagueNameElement.Value;
                        context.Leagues.Add(league);
                        Console.WriteLine("Created league: {0}", leagueNameElement.Value);
                    }
                    else
                    {
                        Console.WriteLine("Existing league: {0}", leagueNameElement.Value);
                    }
                }

                Team team = null;
                if (teamsElements != null)
                {
                    var teamsList = teamsElements.Elements();
                    foreach (var teamElement in teamsList)
                    {
                        if (teamElement != null)
                        {
                            var teamNameAttr = teamElement.Attribute("name");
                            var teamCountryAttr = teamElement.Attribute("country");

                            var teamName = teamNameAttr.Value;
                            string teamCountry = null;

                            if (teamCountryAttr != null)
                            {
                                teamCountry = teamCountryAttr.Value;
                            }
                            team = context.Teams
                                .FirstOrDefault(
                                    t => t.TeamName == teamName &&
                                         t.Country.CountryName == teamCountry);

                            if (team == null)
                            {
                                team = new Team {TeamName = teamName};
                                if (teamCountry != null)
                                {
                                    team.Country = context.Countries.FirstOrDefault(c => c.CountryName == teamCountry);
                                }
                                context.Teams.Add(team);
                                Console.WriteLine("Created team: {0} ({1})", teamName, teamCountry ?? "no country");
                            }
                            else
                            {
                                Console.WriteLine("Existing team: {0} ({1})", teamName, teamCountry ?? "no country");
                            }
                        }

                        if (leagueNameElement != null)
                        {
                            var isTeamInLeague =
                                league.Teams.FirstOrDefault(
                                    t => t.TeamName == team.TeamName && t.Country == team.Country);
                            if (isTeamInLeague == null)
                            {
                                league.Teams.Add(team);
                                Console.WriteLine("Added team to league: {0} to league {1}", team.TeamName,
                                    league.LeagueName);
                            }
                            else
                            {
                                Console.WriteLine("Existing team to league: {0} to league {1}", team.TeamName,
                                    league.LeagueName);
                            }
                        }

                        team = null;
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
