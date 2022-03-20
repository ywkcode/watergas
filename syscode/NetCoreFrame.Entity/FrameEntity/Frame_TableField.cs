using NetCoreFrame.Entity.BaseEntity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
namespace NetCoreFrame.Entity.FrameEntity
{
    [Table("frame_tablefield")]
    public class Frame_TableField : CoreBaseEntity
    {
        [Display(Name = "表Id")]
        [Description("表Id")]
        [Column("tableid")]
        public int TableId { get; set; }

        [Display(Name = "字段名")]
        [Description("字段名")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("fieldname")]
        public string FieldName { get; set; }

        [Display(Name = "字段类型")]
        [Description("字段类型")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("fieldtype")]
        public string FieldType { get; set; }

        [Display(Name = "字段长度")]
        [Description("字段长度")]
        [Column("fieldlength")]

        public int FieldLength { get; set; }

        [Display(Name = "Decimal长度")]
        [Description("Decimal长度")]
        [Column("decimallength")]
        public int DecimalLength { get; set; }

        [Display(Name = "中文名")]
        [Description("中文名")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("fielddisplayname")]
        public string FieldDisplayName { get; set; }

        [Display(Name = "字段展现名称")]
        [Description("字段展现名称")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("fielddisplaytype")]
        public string FieldDisplayType { get; set; }

        [Display(Name = "数据源代码")]
        [Description("数据源代码")]
        [StringLength(50, ErrorMessage = "{0}最多输入{1}个字符")]
        [Column("datasource_codeName")]
        public string DataSource_CodeName { get; set; }

        [Display(Name = "是否添加显示")]
        [Description("是否添加显示")]
        [Column("isshowinadd")]
        public int IsShowInAdd { get; set; }

        [Display(Name = "是否必填")]
        [Description("是否必填")]
        [Column("ismustfill")]

        public int IsMustFill { get; set; }

        [Display(Name = "是否查询")]
        [Description("是否查询")]
        [Column("issearchcondition")]

        public int IsSearchCondition { get; set; }
    }
}
