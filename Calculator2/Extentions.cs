using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator2
{
    static class Extentions
    {
        public static decimal ReverseSign(this decimal a)
        {
            return -a;
        }
    }
}
