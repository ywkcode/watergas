
using NetCoreFrame.Entity.BaseEntity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Entity.FrameEntity
{
    /// <summary>
    /// 模块表名
    /// </summary>
    [Table("frame_tableinfo")]
    public class Frame_TableInfo: CoreBaseEntity
    {
        [Display(Name = "表Id")]
        [Description("表Id")]
        [Column("tableid")]
        public int TableId { get; set; }

        [Display(Name = "表名")]
        [Description("表名")]
        [StringLength(100, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("tablename")]
        public string TableName { get; set; }

        [Display(Name = "数据库实体表名")]
        [Description("数据库实体表名")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("physicalname")]
        public string PhysicalName { get; set; }

        [Display(Name = "项目名称")]
        [Description("项目名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("projectname")]
        public string ProjectName { get; set; }

        [Display(Name = "控制器名称")]
        [Description("控制器名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("controllername")]
        public string ControllerName { get; set; }
    }
}
