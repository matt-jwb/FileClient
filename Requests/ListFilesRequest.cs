using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileClient
{
    public class ListFilesRequest : IRequest
    {
        public string RequestType { get; set; }

        public ListFilesRequest()
        {
            RequestType = "ListFiles";
        }
    }
}
