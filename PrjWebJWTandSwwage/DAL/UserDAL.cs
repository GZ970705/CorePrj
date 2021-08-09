using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using PrjWebJWTandSwwage.IDAL;
using PrjWebJWTandSwwage.Models;
using PrjWebJWTandSwwage.SqlTools;
using Microsoft.Extensions.Options;
using SqlSugar;

namespace PrjWebJWTandSwwage.DAL
{
    public class UserDAL : SqlSugarConfig, IUserDAL
    {
        public UserInfoModels CheckUser(string name, string pwd)
        {
            using var db = new SqlSugarClient(Config);
            UserInfoModels data = db.Queryable<UserInfoModels>().Where(a => a.UserName == name && a.Password == pwd).First();
            return data;
        }

        public UserInfoModels GetUserDate(string id)
        {
            using var db = new SqlSugarClient(Config);
            UserInfoModels data = db.Queryable<UserInfoModels>().Where(a => a.id.ToString() == id).First();
            return data;
        }

        public List<UserInfoModels> GetUserinfoDate(UserInfoModels model)
        {
            List<UserInfoModels> list = new List<UserInfoModels>();
            return list;
        }
    }
}
