using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.SocketConsole.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.SocketConsole.Service
{
    public   class DataHandleService
    {
        public static Water_Gas HandleGas(string Message)
        {
            Water_Gas model = new Water_Gas();
            model.H2S = Math.Round(Convert.ToDecimal(RegexHelper.GetValue(Message, "a21028-Rtd=", ",")), 3);
            model.HCL = Math.Round(Convert.ToDecimal(RegexHelper.GetValue(Message, "a21024-Rtd=", ",")), 3);
            model.CL2 = Math.Round(Convert.ToDecimal(RegexHelper.GetValue(Message, "a21022-Rtd=", ",")), 3);
            model.NH3 = Math.Round(Convert.ToDecimal(RegexHelper.GetValue(Message, "a21001-Rtd=", ",")), 3);
            return model;
        }

        public static Water_Quality HandleQuality(string Message)
        {
          
            Water_Quality model = new Water_Quality();
            model.TOC =Math.Round( Convert.ToDecimal(RegexHelper.GetValue(Message,"COD-Rtd=", ",")),3);
            model.AD = Math.Round(Convert.ToDecimal(RegexHelper.GetValue(Message, "060-Rtd=", ",")), 3);
            model.ZL = Math.Round(Convert.ToDecimal(RegexHelper.GetValue(Message, "101-Rtd=", ",")), 3);
            model.PH = Math.Round(Convert.ToDecimal(RegexHelper.GetValue(Message, "001-Rtd=", ",")), 3);
            model.LL = Math.Round(Convert.ToDecimal(RegexHelper.GetValue(Message, "B01-Rtd=", ",")), 3);
            return model;
        }
    }
}
