using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrjWebJWTandSwwage.DAL;
using PrjWebJWTandSwwage.IBLL;
using PrjWebJWTandSwwage.Models;

namespace PrjWebJWTandSwwage.BLL
{
    public class UserModelBLL : IUserModelBLL
    {
        public UserInfoModels CheckUser(string name, string pwd)
        {
            return new UserModelDAL().CheckUser(name, pwd);
        }

        public UserInfoModels GetUserDate(string id)
        {
            return new UserModelDAL().GetUserDate(id);
        }

        public List<UserInfoModels> GetUserinfoDate(UserInfoModels model)
        {
            return new UserModelDAL().GetUserinfoDate(model);
        }

        public bool UpdateUser(UserInfoModels us)
        {
            return new UserModelDAL().UpdateUser(us);
        }
    }
}
