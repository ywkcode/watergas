using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.SocketConsole.Model
{
    public class DataRequest
    {
       
    }

    public class GasRequest
    {
       
        public decimal H2S { get; set; }

  
        public decimal HCL { get; set; }

      
        public decimal CL2 { get; set; }

      
        public decimal NH3 { get; set; }
    }

    public class QualityRequest
    {
        
        public decimal TOC { get; set; }

      
        public decimal AD { get; set; }

        
        public decimal ZL { get; set; }

      
        public decimal PH { get; set; }

       
        public decimal LL { get; set; }
    }

}
