using System;

namespace Codentia.Common.Helper
{    
    /// <summary>
    /// Maths Helper
    /// </summary>
    public static class MathsHelper
    {
        /// <summary>
        /// ClassicRounding - rounding to 1 or 2 decimal places, where the next significant digit is used for rounding
        /// where 0.381 becomes 0.38, 0.385 becomes 0.39 and 0.389 becomes 0.39
        /// </summary>
        /// <param name="amount">amount to be processed</param>
        /// <param name="decimalPlaces">decimal Places</param>
        /// <returns>decimal after classic rounding</returns>
        public static decimal ClassicRounding(decimal amount, int decimalPlaces)
        {
            if (decimalPlaces < 1 || decimalPlaces > 2)
            {
                throw new ArgumentException("decimalPlaces must be 1 or 2");
            }

            if (amount == 0M)
            {
                return 0M;
            }

            decimal multiplyfactor = 10M;
            if (decimalPlaces == 2)
            {
                multiplyfactor = 100M;
            }

            decimal floorValue = Math.Floor(amount * multiplyfactor);
            decimal workingValue;

            if (((amount * multiplyfactor) - floorValue) >= .5M)
            {
                workingValue = floorValue + 1;
            }
            else
            {
                workingValue = floorValue;
            }

            return workingValue / multiplyfactor;            
        }
    }
}
