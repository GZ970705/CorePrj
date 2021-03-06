using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace PrjWebJWTandSwwage.JWT
{
    /// <summary>
    /// token生成类
    /// </summary>
    public class TokenHelper : ITokenHelper
    {
        private  readonly IOptions<JWTConfig> _options;

        public TokenHelper(IOptions<JWTConfig> options)
        {
            this._options = options;
        }
        /// <summary>
        /// 根据一个对象通过反射提供负载生成token
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="user"></param>
        /// <returns></returns>
        public  TnToken CreateToken<T>(T user) where T : class
        {
            //携带的负载部分，类似一个键值对
            List<Claim> claims = new List<Claim>();
            //这里我们用反射把model数据提供给它
            foreach (var item in user.GetType().GetProperties())
            {
                var obj = item.GetValue(user);
                string value = string.Empty;
                if (obj != null)
                {
                    value = obj.ToString();
                }
                claims.Add(new Claim(item.Name, value));
            }
            //创建token
            return CreateToken(claims);
        }

        /// <summary>
        /// 根据键值对提供负载生成token
        /// </summary>
        /// <param name="keyvalues"></param>
        /// <returns></returns>
        public  TnToken CreateToken(Dictionary<string, string> keyvalues)
        {
            //携带的负载部分，类似一个键值对
            List<Claim> claims = new List<Claim>();
            foreach (var item in keyvalues)
            {
                claims.Add(new Claim(item.Key, item.Value));
            }
            return CreateTokenString(claims);
        }
        /// <summary>
        /// 验证身份 验证签名的有效性
        /// </summary>
        /// <param name="encodeJwt"></param>
        /// <param name="validate">自定义各类验证； 是否包含那种申明，或者申明的值</param>
        /// <returns></returns>
        public bool ValiToken(string encodeJwt, Func<Dictionary<string, string>, bool> validate = null)
        {
            var success = true;
            var jwtArr = encodeJwt.Split('.');
            if (jwtArr.Length < 3)//数据格式都不对直接pass
            {
                return false;
            }
            var header = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[0]));
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[1]));
            //配置文件中取出来的签名秘钥
            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(_options.Value.IssuerSigningKey));
            //验证签名是否正确（把用户传递的签名部分取出来和服务器生成的签名匹配即可）
            success = success && string.Equals(jwtArr[2], 
                Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1])))));
            if (!success)
            {
                return success;//签名不正确直接返回
            }
            //其次验证是否在有效期内（也应该必须）
            var now = ToUnixEpochDate(DateTime.UtcNow);
            success = success && (now >= long.Parse(payLoad["nbf"].ToString()) && now < long.Parse(payLoad["exp"].ToString()));
            //不需要自定义验证不传或者传递null即可
            if (validate == null)
                return true;
            //再其次 进行自定义的验证
            success = success && validate(payLoad);
            return success;
        }

        /// <summary>
        /// 时间转换
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        /// <summary>
        /// 自定义验证身份 验证签名的有效性
        /// </summary>
        /// <param name="encodeJwt">token</param>
        /// <param name="validate">自定义各类验证； 是否包含那种申明，或者申明的值</param>
        /// <param name="action">需要返回的值</param>
        /// <returns></returns>
        public TnToken.TokenType ValiTokenState(string encodeJwt, Func<Dictionary<string, string>, bool> validate, Action<Dictionary<string, string>> action)
        {
            var jwtstr = encodeJwt.Split(':');//包含key:value
            var jwtArr = jwtstr[0].Split('.');
            if (jwtArr.Length < 3)//数据格式都不对直接pass
            {
                return TnToken.TokenType.Fail;
            }
            var header = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[0]));
            var payLoad = JsonConvert.DeserializeObject<Dictionary<string, string>>(Base64UrlEncoder.Decode(jwtArr[1]));
            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(_options.Value.IssuerSigningKey));
            //验证签名是否正确（把用户传递的签名部分取出来和服务器生成的签名匹配即可）
            if (!string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1]))))))
            {
                return TnToken.TokenType.Fail;
            }
            //其次验证是否在有效期内（必须验证）
            var now = ToUnixEpochDate(DateTime.UtcNow);
            if (!(now >= long.Parse(payLoad["nbf"].ToString()) && now < long.Parse(payLoad["exp"].ToString())))
            {
                return TnToken.TokenType.Expired;
            }

            //不需要自定义验证不传或者传递null即可
            if (validate == null)
            {
                action(payLoad);
                return TnToken.TokenType.Ok;
            }
            //再其次 进行自定义的验证
            if (!validate(payLoad))
            {
                return TnToken.TokenType.Fail;
            }
            //可能需要获取jwt摘要里边的数据，封装一下方便使用
            action(payLoad);
            return TnToken.TokenType.Ok;
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        private  TnToken CreateTokenString(List<Claim> claims)
        {
            var NewDate = DateTime.Now;
            var expires = NewDate.Add(TimeSpan.FromMinutes(_options.Value.AccessTokenExpiresMinutes));
            var token = new JwtSecurityToken(
                issuer: _options.Value.Issuer,//Token发布者
                audience: _options.Value.Audience,//Token接受者
                claims: claims,//携带的负载
                notBefore: NewDate,//当前时间token生成时间
                expires: expires,//过期时间
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.IssuerSigningKey)), SecurityAlgorithms.HmacSha256)
                );
            return new TnToken{ TokenStr = new JwtSecurityTokenHandler().WriteToken(token), Expires = expires };
        }
    }
}
