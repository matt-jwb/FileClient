using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileClient.Command
{
    class ListFilesCommand : ICommand
    {
        public object Execute(string[] parameters)
        {
            RequestFacade requestFacade = RequestFacade.GetInstance();

            if (requestFacade.client != null && requestFacade.client.Connected)
            {
                IResponse response = requestFacade.SendRequest(new ListFilesRequest());
                if (response is ValidResponse validResponse)
                {
                    try
                    {
                        string[] files = ((JsonElement)validResponse.Data).EnumerateArray().Select(element => element.GetString()).ToArray();
                        return files;
                    }
                    catch (Exception)
                    {
                        throw new Exception("ERROR: The data recieved was in an invalid format");
                    }
                }
                else if (response is InvalidResponse invalidResponse)
                {
                    throw new Exception(invalidResponse.ErrorMessage);
                }
                else
                {
                    throw new Exception("ERROR: The server did not send a recognisable response");
                }
            }
            else
            {
                throw new Exception("ERROR: Not connected to a server");
            }
        }
    }
}
