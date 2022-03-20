using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Core.Request
{
    public class PageRequest
    {
        public int page { get; set; }
        public int limit { get; set; }

        public string key { get; set; }

        public PageRequest()
        {
            page = 1;
            limit = 10;
        }
    }
}
