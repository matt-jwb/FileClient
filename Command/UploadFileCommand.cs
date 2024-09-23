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
    class UploadFileCommand : ICommand
    {
        public object Execute(string[] parameters)
        {
            Console.WriteLine("Enter filename > ");
            string userInput = Console.ReadLine();

            string fileName;
            string filePath;
            if (Path.IsPathRooted(userInput))
            {
                fileName = Path.GetFileName(userInput);
                filePath = userInput;
            }
            else
            {
                fileName = userInput;
                filePath = Path.Combine(@"C:\MattFileSystem\ClientStorage", userInput);
            }

            byte[] fileData;
            RequestFacade requestFacade = RequestFacade.GetInstance();

            if (requestFacade.client != null && requestFacade.client.Connected)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        fileData = File.ReadAllBytes(filePath);
                    }
                    else
                    {
                        throw new FileNotFoundException("File not found", fileName);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("There was an error saving the file");
                }

                IResponse response = requestFacade.SendRequest(new UploadFileRequest(@$"C:\MattFileSystem\ServerStorage\{fileName}", fileData));
                if (response is ValidResponse validResponse)
                {
                    try
                    {
                        string msg = ((JsonElement)validResponse.Data).GetString();
                        return msg;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw new Exception("ERROR: The data recieved was in an invalid format");
                    }
                }
                else if (response is InvalidResponse invalidResponse)
                {
                    throw new Exception("InvalidResponse: " + invalidResponse.ErrorMessage);
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
