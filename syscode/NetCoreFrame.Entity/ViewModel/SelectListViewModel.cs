using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreFrame.Entity.ViewModel
{
    /// <summary>
    /// 下拉框视图
    /// </summary>
    public class SelectListViewModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 下拉框值显示
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 下拉框值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChoose { get; set; }
    }

    /// <summary>
    /// 多选
    /// </summary>
    public class MultiSelectlistViewModel
    { 
       public string name { get; set; }

        public string value { get; set; }

        public string type { get; set; }

        public string selected { get; set; }
    }
}
