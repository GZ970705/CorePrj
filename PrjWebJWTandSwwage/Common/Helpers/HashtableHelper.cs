using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrjWebJWTandSwwage.Common.Helpers
{
    /// <summary>
    /// 键值对辅助类
    /// </summary>
    public static class HashtableHelper
    {
        /// <summary>
        /// 创建键值对
        /// </summary>
        /// <param name="args">键值对</param>
        /// <returns></returns>
        public static Hashtable Create(params object[] args)
        {
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < args.Length; i++)
                if (i % 2 == 1) hashtable[args[i - 1]] = args[i];
            return hashtable;
        }

        /// <summary>
        /// 追加键值
        /// </summary>
        /// <param name="hashtable"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Hashtable Add(Hashtable hashtable, params object[] args)
        {
            if (hashtable == null) hashtable = new Hashtable();
            for (int i = 0; i < args.Length; i++)
                if (i % 2 == 1) hashtable[args[i - 1]] = args[i];
            return hashtable;
        }
        /// <summary>
        /// 复制键值对
        /// </summary>
        /// <param name="hashtable"></param>
        /// <returns></returns>
        public static Hashtable Copy(Hashtable hashtable)
        {
            Hashtable h = new Hashtable();
            foreach (DictionaryEntry item in hashtable)
                h[item.Key] = item.Value;
            return h;
        }
    }
}
