using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileClient.Command
{
    class RetrieveFileCommand : ICommand
    {
        public object Execute(string[] parameters)
        {
            Console.WriteLine("Enter filename > ");
            string userInput = Console.ReadLine();

            string fileName;
            string filePath;
            if (Path.IsPathRooted(userInput))
            {
                if (Path.GetDirectoryName(userInput) == @"C:\MattFileSystem\ServerStorage")
                {
                    fileName = Path.GetFileName(userInput);
                    filePath = userInput;
                }
                else
                {
                    throw new Exception(@"ERROR: You can only retrieve files from C:\MattFileSystem\ServerStorage");
                }
            }
            else
            {
                fileName = userInput;
                filePath = Path.Combine(@"C:\MattFileSystem\ServerStorage", userInput);
            }

            RequestFacade requestFacade = RequestFacade.GetInstance();

            if (requestFacade.client != null && requestFacade.client.Connected)
            {
                IResponse response = requestFacade.SendRequest(new RetrieveFileRequest(filePath));
                if (response is ValidResponse validResponse)
                {
                    try
                    {
                        byte[] fileData = ((JsonElement)validResponse.Data).GetBytesFromBase64();
                        File.WriteAllBytes(@$"C:\MattFileSystem\ClientStorage\{fileName}", fileData);
                        return $"The file {fileName}, was retrieved and saved to your device";
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
