using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Core.Request
{
    public class PageDeptRequest
    {
        public int page { get; set; }
        public int limit { get; set; }

        public string key { get; set; }

        public int DeptId { get; set; }

        public string ModuleCode { get; set; }

        public PageDeptRequest()
        {
            page = 1;
            limit = 10;
        }
    }
}
