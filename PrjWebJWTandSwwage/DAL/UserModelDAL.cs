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
using PrjWebJWTandSwwage.DAL.Common;

namespace PrjWebJWTandSwwage.DAL
{
    public class UserModelDAL : FoundationDAL, IUserModelDAL
    {
        public UserInfoModels CheckUser(string name, string pwd)
        {
            UserInfoModels data = db.Queryable<UserInfoModels>().Where(a => a.UserName == name && a.Password == pwd).First();
            return data;
        }

        public UserInfoModels GetUserDate(string id)
        {
            UserInfoModels data = db.Queryable<UserInfoModels>().Where(a => a.id.ToString() == id).First();
            return data;
        }

        public List<UserInfoModels> GetUserinfoDate(UserInfoModels model)
        {
            List<UserInfoModels> list = new List<UserInfoModels>();
            return list;
        }

        public bool UpdateUser(UserInfoModels us)
        {
            
            var data = db.Storageable(us).Saveable().ToStorage();
            bool isadd = data.AsInsertable.ExecuteCommand() >0;
            bool isedit = data.AsUpdateable.ExecuteCommand() > 0;
            bool isok = false;
            if (isadd)
            {
                isok = true;
            }
            else if(isedit)
            {
                isok = true;
            }
            return isok;
        }
    }
}
