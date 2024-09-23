using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileClient.Command
{
    class NullCommand : ICommand
    {
        public NullCommand() { }

        public object Execute(string[] parameters)
        {
            throw new Exception("ERROR: Command could not be found");
        }
    }
}
