using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileClient.Command
{
    class CommandFactory
    {
        public CommandFactory() { }

        public ICommand CreateCommand(int commandCode)
        {
            return commandCode switch
            {
                ICommand.CONNECT => new ConnectCommand(),
                ICommand.RETRIEVE => new RetrieveFileCommand(),
                ICommand.UPLOAD => new UploadFileCommand(),
                ICommand.LIST => new ListFilesCommand(),
                ICommand.DISCONNECT => new DisconnectCommand(),
                _ => new NullCommand(),
            };
        }
    }
}
