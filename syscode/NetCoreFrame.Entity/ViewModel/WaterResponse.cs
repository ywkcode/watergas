using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Entity.ViewModel
{
    public class WaterData
    {
        public string DataName { get; set; }

        public string Data_Stand { get; set; }

        public string Data_Real { get; set; }
    }

    public class WaterResponse
    {
        public WaterResponse()
        {
            waterDatas = new List<WaterData>();
            waterDatas2 = new List<WaterData>();
            
            NowDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }
        public string NowDate { get; set; }
        public List<WaterData> waterDatas { get; set; }
        public List<WaterData> waterDatas2 { get; set; }
     
    }
}
