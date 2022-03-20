using SqlSugar;
using System;

namespace NetCoreFrame.WebApi.Model
{
    public class BaseModel
    {
        [SugarColumn(IsPrimaryKey = true, ColumnDataType = "varchar(50)")]
        public string ID { get; set; }

        public bool IsError { get; set; }
    }
}
