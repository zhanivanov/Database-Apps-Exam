namespace EF_Mappings
{
    using System;

    class ListAllTeamNames
    {
        static void Main()
        {
            var context = new FootballEntities();

            foreach (var team in context.Teams)
            {
                Console.WriteLine(team.TeamName);
            }
        }
    }
}
