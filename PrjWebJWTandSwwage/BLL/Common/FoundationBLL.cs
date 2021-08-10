
using PrjWebJWTandSwwage.Common;
using PrjWebJWTandSwwage.IBLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.BLL.Common
{
    public class FoundationBLL : IFoundationBLL
    {
        #region 方法
        /// <summary>
        /// 处理校验信息
        /// </summary>
        /// <param name="prmValidateReturn">校验信息</param>
        public virtual void HandleValidate(ValiReturnInfo prmValidateReturn)
        {
            if (!prmValidateReturn.IsValidated)
                throw new MessageException(prmValidateReturn.ReturnInfo);
        }
        #endregion
    }
}
