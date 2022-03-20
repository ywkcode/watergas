using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Core.WorkFlow
{

    /// <summary>
    /// 节点解析
    /// </summary>
    public class SchemeJson
    {
        public string title { get; set; }
        public Node[] nodes { get; set; }
        public Line[] lines { get; set; }
        public Areas areas { get; set; }
        public int initNum { get; set; }
        public string user { get; set; }

        public class Areas
        {
        }

        public class Node
        {
            public string id { get; set; }
            public string name { get; set; }
            public int left { get; set; }
            public int top { get; set; }
            public string type { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public bool alt { get; set; }
            public Userlist[] userlist { get; set; }
        }

        public class Userlist
        {
            public string username { get; set; }
            public int userid { get; set; }
            public string deptname { get; set; }
            public string deptcode { get; set; }
        }

        public class Line
        {
            public string id { get; set; }
            public string type { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string name { get; set; }
        } 
    } 
}

