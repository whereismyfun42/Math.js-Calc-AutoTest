using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_tester
{
    class CSVReader
    {
        private string _csvFilePath;

        public CSVReader(string csvFilePath)
        {
            this._csvFilePath = csvFilePath;
        }

        public List<Expression> ReadAllExpressions()
        {
            List<Expression> expressions = new List<Expression>();

            using (StreamReader sr = new StreamReader(_csvFilePath))
            {
                sr.ReadLine();

                string csvLine;
                while ((csvLine = sr.ReadLine()) != null)
                {
                    expressions.Add(ReadExpressionFromCSVLine(csvLine));
                }
            }

            return expressions;
        }

        public Expression ReadExpressionFromCSVLine(string csvLine)
        {
            string[] parts = csvLine.Split(',');

            float divident = float.Parse(parts[0]);
            float divisor = float.Parse(parts[1]);
            float expectedResult = float.Parse(parts[2]);

            return new Expression(divident, divisor, expectedResult);
        }
    }
}
