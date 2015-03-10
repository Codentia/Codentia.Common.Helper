using System;
using System.Data;

namespace Codentia.Common.Helper
{
    /// <summary>
    /// Array Utilities
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// Take a One column data table and convert row values to a strin array
        /// </summary>
        /// <param name="dt">A one column data table</param>
        /// <returns>string array</returns>
        public static string[] ConvertOneColumnDataTableToArray(DataTable dt)
        {
            string[] arr = null;

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    string[] arrWorking = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        arrWorking[i] = Convert.ToString(dr[0]);
                    }

                    arr = arrWorking;
                }
            }

            return arr;
         }

        /// <summary>
        /// Generic procedure to compare to 1 dimensional arrays
        /// </summary>
        /// <typeparam name="TArray">Array type</typeparam>
        /// <param name="arr1">one dimensional array 1</param>
        /// <param name="arr2">one dimensional array 2</param>
        /// <returns>bool, true if same, otherwise false</returns>
        public static bool Compare1DArray<TArray>(TArray[] arr1, TArray[] arr2)
        {
            if (arr1 == null || arr2 == null)
            {
                throw new ArgumentException("Both parameters must be not null");
            }

            if (arr1.Length != arr2.Length)
            {
                return false;
            }

            bool retVal = true;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (!arr1[i].Equals(arr2[i]))
                {
                    retVal = false;
                }
            }

            return retVal;
        }
    }
}
