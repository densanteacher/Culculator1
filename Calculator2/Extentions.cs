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
    static class Extentions
    {
        // TODO: コメント
        public static decimal ReverseSign(this decimal a)
        {
            return -a;
        }
    }
}
