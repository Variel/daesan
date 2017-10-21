using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daesan.Models.Request
{
    public class ChatRequest
    {
        public string UserKey { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
