using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_tester
{
    class Expression
    {
        public float Divident { get; }
        public float Divisor { get; }
        public float ExpectedResult { get; }

    public Expression(float divident, float divisor, float expectedResult)
        {
            this.Divident = divident;
            this.Divisor = divisor;
            this.ExpectedResult = expectedResult;
        }
    }
}
