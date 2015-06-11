namespace Export_International_Matches_As_XML
{
    using System.Linq;
    using System.Xml.Linq;
    using EF_Mappings;

    public class ExportInternationalMatchesAsXml
    {
        static void Main()
        {
            var context = new FootballEntities();

            var matchesQuery =
                context.InternationalMatches
                    .Select(m => new
                    {
                        HomeCountry = m.HomeCountry.CountryName,
                        HomeCountryCode = m.HomeCountryCode,
                        AwayCountry = m.AwayCountry.CountryName,
                        AwayCountryCode = m.AwayCountryCode,
                        League = m.League.LeagueName,
                        DateTime = m.MatchDate,
                        HomeGoals = m.HomeGoals,
                        AwayGoals = m.AwayGoals
                    })
                    .OrderBy(m => m.DateTime)
                    .ThenBy(m => m.HomeCountry)
                    .ThenBy(m => m.AwayCountry);

            var doc = new XDocument();
            var rootElement = new XElement("matches");
            doc.Add(rootElement);

            foreach (var match in matchesQuery)
            {
                var matchElement = new XElement("match");

                var homeCountryElement = new XElement("home-country", new XAttribute("code", match.HomeCountryCode), match.HomeCountry);
                var awayCountryElement = new XElement("away-country", new XAttribute("code", match.AwayCountryCode), match.AwayCountry);

                matchElement.Add(homeCountryElement);
                matchElement.Add(awayCountryElement);

                var isScore = (match.HomeGoals != 0 && match.HomeGoals != null) ||
                              (match.AwayGoals != 0 && match.AwayGoals != null);
                if (isScore)
                {
                    var score = match.HomeGoals + "-" + match.AwayGoals;
                    var scoreElement = new XElement("score", score);
                    matchElement.Add(scoreElement);
                }

                if (match.League != null)
                {
                    var leagueElement = new XElement("league", match.League);
                    matchElement.Add(leagueElement);
                }

                if (match.DateTime != null)
                {
                    var date = "";
                    var attrName = "";

                    if (match.DateTime.Value.Hour != 0 ||
                        match.DateTime.Value.Minute != 0 ||
                        match.DateTime.Value.Second != 0)
                    {
                        date = match.DateTime.Value.ToString("dd-MMM-yyyy HH:mm");
                        attrName = "date-time";
                    }
                    else
                    {
                        date = match.DateTime.Value.ToString("dd-MMM-yyyy");
                        attrName = "date";
                    }

                    var dateTimeAttr = new XAttribute(attrName, date);
                    matchElement.Add(dateTimeAttr);
                }
                rootElement.Add(matchElement);
            }

            doc.Save(@"..\..\international-matches.xml");
        }
    }
}
