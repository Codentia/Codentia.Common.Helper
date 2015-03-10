using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
    /// <summary>
    /// Test ArrayHelper
    /// </summary>
    [TestFixture]
    public class ArrayHelperTest
    {
        /// <summary>
        /// Scenario: ConvertOneColumnDataTableToArray called with a null datatable
        /// Expected: A null is returned
        /// </summary>
        [Test]
        public void _001_ConvertOneColumnDataTableToArray_NullDataTable()
        {
            string[] arr = ArrayHelper.ConvertOneColumnDataTableToArray(null);
            Assert.That(arr, Is.Null, "Null Expected");
        }

        /// <summary>
        /// Scenario: ConvertOneColumnDataTableToArray called with an empty datatable
        /// Expected: A null is returned
        /// </summary>
        [Test]
        public void _002_ConvertOneColumnDataTableToArray_EmptyDataTable()
        {
            DataTable dt = new DataTable();
            string[] arr = ArrayHelper.ConvertOneColumnDataTableToArray(dt);
            Assert.That(arr, Is.Null, "Null Expected");
        }

        /// <summary>
        /// Scenario: ConvertOneColumnDataTableToArray called with a known list of data
        /// Expected: String array returned is same as DataTable list
        /// </summary>
        [Test]
        public void _003_ConvertOneColumnDataTableToArray_FullDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Col"));
           
            DataRow dr1 = dt.NewRow();
            dr1["Col"] = "Row1";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["Col"] = "Row2";
            dt.Rows.Add(dr2);
            
            DataRow dr3 = dt.NewRow();
            dr3["Col"] = "Row3";
            dt.Rows.Add(dr3);

            string[] arr = ArrayHelper.ConvertOneColumnDataTableToArray(dt);
              
            Assert.That(arr.Length, Is.EqualTo(3), "Array length of 3 expected");
            Assert.That(arr[0], Is.EqualTo("Row1"), "Row1 expected");
            Assert.That(arr[1], Is.EqualTo("Row2"), "Row2 expected");
            Assert.That(arr[2], Is.EqualTo("Row3"), "Row3 expected");
        }

        /// <summary>
        /// Scenario: Call string type version of Compare1DArray with different length arrays
        /// Expected: false is returned
        /// </summary>
        [Test]
        public void _004_Compare1DArray_String_DifferentLengths()
        {
            string[] arr1 = new string[] { "1", "2", "3", "4" };
            string[] arr2 = new string[] { "1", "2", "3" };

            Assert.That(ArrayHelper.Compare1DArray<string>(arr1, arr2), Is.False, "false expected");
        }     

        /// <summary>
        /// Scenario: Call string type version of Compare1DArray with same length arrays and same values
        /// Expected: True is returned
        /// </summary>
        [Test]
        public void _005_Compare1DArray_String_SameLengthSameValues()
        {
            string[] arr1 = new string[] { "1", "2", "3" };
            string[] arr2 = new string[] { "1", "2", "3" };

            Assert.That(ArrayHelper.Compare1DArray<string>(arr1, arr2), Is.True, "true expected");
        }

        /// <summary>
        /// Scenario: Call string type version of Compare1DArray with same length arrays and different values
        /// Expected: False is returned
        /// </summary>
        [Test]
        public void _006_Compare1DArray_String_SameLengthDifferentValues()
        {
            string[] arr1 = new string[] { "1", "3", "2" };
            string[] arr2 = new string[] { "1", "2", "3" };

            Assert.That(ArrayHelper.Compare1DArray<string>(arr1, arr2), Is.False, "false expected");
        }

        /// <summary>
        /// Scenario: Call int type version of Compare1DArray with different length arrays
        /// Expected: false is returned
        /// </summary>
        [Test]
        public void _007_Compare1DArray_Int_DifferentLengths()
        {
            int[] arr1 = new int[] { 1, 2, 3, 4 };
            int[] arr2 = new int[] { 1, 2, 3 };

            Assert.That(ArrayHelper.Compare1DArray<int>(arr1, arr2), Is.False, "false expected");
        }    

        /// <summary>
        /// Scenario: ConvertOneColumnDataTableToArray called with a known list of data
        /// Expected: String array returned is same as DataTable list
        /// </summary>
        [Test]
        public void _008_Compare1DArray_Int_SameLengthSameValues()
        {
            int[] arr1 = new int[] { 1, 2, 3 };
            int[] arr2 = new int[] { 1, 2, 3 };

            Assert.That(ArrayHelper.Compare1DArray<int>(arr1, arr2), Is.True, "true expected");
        }

        /// <summary>
        /// Scenario: Call int type version of Compare1DArray with same length arrays and different values
        /// Expected: False is returned
        /// </summary>
        [Test]
        public void _009_Compare1DArray_Int_SameLengthDifferentValues()
        {
            int[] arr1 = new int[] { 1, 3, 2 };
            int[] arr2 = new int[] { 1, 2, 3 };

            Assert.That(ArrayHelper.Compare1DArray<int>(arr1, arr2), Is.False, "false expected");
        }

        /// <summary>
        /// Scenario: Call string type version of Compare1DArray with null parameters
        /// Expected: False is returned
        /// </summary>
        [Test]
        public void _010_Compare1DArray_String_NullParameters()
        {
            string[] arr1 = new string[] { "1", "3", "2" };
            string[] arr2 = new string[] { "1", "2", "3" };

            Assert.That(delegate { bool retval = ArrayHelper.Compare1DArray<string>(null, null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("Both parameters must be not null"));
            Assert.That(delegate { bool retval = ArrayHelper.Compare1DArray<string>(arr1, null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("Both parameters must be not null"));
            Assert.That(delegate { bool retval = ArrayHelper.Compare1DArray<string>(null, arr2); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("Both parameters must be not null"));
        }  
    }
}
