using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Daesan.Models.Data
{
    public class Command
    {
        public int Scene { get; set; }
        public string Input { get; set; }

        public string ResponseText { get; set; }
        public string ResponsePhotoUrl { get; set; }
        public int ResponsePhotoWidth { get; set; }
        public int ResponsePhotoHeight { get; set; }
        public string ResponseButtonLabel { get; set; }
        public string ResponseButtonUrl { get; set; }
    }
}
