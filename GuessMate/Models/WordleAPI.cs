using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMate.Models
{
    public class WordleAPI
    {
        public string Solution { get; set; } = string.Empty;
        public int Days_Since_Launch { get; set; }
        public string Editor { get; set; } = string.Empty;
    }
}