using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumOfMultiple
{
    /// <summary>
    /// Specifies technqiue by which sum of multiples
    /// need to be calculated
    /// </summary>
    public enum Technique
    {
        USING_FORLOOP = 1,
        USING_MATHEMATICAL_FORMULA = 2,
        USING_LINQ = 3
    }
    /// <summary>
    /// ErrorCode
    /// </summary>
    public enum ERRORCODE
    {
       INVALID_OPEARTION=-1

    }      
    /// <summary>
    /// Summation Utility class
    /// </summary>
    public class SumUtil
    {
        /// <summary>
        /// Calcaultes sum of multiples of 3 and 5 below a limit
        /// </summary>
        /// <param name="limit">limit below which sum is calculated</param>
        /// <param name="technique">technique used for calculation logic</param>
        /// <param name="executionTime">execution time for the code</param>
        /// <returns>sum of multiples of 3 and 5 below limit</returns>
        public long CalculateSum(int limit, int technique, out string executionTime)
        {
            long sum = 0;
            var watch = new Stopwatch();
            watch.Start();

            switch (technique)
            {
                case (int)Technique.USING_MATHEMATICAL_FORMULA:
                    sum =CalcualteSumByFormula(limit);
                    break;
                case (int)Technique.USING_FORLOOP:
                    sum=CalcualteSumByForLoop(limit);
                    break;
                case (int)Technique.USING_LINQ:
                    sum=CalcualteSumByLINQ(limit);
                    break;
                default:
                    sum = (int)ERRORCODE.INVALID_OPEARTION;
                    break;
            }  

            watch.Stop();
            executionTime = watch.Elapsed.TotalMilliseconds.ToString();

            return sum;
           
        }

        /// <summary>
        /// Calculates Sum by mathematical formula
        /// </summary>
        /// <param name="N">limit</param>
        /// <returns>sum of multiples of 3 and 5 below limit</returns>
        private long CalcualteSumByFormula(int N)
        {
            //using mathematical formula
            long sum = 0;
            long num3Multiples = (N - 1) / 3;
            long num5Multiples = (N - 1) / 5;
            long num15Multiples = (N - 1) / 15;

            long sumofNum3Multiples = (3 * num3Multiples * (num3Multiples + 1)) / 2;
            long sumofNum5Multiples = (5 * num5Multiples * (num5Multiples + 1)) / 2;
            long sumofNum15Multiples = (15 * num15Multiples * (num15Multiples + 1)) / 2;

            sum = (sumofNum3Multiples + sumofNum5Multiples) - sumofNum15Multiples;
            return sum;
        }
        /// <summary>
        /// Calculates sum of multiples using for loop
        /// </summary>
        /// <param name="N">limit</param>
        /// <returns>sum of multiples of 3 and 5 below limit </returns>
        private long CalcualteSumByForLoop(int N)
        {
            long sum = 0;

            for (int i = 1; i < N; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }

            return sum;
        }
        /// <summary>
        /// Calculates sum of multiples using LINQ
        /// </summary>
        /// <param name="N">limit</param>
        /// <returns>sum of multiples of 3 and 5 below limit </returns>
        private long CalcualteSumByLINQ(int N)
        {
            //using LINQ
            return  Enumerable.Range(0, N)
                        .Where(n => n % 3 == 0 || n % 5 == 0).Sum();
        }
    }
}
