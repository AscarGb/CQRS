using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain
{
    public class CommandResponse
    {
        public string ID { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
