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
        public static decimal ReverseSign(this decimal a)
        {
            return -a;
        }

        /// <summary>
        /// aの二乗を求めます。
        /// </summary>
        public static decimal Square(this decimal a)
        {
            return a * a;
        }

        /// <summary>
        /// 1/aを求めます。
        /// </summary>
        public static decimal DivideBy(this decimal a)
        {
            return 1 / a;
        }

        /// <summary>
        /// aの百分率パーセンテージを求めます。
        /// </summary>
        public static decimal CalculatePercentage(this decimal a)
        {
            return a / 100;
        }

        // DONE: コメント
        /// <summary>
        /// aの平方根を求めます。
        /// </summary>
        public static decimal SquareRoot (this decimal a)
        {
            var b = Math.Sqrt((double)a);
            return (decimal)b;
        }
    }
}
