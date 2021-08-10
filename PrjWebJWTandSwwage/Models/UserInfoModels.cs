using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.Models
{
    public class UserInfoModels
    {
        [Key]
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Sex { get; set; }
        public string Remark { get; set; }

        public string PosId { get; set; }
        public string Test { get; set; }
        public string Createby { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
