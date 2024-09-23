using System;
using System.IO;
using System.Net.Sockets;
using FileClient.Command;

namespace FileClient
{
    class Program
    {
        private static CommandFactory commandFactory = new CommandFactory();
        static void Main(string[] args)
        {
            Directory.CreateDirectory(@"C:\MattFileSystem\ClientStorage");
            Console.WriteLine("Matt Bonham's File Client V1 started");
            string cmd = "";
            while (cmd != "exit")
            {
                Console.Write("Enter a command > ");
                cmd = Console.ReadLine();

                if (!string.IsNullOrEmpty(cmd))
                {
                    string[] sections = cmd.Split(' ');
                    string command = sections[0];
                    string[] parameters = sections.Length > 1 ? sections[1..] : new string[] { };

                    switch (command.ToLower())
                    {
                        case "connect":
                            ExecuteCommand(ICommand.CONNECT, parameters);
                            break;
                        case "disconnect":
                            ExecuteCommand(ICommand.DISCONNECT, parameters);
                            break;
                        case "download":
                            ExecuteCommand(ICommand.RETRIEVE, parameters);
                            break;
                        case "upload":
                            ExecuteCommand(ICommand.UPLOAD, parameters);
                            break;
                        case "ls":
                            ListFiles();
                            break;
                        case "help":
                            PrintHelp();
                            break;
                        case "exit":
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Unknown command, type 'help' for a list of commands");
                            break;
                    }
                }
            }
        }

        private static void ExecuteCommand(int commandType, string[] parameters)
        {
            try
            {
                string result = (string)commandFactory.CreateCommand(commandType).Execute(parameters);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ListFiles()
        {
            try
            {
                string[] files = (string[])commandFactory.CreateCommand(ICommand.LIST).Execute(null);
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("\n\nList of commands:\n");
            Console.WriteLine("connect <ip> <port> - Connect to the server.");
            Console.WriteLine("disconnect - Disconnect from the server.");
            Console.WriteLine("download <file> - Download a file from the server.");
            Console.WriteLine("upload <file> - Upload a file to the server.");
            Console.WriteLine("ls - List files on the server.");
            Console.WriteLine("help - Show this help message.");
            Console.WriteLine("exit - Exit the client.\n\n");
        }
    }
}
