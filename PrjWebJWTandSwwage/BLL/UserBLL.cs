
using PrjWebJWTandSwwage.BLL.Common;
using PrjWebJWTandSwwage.Common;
using PrjWebJWTandSwwage.IBLL;
using PrjWebJWTandSwwage.IDAL;
using PrjWebJWTandSwwage.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrjWebJWTandSwwage.BLL
{
    class UserBLL : FoundationBLL, IUserBLL
    {
        #region 注入对象
        public IUserDAL UserDAL { get; set; }
        #endregion

        #region IUsrBLL
        public IList<Users> GetUsr(Users htUsr)
        {
            return this.UserDAL.GetUsr(htUsr);
        }

        public bool DeleteUsr(Users prmUsr)
        {
            this.HandleValidate(DeleteValidate(prmUsr));
            return this.UserDAL.DeleteUsr(prmUsr);
        }
        
        public decimal GetUserCount(Users htUsr)
        {
            return this.UserDAL.GetUserCount(htUsr); ;
        }
        public bool UpdateUsr(List<Users> htUsr) {
            return this.UpdateUsr(htUsr);
        }
        #endregion
        #region 用户表校验
        /// <summary>
        /// 用户表保存校验
        /// </summary>
        /// <param name="prmUsr"></param>
        /// <returns></returns>
        public ValiReturnInfo SaveValidate(Users prmUsr)
        {
            return new ValiReturnInfo(true);
        }
        /// <summary>
        /// 用户表删除校验
        /// </summary>
        /// <param name="prmUsr"></param>
        /// <returns></returns>
        public ValiReturnInfo DeleteValidate(Users prmUsr)
        {
            return new ValiReturnInfo(true);
        }
        #endregion
    }
}
