using System;
using System.Collections.Generic;

namespace DFS.Models
{
    public class InstagramMedia
    {
        
        public List<Data> data { get; set; }        

        public class Data
        {
            public string id { get; set; }
            public string media_url { get; set; }
        }

    }
}
