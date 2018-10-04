using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu.Common
{
    public class PrimeFactor
    {
        public long Factor { get; set; }
        public int Exponent { get; set; }
        public long Value { get { return (long)Math.Pow(Factor, Exponent); } }

        public override string ToString() {
            return $"{Factor}^{Exponent}";
        }
    }
}
