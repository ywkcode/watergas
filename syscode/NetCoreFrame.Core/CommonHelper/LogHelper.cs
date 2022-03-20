﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreFrame.Core.CommonHelper
{
    /// <summary>
    /// 自定义日志
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 写日志  await Task.Run(()=>WriteLogs("内容"))
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static void WriteLogs(string content)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string LogName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace.Split('.')[0];
            string[] sArray = path.Split(new string[] { LogName }, StringSplitOptions.RemoveEmptyEntries);
            string aa = sArray[0] + "\\" + LogName + "Log\\";
            path = aa;
            if (!string.IsNullOrEmpty(path))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = path + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";//
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
                if (File.Exists(path))
                {
                    StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default);
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "----" + content + "\r\n");
                    sw.Close();
                }
            }
        }
    }
}
