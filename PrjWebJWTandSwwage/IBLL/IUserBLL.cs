
using PrjWebJWTandSwwage.IBLL.Common;
using PrjWebJWTandSwwage.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.IBLL
{
    /// <summary>
    /// 用户逻辑层
    /// </summary>
    public interface IUserBLL: IFoundationBLL
    {
        #region 用户       

        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="htUsr"></param>
        /// <returns></returns>
        IList<Users> GetUsr(Users htUsr);

        /// <summary>
        /// 用户更新
        /// </summary>
        /// <param name="prmUsr"></param>
        /// <returns></returns>
        bool UpdateUsr(List<Users> prmUsr);

        /// <summary>
        /// 用户删除
        /// </summary>
        /// <param name="prmUsr"></param>
        /// <returns></returns>
        bool DeleteUsr(Users prmUsr);
        /// <summary>
        /// 用户总数
        /// </summary>
        /// <param name="htUsr"></param>
        /// <returns></returns>
        decimal GetUserCount(Users htUsr);
        #endregion
    }
}
