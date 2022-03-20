using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreFrame.Entity.BaseEntity
{
    /// <summary>
    /// 
    /// </summary>
    public class TopBaseEntity
    {
        [Description("唯一标识")]

        private string _id;

        [Column("id")]
        [StringLength(50)]
        public string ID
        {
            get
            {
                if (_id == string.Empty)
                {
                    _id = Guid.NewGuid().ToString();
                }
                return _id;
            }
            set
            {
                _id = value;
            }
        }
    }
}
