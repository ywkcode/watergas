using SqlSugar;
using System;

namespace NetCoreFrame.SocketConsole.Model
{
    public class BaseModel
    {
        [SugarColumn(IsPrimaryKey = true, ColumnDataType = "varchar(50)")]
        public string ID { get; set; } =Guid.NewGuid().ToString(); 
        public bool IsError { get; set; }
    }
}
