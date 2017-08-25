using System;
using TentacleSoftware.TeamSpeakQuery;
using TentacleSoftware.TeamSpeakQuery.NotifyResult;
using TentacleSoftware.TeamSpeakQuery.ServerQueryResult;

namespace Suspriar
{
    public class TeamspeakManager : ITeamspeakManager
    {
        public string QueryHost { get; }
        public int QueryPort { get; }
        public string QueryUserName { get; }
        public string QueryPassword { get; }

        private ServerQueryClient _serverQueryClient;
        
        public TeamspeakManager(string queryHost, int queryPort, string queryUserName, string queryPassword)
        {
            QueryHost = queryHost;
            QueryPort = queryPort;
            QueryUserName = queryUserName;
            QueryPassword = queryPassword;
        }

        public void Connect()
        {
            _serverQueryClient = new ServerQueryClient(QueryHost, QueryPort, TimeSpan.FromSeconds(3));
            _serverQueryClient.ConnectionClosed += (sender, eventArgs) => Console.WriteLine(":sadface:");
            
            ServerQueryBaseResult connected = _serverQueryClient.Initialize().Result;
            Console.WriteLine("connected {0}", connected.Success);
        }

        public void Login()
        {
            var login = _serverQueryClient.Login(QueryUserName, QueryPassword).Result;
            Console.WriteLine("login {0} {1} {2}", login.Success, login.ErrorId, login.ErrorMessage);
            
            ServerQueryBaseResult use = _serverQueryClient.Use(UseServerBy.ServerId, 1).Result;
            Console.WriteLine("use {0} {1} {2}", use.Success, use.ErrorId, use.ErrorMessage);
            
            _serverQueryClient.KeepAlive(TimeSpan.FromMinutes(2));
            
            var registerTextChannel = _serverQueryClient.ServerNotifyRegister(Event.server).Result;
            Console.WriteLine("registerTextChannel {0} {1} {2}", registerTextChannel.Success, registerTextChannel.ErrorId, registerTextChannel.ErrorMessage);

            _serverQueryClient.NotifyServerEdited += (sender, result) =>
            {
                Console.WriteLine("HOAL!");
            };
            
            var clientList = _serverQueryClient.ClientList().Result;
            Console.WriteLine("clientList {0} {1} [x{2}]", clientList.Success, clientList.ErrorId, clientList.Values?.Count ?? 0);
        }
    }
}