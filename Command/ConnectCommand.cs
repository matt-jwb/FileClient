using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FileClient.Command
{
    class ConnectCommand : ICommand
    {
        public object Execute(string[] parameters)
        {
            RequestFacade requestFacade = RequestFacade.GetInstance();
            if (requestFacade.client == null || !requestFacade.client.Connected)
            {
                if (parameters.Length > 0 && parameters.Length < 3)
                {
                    string ip;
                    int port;

                    if (parameters.Length == 1)
                    {
                        string[] sections = parameters[0].Split(':');
                        if (sections.Length != 2 || !int.TryParse(sections[1], out port))
                        {
                            throw new Exception("Invalid parameter format. Expected format: IP:port");
                        }
                        ip = sections[0];
                    }
                    else
                    {
                        ip = parameters[0];
                        if (!int.TryParse(parameters[1], out port))
                        {
                            throw new Exception("Invalid port number.");
                        }
                    }

                    try
                    {
                        TcpClient client = new TcpClient();
                        client.Connect(ip, port);
                        requestFacade.client = client;
                        return $"Connected to {ip} on port {port}";
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Failed to connect: {ex.Message}");
                    }
                }
                else
                {
                    throw new Exception("Invalid parameters. Use the format 'connect IP:port' or 'connect IP port'.");
                }
            }
            else
            {
                return "Already connected to the server.";
            }
        }
    }
}
