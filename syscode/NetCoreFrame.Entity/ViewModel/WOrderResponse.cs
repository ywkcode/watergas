using NetCoreFrame.Entity.Wedrent;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Entity.ViewModel
{
    public class WOrderResponse: W_Order
    {
        public List<W_GoodsInfo> GoodsInfo { get; set; }
    }

    public class W_GoodsInfo
    { 
       public string GoodsId { get; set; }

        public string GoodsName { get; set; }
    }
}
