using System;
using System.Collections.Generic;
using System.Text;

namespace DapperExample.Models
{
    public class MessageDTO
    {
        public ObjectTypes ObjectType { get; set; }
        public object Data { get; set; }
    }
}
