using System;
using System.Collections.Generic;
using System.Text;
using Codentia.Common.Helper;
using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
    /// <summary>
    /// Unit testing framework for IntArrayFormatter
    /// </summary>
    [TestFixture]
    public class IntArrayFormatterTest
    {
        /// <summary>
        /// Scenario: int[] 1,2,3 formatted as a string with no delimiter
        /// Expected: "123"
        /// </summary>
        [Test]
        public void _001_NoDelimiter()
        {
            IntArrayFormatter x = new IntArrayFormatter();
            string value = string.Format(x, "{0}", new int[] { 1, 2, 3 });
            Assert.That(value, Is.EqualTo("123"));
        }

        /// <summary>
        /// Scenario: int[] 1,2,3 formatted as a string with a comma delimiter
        /// Expected: "1,2,3"
        /// </summary>
        [Test]
        public void _002_Delimiter()
        {
            IntArrayFormatter x = new IntArrayFormatter(",");
            string value = string.Format(x, "{0}", new int[] { 1, 2, 3 });            
            Assert.That(value, Is.EqualTo("1,2,3"));
        }
    }
}
