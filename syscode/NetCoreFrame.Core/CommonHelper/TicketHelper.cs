using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Core.CommonHelper
{
    public static class TicketHelper
    {
        public static string GetTicketNum()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0).ToString();
        }
    }
}
