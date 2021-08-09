using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrjWebJWTandSwwage.DAL;
using PrjWebJWTandSwwage.IBLL;
using PrjWebJWTandSwwage.Models;

namespace PrjWebJWTandSwwage.BLL
{
    public class UserBLL : IUserBLL
    {
        public UserInfoModels CheckUser(string name, string pwd)
        {
            return new UserDAL().CheckUser(name, pwd);
        }

        public UserInfoModels GetUserDate(string id)
        {
            return new UserDAL().GetUserDate(id);
        }

        public List<UserInfoModels> GetUserinfoDate(UserInfoModels model)
        {
            return new UserDAL().GetUserinfoDate(model);
        }
    }
}
