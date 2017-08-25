namespace Suspriar
{
    public class Suspriar
    {
        ITeamspeakManager CreateTeamspeakManager(string queryHost, int queryPort, string queryUserName, string queryPassword)
        {
            // "localhost", 10011, "tsbot_test", "sBzYsqog"

            var teamspeakManager = new TeamspeakManager(queryHost, queryPort, queryUserName, queryPassword);
            teamspeakManager.Connect();
            teamspeakManager.Login();          
            
            return teamspeakManager;
        }

        public static void Main()
        {
            var teamspeakManager = 
                new Suspriar().CreateTeamspeakManager("localhost", 10011, "tsbot_test", "sBzYsqog");
          
            while (true)
            {
                
            }
        }
    }
}