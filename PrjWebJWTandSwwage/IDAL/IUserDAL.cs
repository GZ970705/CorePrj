
using PrjWebJWTandSwwage.IDAL.Common;
using PrjWebJWTandSwwage.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PrjWebJWTandSwwage.IDAL
{
    /// <summary>
    /// 用户数据层
    /// </summary>
    public interface IUserDAL : IFoundationDAL
    {
        #region 用户
        /// <summary>
        /// 用户查询
        /// </summary>
        /// <param name="us"></param>
        /// <returns></returns>
        IList<Users> GetUsr(Users us);

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
        /// <param name="us"></param>
        /// <returns></returns>
        decimal GetUserCount(Users us);
        #endregion
    }
}
