using System;
using System.Collections.Specialized;
using System.Text;
using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
    /// <summary>
    /// TestFixture for StringHelper
    /// <seealso cref="StringHelper"/>
    /// </summary>
    [TestFixture]
    public class StringHelperTest
    {
        /// <summary>
        /// Ensure all set-up required for testing has been completed
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        /// <summary>
        /// Scenario: Test ConvertStringCollectionToString with no new line separator
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _001_ConvertStringCollectionToString_EmptySeparator()
        {
            StringCollection sc = new StringCollection();
            sc.Add("A");
            sc.Add("B");
            sc.Add("C");
            StringBuilder resultSB = StringHelper.ConvertStringCollectionToStringBuilder(sc, string.Empty);
            Assert.That(resultSB.ToString(), Is.EqualTo("ABC"));
        }

        /// <summary>
        /// Scenario: Test ConvertStringCollectionToString with a new line separator
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _002_ConvertStringCollectionToString_WithNewLineSeparator()
        {
            StringCollection sc = new StringCollection();
            sc.Add("A");
            sc.Add("B");
            sc.Add("C");
            StringBuilder resultSB = StringHelper.ConvertStringCollectionToStringBuilder(sc, System.Environment.NewLine.ToString());            
            Assert.That(resultSB.ToString(), Is.EqualTo("A\r\nB\r\nC\r\n"));
        }

        /// <summary>
        /// Scenario: Run EncodeForSavingToSql
        /// Expected: String replacement works as expected
        /// </summary>
        [Test]
        public void _003_EncodeForSavingToSql()
        {
            string expectedResult = "blah ~~amp; blah ~~gt; ~~lt; ~~quot;";
            string stringtoEncode = "blah & blah > < \"";
            string actualResult = StringHelper.EncodeForSavingToSql(stringtoEncode);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Scenario: Run DecodeRetrievingFromSql
        /// Expected: String replacement works as expected
        /// </summary>
        [Test]
        public void _004_DecodeRetrievingFromSql()
        {
            string expectedResult = "blah & blah > < \"";
            string stringtoDecode = "blah ~~amp; blah ~~gt; ~~lt; ~~quot;";
            string actualResult = StringHelper.DecodeRetrievingFromSql(stringtoDecode);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Scenario: Call method with null parameter
        /// Expected: null
        /// </summary>
        [Test]
        public void _005_GetMD5Hash_Null()
        {
            Assert.That(StringHelper.GetMD5Hash(null), Is.Null);
        }

        /// <summary>
        /// Scenario: Call method with valid string
        /// Expected: Matching MD5 hash
        /// </summary>
        [Test]
        public void _006_GetMD5Hash_Valid()
        {
            Assert.That(StringHelper.GetMD5Hash("ihavean@email.com"), Is.EqualTo("3b3be63a4c2a439b013787725dfce802"));
        }

        /// <summary>
        /// Scenario: Test method with invalid inputs
        /// Expected: Empty String
        /// </summary>
        [Test]
        public void _007_GetShortForm_Invalid()
        {
            Assert.That(StringHelper.GetShortForm(null, 1, 5), Is.EqualTo(string.Empty));
            Assert.That(StringHelper.GetShortForm(string.Empty, 1, 5), Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Scenario: Test method with strings containing a valid set of caps
        /// Expected: Correct acronym
        /// </summary>
        [Test]
        public void _008_GetShortForm_Caps()
        {
            Assert.That(StringHelper.GetShortForm("CafeJAC", 3, 3), Is.EqualTo("CJA"));
            Assert.That(StringHelper.GetShortForm("Mattched IT", 3, 3), Is.EqualTo("MIT"));
        }

        /// <summary>
        /// Scenario: Test method with strings containing words
        /// Expected: Correct letters
        /// </summary>
        [Test]
        public void _009_GetShortForm_Words()
        {
            Assert.That(StringHelper.GetShortForm("my test string", 3, 3), Is.EqualTo("mts"));
        }

        /// <summary>
        /// Scenario: Test method with strings containing neither words nor caps
        /// Expected: Correct letters
        /// </summary>
        [Test]
        public void _010_GetShortForm_Substring()
        {
            Assert.That(StringHelper.GetShortForm("demonstration", 4, 4), Is.EqualTo("demo"));
        }

        /// <summary>
        /// Scenario: Test method with strings a mix of non alphanumeric and alphanumeric chars
        /// Expected: Only alphanumeric chars remain
        /// </summary>
        [Test]
        public void _011_AlphanumericOnly()
        {
            string textText = "!\"t\\h/e 10 q|ui£ck B{r}[o]W%n f$o,&xes j^um*p-=+ed OVER the 23456789 LAZY DoGs?";

            Assert.That(StringHelper.AlphanumericOnly(textText, false, false), Is.EqualTo("the 10 quick BroWn foxes jumped OVER the 23456789 LAZY DoGs"));
            Assert.That(StringHelper.AlphanumericOnly(textText, true, false), Is.EqualTo("the10quickBroWnfoxesjumpedOVERthe23456789LAZYDoGs"));
            Assert.That(StringHelper.AlphanumericOnly(textText, true, true), Is.EqualTo("thequickBroWnfoxesjumpedOVERtheLAZYDoGs"));
            Assert.That(StringHelper.AlphanumericOnly(textText, false, true), Is.EqualTo("the  quick BroWn foxes jumped OVER the  LAZY DoGs"));
        }

        /// <summary>
        /// Scenario: Test method with string containing HTML tags
        /// Expected: Tags are removed
        /// </summary>
        [Test]
        public void _012_StripHtmlTags()
        {
            string result = StringHelper.StripHtmlTags("This <img src='test.jpg' alt='test'/> is a test string");
            Assert.That(result, Is.EqualTo("This is a test string"));
        }

        /// <summary>
        /// Scenario: Check boundaries for password Length
        /// Expected: Exceptions raised
        /// </summary>
        [Test]
        public void _013_GenerateFriendlyPassword_InvalidLength()
        {            
            Assert.That(delegate { StringHelper.GenerateFriendlyPassword(5); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("passwordLength is 5: it must be between 6 and 15 characters"));
            Assert.That(delegate { StringHelper.GenerateFriendlyPassword(16); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("passwordLength is 16: it must be between 6 and 15 characters")); 
        }

        /// <summary>
        /// Scenario: For
        /// Expected: 
        /// </summary>
        [Test]
        public void _014_GenerateFriendlyPassword()
        {
            string previousPW = string.Empty;
            for (int i = 6; i < 16; i++)
            {
                System.Threading.Thread.Sleep(1);
                string pw = StringHelper.GenerateFriendlyPassword(i);                
                Console.WriteLine(pw);
                Assert.That(pw.Length, Is.EqualTo(i));
                Assert.That(pw, Is.Not.EqualTo(previousPW));
                previousPW = pw;
            }        
        }
    }
}
