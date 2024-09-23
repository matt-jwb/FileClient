using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileClient
{
    class RequestFacade
    {
        private static RequestFacade instance = null;
        public TcpClient client;
        private RequestFacade()
        {

        }

        public static RequestFacade GetInstance()
        {
            if (instance == null)
            {
                instance = new RequestFacade();
            }
            return instance;
        }

        public IResponse SendRequest<T>(T request)
        {
            StreamWriter writer = new StreamWriter(client.GetStream());
            StreamReader reader = new StreamReader(client.GetStream());

            writer.WriteLine(JsonSerializer.Serialize<T>(request));
            writer.Flush();

            string responseJson = reader.ReadLine();

            try
            {
                return JsonSerializer.Deserialize<ValidResponse>(responseJson);
            }
            catch (Exception e)
            {
                try
                {
                    return JsonSerializer.Deserialize<InvalidResponse>(responseJson);
                }
                catch (Exception)
                {
                    throw new Exception($"ERROR: {e.Message}");
                }
            }
        }
    }
}
