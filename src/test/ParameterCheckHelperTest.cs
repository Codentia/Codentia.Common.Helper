using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
    /// <summary>
    /// TestFixture for ParameterCheckHelper
    /// <seealso cref="ParameterCheckHelper"/>
    /// </summary>
    [TestFixture]
    public class ParameterCheckHelperTest
    {
        /// <summary>
        /// Test Enum 1
        /// </summary>
        internal enum TestEnum1
        {
            /// <summary>
            /// Option 0
            /// </summary>
            Option0 = 0,

            /// <summary>
            /// Option 1
            /// </summary>
            Option1 = 1,

            /// <summary>
            /// Option 2
            /// </summary>
            Option2 = 2
        }

        /// <summary>
        /// Ensure all set-up required for testing has been completed
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        /// <summary>
        /// Scenario: Test CheckIsValidNullableId with a null
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _001_CheckIsValidNullableId_NullId()
        {
            ParameterCheckHelper.CheckIsValidNullableId(null, "testTypeId");
            ParameterCheckHelper.CheckIsValidNullableId(1, "testTypeId");

            int? i = 1;
            ParameterCheckHelper.CheckIsValidNullableId(i, "testTypeId");
        }

        /// <summary>
        /// Scenario: Test CheckIsValidNullableId with an invalid id
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _002_CheckIsValidNullableId_InvalidId()
        {
            int? i = -1;
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidNullableId(i, "testTypeId"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testTypeId: -1 is not valid"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidNullableId with a valid id
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _003_CheckIsValidNullableId_ValidId()
        {
            ParameterCheckHelper.CheckIsValidNullableId(10, "testTypeId");
        }

        /// <summary>
        /// Scenario: Test CheckIsValidId with -1
        /// Expected: Invalid Exception should be raised
        /// </summary>
        [Test]
        public void _004_CheckIsValidId_InvalidId()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidId(-1, "testTypeId"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testTypeId: -1 is not valid"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidId(-1, "testTypeId", true); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testTypeId: -1 is not valid"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidId with 1000
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _005_CheckIsValidId_ValidId()
        {
            ParameterCheckHelper.CheckIsValidId(1000, "testTypeId");
            ParameterCheckHelper.CheckIsValidId(0, "testTypeId", true);
        }

        /// <summary>
        /// Scenario: Test CheckIsValidCurrency with -10M
        /// Expected: Invalid Currency Exception should be raised
        /// </summary>
        [Test]
        public void _006_CheckIsValidCurrency_InvalidCurrency()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidCurrency(-10M, "testCurrency"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testCurrency cannot be less than 0.00"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidCurrency(0M, "testCurrency", true); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testCurrency cannot be 0.00"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidCurrency with 10M
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _007_CheckIsValidCurrency_ValidCurrency()
        {
            ParameterCheckHelper.CheckIsValidCurrency(10M, "testCurrency");
            ParameterCheckHelper.CheckIsValidCurrency(0M, "testCurrency", false);
            ParameterCheckHelper.CheckIsValidCurrency(10M, "testCurrency", true);
        }

        /// <summary>
        /// Scenario: Test CheckIsValidString with a null
        /// Expected: A not specified Exception should be raised
        /// </summary>
        [Test]
        public void _008_CheckIsValidString_NullString()
        {
            ParameterCheckHelper.CheckIsValidString(null, "testString", true);
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidString(null, "testString", false); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testString is not specified"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidString with an empty string
        /// Expected: A not specified Exception should be raised
        /// </summary>
        [Test]
        public void _009_CheckIsValidString_EmptyString()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidString(string.Empty, "testString", true); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testString is not specified"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidString(string.Empty, "testString", false); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testString is not specified"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidString with an empty string
        /// Expected: A not specified Exception should be raised
        /// </summary>
        [Test]
        public void _010_CheckIsValidString_ValidString()
        {
            ParameterCheckHelper.CheckIsValidString("This is OK", "testString", true);
            ParameterCheckHelper.CheckIsValidString("This is OK", "testString", false);
        }

        /// <summary>
        /// Scenario: Test CheckIsValidEmailAddress with null
        /// Expected: A not specified Exception should be raised
        /// </summary>
        [Test]
        public void _011_CheckIsValidEmailAddress_NullString()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEmailAddress(null, "testEmail"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testEmail is not specified"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEmailAddress(null, 100, "testEmail"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testEmail is not specified"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidEmailAddress with an empty string
        /// Expected: A not specified Exception should be raised
        /// </summary>
        [Test]
        public void _012_CheckIsValidEmailAddress_EmptyString()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEmailAddress(string.Empty, "testEmail"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testEmail is not specified"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEmailAddress(string.Empty, 100, "testEmail"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testEmail is not specified"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidEmailAddress with an empty string
        /// Expected: A not specified Exception should be raised
        /// </summary>
        [Test]
        public void _013_CheckIsValidEmailAddress_InvalidStrings()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEmailAddress("me", "testEmail"); }, Throws.InstanceOf<InvalidEmailAddressException>().With.Message.EqualTo("testEmail: me is not a valid email address"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEmailAddress("me@", "testEmail"); }, Throws.InstanceOf<InvalidEmailAddressException>().With.Message.EqualTo("testEmail: me@ is not a valid email address"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEmailAddress("me", 100, "testEmail"); }, Throws.InstanceOf<InvalidEmailAddressException>().With.Message.EqualTo("testEmail: me is not a valid email address"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEmailAddress("me@", "testEmail"); }, Throws.InstanceOf<InvalidEmailAddressException>().With.Message.EqualTo("testEmail: me@ is not a valid email address"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEmailAddress("me@", 2, "testEmail"); }, Throws.InstanceOf<InvalidEmailAddressException>().With.Message.EqualTo("testEmail exceeds maxLength 2 as it has 3 chars"));

            // cannot have a plus on it's own, even with plus addressing
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEmailAddress("notok.com", "testEmail"); }, Throws.InstanceOf<InvalidEmailAddressException>().With.Message.EqualTo("testEmail: notok.com is not a valid email address"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidEmailAddress with an valid email address
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _014_CheckIsValidEmailAddress_ValidEmailAddress()
        {
            ParameterCheckHelper.CheckIsValidEmailAddress("blah@OK.com", "testEmail");
            ParameterCheckHelper.CheckIsValidEmailAddress("blah@OK.com", 20, "testEmail");
            ParameterCheckHelper.CheckIsValidEmailAddress("events@foe-scotland.org.uk", 30, "testEmail");
            ParameterCheckHelper.CheckIsValidEmailAddress("Beautiful-Beads@gmx.net", 30, "testEmail");
            ParameterCheckHelper.CheckIsValidEmailAddress("katie.drury@ap-personnel.com", 30, "testEmail");

            // check "plus" addressing works ok
            ParameterCheckHelper.CheckIsValidEmailAddress("foo+bar@OK.com", "testEmail");
        }

        /// <summary>
        /// Scenario: Test CheckIsValidGuid with an empty Guid
        /// Expected: Empty Guid Exception raised
        /// </summary>
        [Test]
        public void _015_CheckIsValidGuid_EmptyGuid()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidGuid(Guid.Empty, "testGuid"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testGuid is an empty Guid"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidGuid with an valid Guid
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _016_CheckIsValidGuid_ValidGuid()
        {
            ParameterCheckHelper.CheckIsValidGuid(Guid.NewGuid(), "testGuid");
        }

        /// <summary>
        /// Scenario: Test CheckStringIsValidXmlDoc with null and empty strings
        /// Expected: xmlString is not specified Exception raised
        /// </summary>
        [Test]
        public void _017_CheckStringIsValidXmlDoc_NullorEmptyString()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidXmlDoc(null, "testXmlString", null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testXmlString is not specified"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidXmlDoc(string.Empty, "testXmlString", null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testXmlString is not specified"));
        }

        /// <summary>
        /// Scenario: Test CheckStringIsValidXmlDoc with null and empty strings
        /// Expected: xmlString is not valid Exception raised
        /// </summary>
        [Test]
        public void _018_CheckStringIsValidXmlDoc_InvalidXml()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidXmlDoc("<INVALID", "testXmlString", null); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testXmlString is not valid"));
        }

        /// <summary>
        /// Scenario: Test CheckStringIsValidXmlDoc with null and empty strings
        /// Expected: no Exception raised
        /// </summary>
        [Test]
        public void _019_CheckStringIsValidXmlDoc_ValidXml_NullAttributesToCheck()
        {
            ParameterCheckHelper.CheckStringIsValidXmlDoc("<VALIDROOT><VALIDELEM validAttrib=\"Y\"></VALIDELEM></VALIDROOT>", "testXmlString", null);
        }

        /// <summary>
        /// Scenario: Test CheckStringIsValidXmlDoc with valid xml but invalid attribute array
        /// Expected: attribute Exception raised
        /// </summary>
        [Test]
        public void _020_CheckStringIsValidXmlDoc_ValidXml_InvalidAttributesToCheck()
        {
            string validXml = "<VALIDROOT><VALIDELEM validAttrib1=\"Y\" validAttrib2=\"N\"></VALIDELEM></VALIDROOT>";
            string[] invalidArray = { string.Empty, "TEST" };
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidXmlDoc(validXml, "testXmlString", invalidArray); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("attribute 0 is not specified"));

            string[] invalidArray2 = { "TEST", null };
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidXmlDoc(validXml, "testXmlString", invalidArray2); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("attribute 1 is not specified"));
        }

        /// <summary>
        /// Scenario: Test CheckStringIsValidXmlDoc with valid xml that has no child nodes
        /// Expected: node does not have any child nodes Exception raised
        /// </summary>
        [Test]
        public void _021_CheckStringIsValidXmlDoc_ValidXml_NoChildNodes()
        {
            string validXml = "<VALIDROOT></VALIDROOT>";
            string[] invalidArray = { string.Empty, "TEST" };
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidXmlDoc(validXml, "testXmlString", invalidArray); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("node does not have any child nodes"));
        }

        /// <summary>
        /// Scenario: Test CheckStringIsValidXmlDoc with valid xml and attribute array
        /// Expected: no Exception raised
        /// </summary>
        [Test]
        public void _022_CheckStringIsValidXmlDoc_ValidXml_ValidAttributesToCheck_MissingAttributes()
        {
            string validXml = "<VALIDROOT><VALIDELEM validAttrib1=\"Y\" ></VALIDELEM></VALIDROOT>";
            string[] validArray = { "validAttrib1", "validAttrib2" };
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidXmlDoc(validXml, "testXmlString", validArray); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("Required attribute(s) are missing from node"));
        }

        /// <summary>
        /// Scenario: Test CheckStringIsValidXmlDoc with valid xml and attribute array
        /// Expected: no Exception raised
        /// </summary>
        [Test]
        public void _023_CheckStringIsValidXmlDoc_ValidXml_ValidAttributesToCheck()
        {
            string validXml = "<VALIDROOT><VALIDELEM validAttrib1=\"Y\" validAttrib2=\"N\"></VALIDELEM></VALIDROOT>";
            string[] validArray = { "validAttrib1", "validAttrib2" };
            ParameterCheckHelper.CheckStringIsValidXmlDoc(validXml, "testXmlString", validArray);
        }

        /// <summary>
        /// Scenario: Valid IP Addresses tested
        /// Expected: No exception raised
        /// </summary>
        [Test]
        public void _024_CheckStringIsValidIPAddress_Valid()
        {
            ParameterCheckHelper.CheckStringIsValidIPAddress("127.0.0.1", "ipAddress");
            ParameterCheckHelper.CheckStringIsValidIPAddress("192.168.0.1", "ipAddress");
            ParameterCheckHelper.CheckStringIsValidIPAddress("92.76.85.102", "ipAddress");
        }

        /// <summary>
        /// Scenario: Invalid IP Addresses tested
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _025_CheckStringIsValidIPAddress_NotValid()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidIPAddress(null, "ipAddress"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("ipAddress is not specified"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidIPAddress(string.Empty, "ipAddress"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("ipAddress is not specified"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidIPAddress("wibble", "ipAddress"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("ipAddress: wibble is invalid"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidIPAddress("this.is.no.ip", "ipAddress"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("ipAddress: this.is.no.ip is invalid"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidIPAddress("this.is.no.ip.extrachars", "ipAddress"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("ipAddress exceeds maxLength 15 as it has 24 chars"));
        }

        /// <summary>
        /// Scenario: Call method with a valid, in range number
        /// Expected: Executes without error
        /// </summary>
        [Test]
        public void _026_CheckNumberIsInRange_InRange()
        {
            ParameterCheckHelper.CheckNumberIsInRange(10.0m, 1.0m, 15.0m, "value");
        }

        /// <summary>
        /// Scenario: Call method with a value which is below the minimum
        /// Expected: Exception ([valueName]: must be between [minimum] and [maximum]
        /// </summary>
        [Test]
        public void _027_CheckNumberIsInRange_BelowMinimum()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckNumberIsInRange(-1.0m, 1.0m, 15.0m, "value"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("value: must be between 1.0 and 15.0"));
        }

        /// <summary>
        /// Scenario: Call method with a value which is above the maximum
        /// Expected: Exception ([valueName]: must be between [minimum] and [maximum]
        /// </summary>
        [Test]
        public void _028_CheckNumberIsInRange_AboveMaximum()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckNumberIsInRange(20.0m, 1.0m, 15.0m, "value"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("value: must be between 1.0 and 15.0"));
        }

        /// <summary>
        /// Scenario: Method called with null, empty or invalid argument
        /// Expected: Exception
        /// </summary>
        [Test]
        public void _029_CheckStringIsValidPhoneNumber_Invalid()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidPhoneNumber(null, "phoneNumber"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("phoneNumber is not specified"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidPhoneNumber(null, 11, "phoneNumber"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("phoneNumber is not specified"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidPhoneNumber(string.Empty, "phoneNumber"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("phoneNumber is not specified"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidPhoneNumber(string.Empty, 11, "phoneNumber"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("phoneNumber is not specified"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidPhoneNumber("not a number", "phoneNumber"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("phoneNumber: not a number is invalid"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidPhoneNumber("not a no.", 11, "phoneNumber"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("phoneNumber: not a no. is invalid"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidPhoneNumber("123", "phoneNumber"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("phoneNumber: 123 is invalid"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidPhoneNumber("123", 11, "phoneNumber"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("phoneNumber: 123 is invalid"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidPhoneNumber("12345678901234567890", "phoneNumber"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("phoneNumber: 12345678901234567890 is invalid"));
            Assert.That(delegate { ParameterCheckHelper.CheckStringIsValidPhoneNumber("12345678901234567890", 11, "phoneNumber"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("phoneNumber exceeds maxLength 11 as it has 20 chars"));
        }

        /// <summary>
        /// Scenario: Method called with valid phone number
        /// Expected: No exception raised
        /// </summary>
        [Test]
        public void _030_CheckStringIsValidPhoneNumber_Valid()
        {
            ParameterCheckHelper.CheckStringIsValidPhoneNumber("01534738655", "phoneNumber");
            ParameterCheckHelper.CheckStringIsValidPhoneNumber("02392811526", "phoneNumber");
            ParameterCheckHelper.CheckStringIsValidPhoneNumber("01534738655", 11, "phoneNumber");
            ParameterCheckHelper.CheckStringIsValidPhoneNumber("02392811526", 11, "phoneNumber");
        }

        /// <summary>
        /// Scenario: Test CheckIsValidString with string that exceeds maxStringLength
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _031_CheckIsValidString_ExceedsMaxLength()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidString("123456", "testString", 5, true); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testString exceeds maxLength 5 as it has 6 chars"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidString with string that exceeds maxStringLength
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _032_CheckIsValidString_MaxLengthNotExceeded()
        {
            ParameterCheckHelper.CheckIsValidString("123456", "testString", 6, true);
        }

        /// <summary>
        /// Scenario: Test CheckICollectionIsNotNullOrEmpty with a null parameter
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _033_CheckICollectionIsNotNullOrEmpty_Null()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckICollectionIsNotNullOrEmpty(null, "myparam"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("myparam cannot be null"));
        }

        /// <summary>
        /// Scenario: Test CheckICollectionIsNotNullOrEmpty with an empty dictionary
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _034_CheckICollectionIsNotNullOrEmpty_Empty()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            Assert.That(delegate { ParameterCheckHelper.CheckICollectionIsNotNullOrEmpty(dict, "myparam"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("myparam cannot be empty"));
        }

        /// <summary>
        /// Scenario: Test CheckICollectionIsNotNullOrEmpty with valid param
        /// Expected: Runs successfully
        /// </summary>
        [Test]
        public void _035_CheckICollectionIsNotNullOrEmpty_ValidParam()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "test");
            ParameterCheckHelper.CheckICollectionIsNotNullOrEmpty(dict, "myparam");
        }

        /// <summary>
        /// Scenario: Test CheckICollectionCount with an invalid countToMatch
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _036_CheckICollectionCount_InvalidCountToMatch()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "test");
            Assert.That(delegate { ParameterCheckHelper.CheckICollectionCount(0, dict, "myparam"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("countToMatch: 0 is not valid"));
            Assert.That(delegate { ParameterCheckHelper.CheckICollectionCount(-1, dict, "myparam"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("countToMatch: -1 is not valid"));
        }

        /// <summary>
        /// Scenario: Test CheckICollectionCount with an null or empty dictionary
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _037_CheckICollectionCount_NullAndEmptyDictionary()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            Assert.That(delegate { ParameterCheckHelper.CheckICollectionCount(2, null, "myparam"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("myparam cannot be null"));
            Assert.That(delegate { ParameterCheckHelper.CheckICollectionCount(2, dict, "myparam"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("myparam cannot be empty"));
        }

        /// <summary>
        /// Scenario: Test CheckICollectionCount with a dictionary count that does not match
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _038_CheckICollectionCount_CountNotTheSame()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "test");
            Assert.That(delegate { ParameterCheckHelper.CheckICollectionCount(2, dict, "myparam"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("myparam count 1 does not match countToMatch 2"));
        }

        /// <summary>
        /// Scenario: Test CheckICollectionCount with a dictionary count that matches countToMatch
        /// Expected: Runs successfully
        /// </summary>
        [Test]
        public void _039_CheckICollectionCount_CountTheSame()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "test1");
            dict.Add(2, "test2");
            ParameterCheckHelper.CheckICollectionCount(2, dict, "myparam");
        }

        /// <summary>
        /// Scenario: Run CheckIsValidEnumValue with an type that isnt an enum
        /// Expected: Raises execption
        /// </summary>
        [Test]
        public void _040_CheckIsValidEnumValue_TypeNotAnEnum()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEnumValue(typeof(DateTime), 0, "myparam"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("Type parameter is not an enum"));
        }

        /// <summary>
        /// Scenario: Run CheckIsValidEnumValue with an invalid value
        /// Expected: Raises execption
        /// </summary>
        [Test]
        public void _041_CheckIsValidEnumValue_InvalidValue()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidEnumValue(typeof(TestEnum1), -1, "myparam"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("value (-1) of myparam is not in range for Enum"));
        }

        /// <summary>
        /// Scenario: Run CheckIsValidEnumValue with an invalid value
        /// Expected: Raises execption
        /// </summary>
        [Test]
        public void _042_CheckIsValidEnumValue_ValidValue()
        {
            ParameterCheckHelper.CheckIsValidEnumValue(typeof(TestEnum1), (int)TestEnum1.Option0, "myparam");
            ParameterCheckHelper.CheckIsValidEnumValue(typeof(TestEnum1), (int)TestEnum1.Option1, "myparam");
            ParameterCheckHelper.CheckIsValidEnumValue(typeof(TestEnum1), (int)TestEnum1.Option2, "myparam");
        }

        /// <summary>
        /// Scenario: Test CheckIsValidNullableId with a -1: which is assumed to be null and therefore allowed
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _043_CheckIsValidNullableId_Minus1()
        {
            ParameterCheckHelper.CheckIsValidNullableId(-1, "testTypeId");
        }

        /// <summary>
        /// Scenario: Test CheckIsValidNullableId with an invalid id
        /// Expected: Exception
        /// </summary>
        [Test]
        public void _044_CheckIsValidNullableId_InvalidId()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidNullableId(0, "testTypeId"); }, Throws.Exception.With.Message.EqualTo("testTypeId: 0 is not valid"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidNullableId(-100, "testTypeId"); }, Throws.Exception.With.Message.EqualTo("testTypeId: -100 is not valid"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidNullableId with a valid id
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _045_CheckIsValidNullableId_ValidId()
        {
            ParameterCheckHelper.CheckIsValidNullableId(10, "testTypeId");
        }

        /// <summary>
        /// Scenario: Test CheckIsValidString with string that starts or ends with a space
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _046_CheckIsValidString_SpaceCheck_True_WithSpaces()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidString(" 123456", "testString", true, true); }, Throws.Exception.With.Message.EqualTo("testString cannot start and/or end with a space"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidString("123456 ", "testString", true, true); }, Throws.Exception.With.Message.EqualTo("testString cannot start and/or end with a space"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidString(" 123456 ", "testString", true, true); }, Throws.Exception.With.Message.EqualTo("testString cannot start and/or end with a space"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidString(" 123456", "testString", 10, true, true); }, Throws.Exception.With.Message.EqualTo("testString cannot start and/or end with a space"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidString("123456 ", "testString", 10, true, true); }, Throws.Exception.With.Message.EqualTo("testString cannot start and/or end with a space"));
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidString(" 123456 ", "testString", 10, true, true); }, Throws.Exception.With.Message.EqualTo("testString cannot start and/or end with a space"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidString with string that starts or ends with a space
        /// Expected: Exception raised
        /// </summary>
        [Test]
        public void _047_CheckIsValidString_SpaceCheck_True_WithoutSpaces()
        {
            ParameterCheckHelper.CheckIsValidString("123456", "testString", true, true);
            ParameterCheckHelper.CheckIsValidString("123456", "testString", 10, true, true);
        }

        /// <summary>
        /// Scenario: Test CheckIsValidString with string that starts or ends with a space but no check
        /// Expected: Runs successfully
        /// </summary>
        [Test]
        public void _048_CheckIsValidString_SpaceCheck_False()
        {
            // no length check
            ParameterCheckHelper.CheckIsValidString(" 123456", "testString", true, false);
            ParameterCheckHelper.CheckIsValidString("123456 ", "testString", true, false);
            ParameterCheckHelper.CheckIsValidString(" 123456 ", "testString", true, false);

            // length check
            ParameterCheckHelper.CheckIsValidString(" 123456", "testString", 10, true, false);
            ParameterCheckHelper.CheckIsValidString("123456", "testString", 10, true, false);
            ParameterCheckHelper.CheckIsValidString(" 123456 ", "testString", 10, true, false);
        }

        /// <summary>
        /// Scenario: Test CheckIsValidByteArray with a null param and nulls allowed
        /// Expected: Runs successfully
        /// </summary>
        [Test]
        public void _049_CheckIsValidByteArray_Null_NullsAllowed()
        {
            ParameterCheckHelper.CheckIsValidByteArray(null, "testArray", true);
        }

        /// <summary>
        /// Scenario: Test CheckIsValidByteArray with a null param and nulls not allowed
        /// Expected: Exception
        /// </summary>
        [Test]
        public void _050_CheckIsValidByteArray_Null_NullsNotAllowed()
        {
            Assert.That(delegate { ParameterCheckHelper.CheckIsValidByteArray(null, "testArray", false); }, Throws.Exception.With.Message.EqualTo("testArray: is not specified"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidByteArray with an empty bit array
        /// Expected: Exception
        /// </summary>
        [Test]
        public void _051_CheckIsValidByteArray_EmptyBitArray()
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] testArray = encoding.GetBytes(string.Empty);

            Assert.That(delegate { ParameterCheckHelper.CheckIsValidByteArray(testArray, "testArray", false); }, Throws.Exception.With.Message.EqualTo("testArray: is empty"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidByteArray with a valid byte array
        /// Expected: Runs successfully
        /// </summary>
        [Test]
        public void _052_CheckIsValidByteArray_ValidBitArray()
        {
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] testArray = encoding.GetBytes("blah");

            ParameterCheckHelper.CheckIsValidByteArray(testArray, "testArray", false);
            ParameterCheckHelper.CheckIsValidByteArray(testArray, "testArray", true);
        }

        /// <summary>
        /// Scenario: Test IPv6 Local/loop back address
        /// Expected: No exception raised
        /// </summary>
        [Test]
        public void _053_CheckStringIsValidIPAddress_IPv6Local()
        {
            ParameterCheckHelper.CheckStringIsValidIPAddress("::1", "ipAddress");
        }

        /// <summary>
        /// Scenario: Test CheckIsValidByte with -1
        /// Expected: Invalid Exception should be raised
        /// </summary>
        [Test]
        public void _054_CheckIsValidByte_InvalidByte()
        {
            byte testByte = Convert.ToByte(0);

            Assert.That(delegate { ParameterCheckHelper.CheckIsValidByte(testByte, "testTypeId"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testTypeId: 0 is not valid"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidByte with 1000
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _055_CheckIsValidId_ValidByte()
        {
            byte testByte = Convert.ToByte(255);
            ParameterCheckHelper.CheckIsValidByte(testByte, "testTypeId");
            ParameterCheckHelper.CheckIsValidByte(0, "testTypeId", true);
        }

        /// <summary>
        /// Scenario: Test CheckIsValidShort with -1
        /// Expected: Invalid Exception should be raised
        /// </summary>
        [Test]
        public void _056_CheckIsValidShort_InvalidShort()
        {
            short testShort = Convert.ToInt16(0);

            Assert.That(delegate { ParameterCheckHelper.CheckIsValidShort(testShort, "testTypeId"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testTypeId: 0 is not valid"));
        }

        /// <summary>
        /// Scenario: Test CheckIsValidShort with 1000
        /// Expected: No Exception should be raised
        /// </summary>
        [Test]
        public void _057_CheckIsValidId_ValidShort()
        {
            short testShort = Convert.ToInt16(255);
            ParameterCheckHelper.CheckIsValidShort(testShort, "testTypeId");
            ParameterCheckHelper.CheckIsValidShort(0, "testTypeId", true);
        }

        /// <summary>
        /// Scenario: Test CheckIsNotNullParameter with null and not null parameters
        /// Expected: ArgumentNullException should be raised for null
        /// </summary>
        [Test]
        public void _058_CheckIsNotNullParameter_Null()
        {
            // Test that null raises and exception
            Assert.That(delegate { ParameterCheckHelper.CheckIsNotNullParameter(null, "testobject"); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("testobject cannot be null"));

            // for completeness test a not null param
            ParameterCheckHelper.CheckIsNotNullParameter("not null", "testobject");
        }
    }
}
