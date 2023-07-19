using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator2
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// 正負を反転させます。
        /// </summary>
        /// <param name="a">正負を反転させたい数値です。</param>
        public static decimal ReverseSign(this decimal a)
        {
            return -a;
        }

        public static decimal Square(this decimal a) 
        { 
            return a * a; 
        }
    }
}
