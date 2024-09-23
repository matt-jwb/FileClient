using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileClient.Command
{
    class DisconnectCommand : ICommand
    {
        public object Execute(string[] parameters)
        {
            RequestFacade requestFacade = RequestFacade.GetInstance();
            if (requestFacade.client != null && requestFacade.client.Connected)
            {
                requestFacade.client.Close();
                return "Disconnected from Server";
            }
            else
            {
                return "No server from which to disconnect.";
            }
        }
    }
}
