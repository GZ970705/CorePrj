using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrjWebJWTandSwwage.Models;

namespace PrjWebJWTandSwwage.IBLL
{
   public interface IUserBLL
    {
        UserInfoModels CheckUser(string name, string pwd);
        UserInfoModels GetUserDate(string id);
        List<UserInfoModels> GetUserinfoDate(UserInfoModels model);
    }
}
