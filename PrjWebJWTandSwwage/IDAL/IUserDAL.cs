using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrjWebJWTandSwwage.Models;

namespace PrjWebJWTandSwwage.IDAL
{
    public interface IUserDAL
    {
        UserInfoModels CheckUser(string name, string pwd);
        UserInfoModels GetUserDate(string id);
        List<UserInfoModels> GetUserinfoDate(UserInfoModels model);
    }
}
