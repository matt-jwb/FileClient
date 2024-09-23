using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FileClient.Command
{
    interface ICommand
    {
        public const int CONNECT = 1;
        public const int RETRIEVE = 2;
        public const int UPLOAD = 3;
        public const int LIST = 4;
        public const int DISCONNECT = 5;

        public abstract object Execute(string[] parameters);
    }
}
