using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrjWebJWTandSwwage.JWT
{
   public interface ITokenHelper
    {
        /// <summary>
        /// 根据一个对象通过反射提供负载生成token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        TnToken CreateToken<T>(T user) where T : class;

        /// <summary>
        /// 根据键值对提供负载生成token
        /// </summary>
        /// <param name="keyvalues"></param>
        /// <returns></returns>
        TnToken CreateToken(Dictionary<string, string> keyvalues);

        /// <summary>
        /// Token验证
        /// </summary>
        /// <param name="encodeJwt">token</param>
        /// <param name="validate">自定义各类验证； 是否包含那种申明，或者申明的值</param>
        /// <returns></returns>
        bool ValiToken(string encodeJwt, Func<Dictionary<string, string>, bool> validate=null);

        TnToken.TokenType ValiTokenState(string encodeJwt, Func<Dictionary<string, string>, bool> validate, Action<Dictionary<string, string>> action);
    }
}
