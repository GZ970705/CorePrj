
using PrjWebJWTandSwwage.Base;
using PrjWebJWTandSwwage.Common;
using PrjWebJWTandSwwage.IDAL.Common;
using PrjWebJWTandSwwage.SqlTools;
using System.Collections;
using System.Collections.Generic;

namespace PrjWebJWTandSwwage.DAL.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class FoundationDAL : SqlSugarConfig,IFoundationDAL
    {
        #region 公共方法
        /// <summary>
        /// 执行SQL语句 并处理Version
        /// 为Save和Update提供
        /// </summary>
        /// <param name="prmStatementName"></param>
        /// <param name="prmParamObject"></param>
        /// <returns></returns>
        public int ExecuteSQLForVersion(string prmStatementName, VersionInfo prmParamObject)
        {
            int curRet = 0;//ExecuteSQL(prmStatementName, prmParamObject);
            if (curRet <= 0)
            {
                throw new MessageException("保存失败！数据已经发生改变，请刷新数据！");
            }
            prmParamObject.Version++;
            return curRet;
        }
        #endregion
    }
}
