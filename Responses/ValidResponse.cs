using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileClient
{
    public class ValidResponse : IResponse
    {
        public object Data { get; set; }

        public ValidResponse(object data)
        {
            Data = data;
        }
    }
}
