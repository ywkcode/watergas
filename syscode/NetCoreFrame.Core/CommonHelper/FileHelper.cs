using System;
using System.IO;
namespace NetCoreFrame.Core.CommonHelper
{
    public static class FileHelper
    {

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="orignFile">原始文件</param>
        /// <param name="newFile">新文件路径</param>
        public static void FileCoppy(string orignFile, string newFile)
        {
            if (string.IsNullOrEmpty(orignFile))
            {
                throw new ArgumentException(orignFile);
            }
            if (string.IsNullOrEmpty(newFile))
            {
                throw new ArgumentException(newFile);
            }
            System.IO.File.Copy(orignFile, newFile, true);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">路径</param>
        public static void FileDel(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(path);
            }
            System.IO.File.Delete(path);
        }
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="orignFile">原始路径</param>
        /// <param name="newFile">新路径</param>
        public static void FileMove(string orignFile, string newFile)
        {
            if (string.IsNullOrEmpty(orignFile))
            {
                throw new ArgumentException(orignFile);
            }
            if (string.IsNullOrEmpty(newFile))
            {
                throw new ArgumentException(newFile);
            }
            System.IO.File.Move(orignFile, newFile);
        }
        //创建路径
        public static void CreatePath(string FilePath)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
        }
        //创建文件
        public static void CreateFile(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }
        }

        /// <summary>
        /// 获取路径上一级
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="PrePage"></param>
        /// <returns></returns>
        public static string GetPrePath(string Path, int PrePage)
        {
            string[] RetPathArr = Path.Split('\\');
            string RetPath = "";
            for (int i = 0; i < RetPathArr.Length - PrePage; i++)
            {
                RetPath += RetPathArr[i] + "\\";
            }
            return RetPath.TrimEnd('\\');

        }
    }
}
