using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator2
{
    // TODO: public
    // internal とかありますが、基本的には public か private でよいです。
    // internal とか protected とかしっかり書くと、最適化が聞きやすくなったりするメリットがあります。
    // TODO: なんのExtensionsか名前をつけた方が、それらしくなります。
    // TODO: タイポ
    public static class DecimalExtentions
    {
        // DONE: コメント
        /// <summary>
        /// 正負を反転させます。
        /// </summary>
        /// <param name="a">正負を反転させたい数値です。</param>
        /// <returns></returns>
        public static decimal ReverseSign(this decimal a)
        {
            return -a;
        }
    }
}
