
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
    class UserDAL: FoundationDAL, IUserDAL
    {

        #region 用户
        public IList<Users> GetUsr(Users htUsr)
        {
            List<Users> user = db.Queryable<Users, Companys, Tlts, Users>((us, cs, ts, us1) => new JoinQueryInfos(JoinType.Left, us.CompanyID == cs.ID, JoinType.Left, us.Tlt == ts.ID && us.CompanyID == ts.CompanyID, JoinType.Left, us1.Uptr == Convert.ToString(us1.ID))).WhereIF(string.IsNullOrEmpty(htUsr.LgnName),it=>it.LgnName== htUsr.LgnName).WhereIF(string.IsNullOrEmpty(htUsr.Name),it=>it.Name== htUsr.Name).WhereIF(string.IsNullOrEmpty(htUsr.Type),it=>it.Type==htUsr.Type).OrderBy(us=> us.ID).ToPageList(htUsr.pageIndex, htUsr.pageSize);
            return user;
        }

        public bool UpdateUsr(List<Users> prmUsr)
        {
            bool isok = false;
            if (prmUsr.Count==0) {

                return isok;
            }
            else
            {
                foreach (Users item in prmUsr)
                {
                    item.Version += 1;
                    var data = db.Storageable(item).WhereColumns(it => it.IsNew).Saveable().ToStorage();
                    data.AsInsertable.ExecuteCommand();
                    data.AsUpdateable.ExecuteCommand();
                    isok = true;
                }
            }
            return isok;
        }

        public bool DeleteUsr(Users prmUsr)
        {
            bool isok = db.Deleteable<Users>().In(new Users { ID = prmUsr.ID}).ExecuteCommand()>0;
            return isok;
        }

        public decimal GetUserCount(Users htUsr)
        {
            int data = db.Queryable<Users, Companys, Tlts, Users>((us, cs, ts, us1) => new JoinQueryInfos(JoinType.Left, us.CompanyID == cs.ID, JoinType.Left, us.Tlt == ts.ID && us.CompanyID == ts.CompanyID, JoinType.Left, us1.Uptr == Convert.ToString(us1.ID))).WhereIF(string.IsNullOrEmpty(htUsr.LgnName), it => it.LgnName == htUsr.LgnName).WhereIF(string.IsNullOrEmpty(htUsr.Name), it => it.Name == htUsr.Name).WhereIF(string.IsNullOrEmpty(htUsr.Type), it => it.Type == htUsr.Type).OrderBy(us => us.ID).Count();
            return data;
        }
        #endregion
    }
}
