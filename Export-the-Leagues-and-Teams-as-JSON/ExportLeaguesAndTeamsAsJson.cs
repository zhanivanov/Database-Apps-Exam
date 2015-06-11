namespace Export_the_Leagues_and_Teams_as_JSON
{
    using System.IO;
    using System.Linq;
    using System.Web.Script.Serialization;
    using EF_Mappings;

    class ExportLeaguesAndTeamsAsJson
    {
        static void Main()
        {
            var context = new FootballEntities();

            var leagueTeamsQuery =
                context.Leagues
                .OrderBy(l => l.LeagueName)
                .Select(l => new
                {
                    leagueName = l.LeagueName,
                    teams = l.Teams
                    .OrderBy(t => t.TeamName)
                    .Select(t => t.TeamName)
                });

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            var jsonString = serializer.Serialize(leagueTeamsQuery);

            File.WriteAllText(@"..\..\leagues-and-teams.json", jsonString);
        }
    }
}
