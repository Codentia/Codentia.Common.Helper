using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Xml;

namespace Codentia.Common.Helper
{
    /// <summary>
    /// This class exposes methods to assist in the checking of parameter for methods   
    /// </summary>
    public static class ParameterCheckHelper
    {
        /// <summary>
        /// Throw an exception if id is less than 1 (nullable int)
        /// </summary>
        /// <param name="testId">Nullable Id parameter to test</param>
        /// <param name="idType">Type of id (for exception message)</param>        
        public static void CheckIsValidNullableId(int? testId, string idType)
        {
            if (testId != null)
            {
                if (testId < 1)
                {
                    throw new ArgumentException(string.Format("{0}: {1} is not valid", idType, testId));
                }
            }
        }

        /// <summary>
        /// Throw an exception if id is less than -1 or 0: -1 is allowed and is assumed to be null
        /// </summary>
        /// <param name="testId">Nullable Id parameter to test</param>
        /// <param name="idType">Type of id (for exception message)</param>        
        public static void CheckIsValidNullableId(int testId, string idType)
        {
            if (testId <= 0)
            {
                if (testId == -1)
                {
                    return;
                }

                throw new ArgumentException(string.Format("{0}: {1} is not valid", idType, testId));
            }
        }

        /// <summary>
        /// Throw an exception if id is less than 1 (int)
        /// </summary>
        /// <param name="testId">Id parameter to test</param>
        /// <param name="idType">Type of id (for exception message)</param>        
        public static void CheckIsValidId(int testId, string idType)
        {
            CheckIsValidId(testId, idType, false);
        }

        /// <summary>
        /// Throw an exception if id is less than 1 (int)
        /// </summary>
        /// <param name="testId">Id parameter to test</param>
        /// <param name="idType">Type of id (for exception message)</param>
        /// <param name="zeroAllowed">if true zero is allowed</param>
        public static void CheckIsValidId(int testId, string idType, bool zeroAllowed)
        {
            int compareValue = 1;
            if (zeroAllowed)
            {
                compareValue = 0;
            }

            if (testId < compareValue)
            {
                throw new ArgumentException(string.Format("{0}: {1} is not valid", idType, testId));
            }
        }

        /// <summary>
        /// Throw an exception if id is 0
        /// </summary>
        /// <param name="testByte">byte parameter to test</param>
        /// <param name="byteType">Type of byte (for exception message)</param>        
        public static void CheckIsValidByte(byte testByte, string byteType)
        {
            CheckIsValidByte(testByte, byteType, false);
        }

        /// <summary>
        /// Throw an exception if id is 0 if zero is not allowed
        /// </summary>
        /// <param name="testByte">byte parameter to test</param>
        /// <param name="byteType">Type of byte (for exception message)</param>
        /// <param name="zeroAllowed">if true zero is allowed</param>
        public static void CheckIsValidByte(byte testByte, string byteType, bool zeroAllowed)
        {
            if (!zeroAllowed)
            {
                if (testByte == 0)
                {
                    throw new ArgumentException(string.Format("{0}: {1} is not valid", byteType, testByte));
                }
            }
        }

        /// <summary>
        /// Throw an exception if id is 0
        /// </summary>
        /// <param name="testShort">short parameter to test</param>
        /// <param name="shortType">Type of short (for exception message)</param>        
        public static void CheckIsValidShort(short testShort, string shortType)
        {
            CheckIsValidShort(testShort, shortType, false);
        }

        /// <summary>
        /// Throw an exception if id is 0 if zero is not allowed
        /// </summary>
        /// <param name="testShort">short parameter to test</param>
        /// <param name="shortType">Type of short (for exception message)</param>
        /// <param name="zeroAllowed">if true zero is allowed</param>
        public static void CheckIsValidShort(short testShort, string shortType, bool zeroAllowed)
        {
            if (!zeroAllowed)
            {
                if (testShort == 0)
                {
                    throw new ArgumentException(string.Format("{0}: {1} is not valid", shortType, testShort));
                }
            }
        }

        /// <summary>
        /// Throw an exception if testCurrency is less than 0
        /// </summary>
        /// <param name="testCurrency">Currency parameter to test</param>
        /// <param name="idType">Type of id (for exception message)</param>        
        public static void CheckIsValidCurrency(decimal testCurrency, string idType)
        {
            CheckIsValidCurrency(testCurrency, idType, false);
        }

        /// <summary>
        /// Throw an exception if testCurrency is less than 0
        /// </summary>
        /// <param name="testCurrency">Currency parameter to test</param>
        /// <param name="idType">Type of id (for exception message)</param>
        /// <param name="cannotBeZero">If true, zero is not allowed</param>
        public static void CheckIsValidCurrency(decimal testCurrency, string idType, bool cannotBeZero)
        {
            if (cannotBeZero)
            {
                if (testCurrency == 0M)
                {
                    throw new ArgumentException(string.Format("{0} cannot be 0.00", idType));
                }
            }

            if (testCurrency < 0M)
            {
                throw new ArgumentException(string.Format("{0} cannot be less than 0.00", idType));
            }
        }

        /// <summary>
        /// Throw an exception if testString is null or empty string
        /// </summary>
        /// <param name="testString">string parameter to test</param>
        /// <param name="stringName">string name (for exception message)</param> 
        /// <param name="nullAllowed">if true allow nulls, if false do not</param>
        public static void CheckIsValidString(string testString, string stringName, bool nullAllowed)
        {
            if (nullAllowed)
            {
                if (testString == string.Empty)
                {
                    throw new ArgumentException(string.Format("{0} is not specified", stringName));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(testString))
                {
                    throw new ArgumentException(string.Format("{0} is not specified", stringName));
                }
            }
        }

        /// <summary>
        /// Throw an exception if testString is null or empty string with start and end space check
        /// </summary>
        /// <param name="testString">string parameter to test</param>
        /// <param name="stringName">string name (for exception message)</param> 
        /// <param name="nullAllowed">if true allow nulls, if false do not</param>
        /// <param name="spaceAtStartAndEndCheck">if true check there are no spaces at beginning or end</param>
        public static void CheckIsValidString(string testString, string stringName, bool nullAllowed, bool spaceAtStartAndEndCheck)
        {
            CheckIsValidString(testString, stringName, nullAllowed);
            if (spaceAtStartAndEndCheck)
            {
                if (DoesStringHaveSpacesAtStartOrEnd(testString))
                {
                    throw new ArgumentException(string.Format("{0} cannot start and/or end with a space", stringName));
                }
            }
        }

        /// <summary>
        /// Throw an exception if testString is null or empty string or does not meet string length check
        /// </summary>
        /// <param name="testString">string parameter to test</param>
        /// <param name="stringName">string name (for exception message)</param> 
        /// <param name="maxStringLength">Maximum Length allowed for string</param>
        /// <param name="nullAllowed">if true allow nulls, if false do not</param>
        public static void CheckIsValidString(string testString, string stringName, int maxStringLength, bool nullAllowed)
        {
            CheckIsValidString(testString, stringName, nullAllowed);
            CheckStringLengthOnly(testString, stringName, maxStringLength);
        }

        /// <summary>
        /// Throw an exception if testString is null or empty string or does not meet string length check with start and end space check
        /// </summary>
        /// <param name="testString">string parameter to test</param>
        /// <param name="stringName">string name (for exception message)</param> 
        /// <param name="maxStringLength">Maximum Length allowed for string</param>
        /// <param name="nullAllowed">if true allow nulls, if false do not</param>
        /// <param name="spaceAtStartAndEndCheck">if true check there are no spaces at beginning or end</param>
        public static void CheckIsValidString(string testString, string stringName, int maxStringLength, bool nullAllowed, bool spaceAtStartAndEndCheck)
        {
            CheckIsValidString(testString, stringName, nullAllowed);
            CheckStringLengthOnly(testString, stringName, maxStringLength);
            if (spaceAtStartAndEndCheck)
            {
                if (DoesStringHaveSpacesAtStartOrEnd(testString))
                {
                    throw new ArgumentException(string.Format("{0} cannot start and/or end with a space", stringName));
                }
            }
        }

        /// <summary>
        /// Throw an exception if testString does not meet string length check
        /// </summary>
        /// <param name="testString">string parameter to test</param>
        /// <param name="stringName">string name (for exception message)</param> 
        /// <param name="maxStringLength">Maximum Length allowed for string</param>
        public static void CheckStringLengthOnly(string testString, string stringName, int maxStringLength)
        {
            if (!string.IsNullOrEmpty(testString))
            {
                if (testString.Length > maxStringLength)
                {
                    throw new ArgumentException(string.Format("{0} exceeds maxLength {1} as it has {2} chars", stringName, maxStringLength, testString.Length));
                }
            }
        }

        /// <summary>
        /// Throw an exception if testString is not a valid email address
        /// </summary>
        /// <param name="testString">string parameter to test</param>
        /// <param name="maxStringLength">Maximum Length allowed for string</param>
        /// <param name="stringName">string name (for exception message)</param>        
        public static void CheckIsValidEmailAddress(string testString, int maxStringLength, string stringName)
        {
            try
            {
                CheckIsValidString(testString, stringName, maxStringLength, false);
            }
            catch (Exception ex)
            {                
                throw new InvalidEmailAddressException(ex.Message);
            }

            CheckEmailAddressOnly(testString, stringName);
        }

        /// <summary>
        /// Throw an exception if testString is not a valid email address
        /// </summary>
        /// <param name="testString">string parameter to test</param>
        /// <param name="stringName">string name (for exception message)</param>        
        public static void CheckIsValidEmailAddress(string testString, string stringName)
        {
            try
            {
                CheckIsValidString(testString, stringName, false);
            }
            catch (Exception ex)
            {
                throw new InvalidEmailAddressException(ex.Message);
            }
            
            CheckEmailAddressOnly(testString, stringName);
        }

        /// <summary>
        /// Throw an exception if testGuid is not a valid Guid
        /// </summary>
        /// <param name="testGuid">string parameter to test</param>
        /// <param name="stringName">string name (for exception message)</param>        
        public static void CheckIsValidGuid(Guid testGuid, string stringName)
        {
            if (testGuid == Guid.Empty)
            {
                throw new ArgumentException(string.Format("{0} is an empty Guid", stringName));
            }
        }

        /// <summary>
        /// Check xmlString evaluates to a valid Xml Document
        /// </summary>
        /// <param name="xmlString">String representing an Xml Document to test</param>   
        /// <param name="stringName">string name (for exception message)</param>
        /// <param name="attributesToCheck">(optional) string array of attributes to check</param>
        public static void CheckStringIsValidXmlDoc(string xmlString, string stringName, string[] attributesToCheck)
        {
            XmlDocument xmlDoc = XMLHelper.GetXmlDoc(xmlString, stringName);

            if (attributesToCheck != null && attributesToCheck.Length > 0)
            {
                XMLHelper.CheckAttributesInXmlNodeChildren(xmlDoc.FirstChild, stringName, attributesToCheck);
            }
        }

        /// <summary>
        /// Check that a given string contains a valid IP Address (xxx.xxx.xxx.xxx).
        /// </summary>
        /// <param name="address">IP Address to check</param>
        /// <param name="stringName">Field Name (for exception message)</param>
        public static void CheckStringIsValidIPAddress(string address, string stringName)
        {
            CheckIsValidString(address, "ipAddress", 15, false);
            if (!string.IsNullOrEmpty(address))
            {
                Regex match = new Regex(@"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}");

                if (address == "::1" || match.IsMatch(address))
                {
                    return;
                }
            }

            throw new ArgumentException(string.Format("{0}: {1} is invalid", stringName, address));
        }

        /// <summary>
        /// Check if a given numeric type is within the specified range.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="minValue">Minimum acceptable value</param>
        /// <param name="maxValue">Maximum acceptable value</param>
        /// <param name="valueName">Name of value parameter</param>
        public static void CheckNumberIsInRange(decimal value, decimal minValue, decimal maxValue, string valueName)
        {
            if (value < minValue || value > maxValue)
            {
                throw new ArgumentException(string.Format("{0}: must be between {1} and {2}", valueName, minValue, maxValue));
            }
        }

        /// <summary>
        /// Check if a given string represents a valid 11 digit phone number
        /// </summary>
        /// <param name="phoneNumber">String value to check</param>
        /// <param name="parameterName">Parameter name</param>
        public static void CheckStringIsValidPhoneNumber(string phoneNumber, string parameterName)
        {
            CheckIsValidString(phoneNumber, parameterName, false);
            CheckPhoneNumber(phoneNumber, parameterName);
        }

        /// <summary>
        /// Check if a given string represents a valid 11 digit phone number
        /// </summary>
        /// <param name="phoneNumber">String value to check</param>
        /// <param name="maxStringLength">Maximum Length allowed for string</param>
        /// <param name="parameterName">Parameter name</param>
        public static void CheckStringIsValidPhoneNumber(string phoneNumber, int maxStringLength, string parameterName)
        {
            CheckIsValidString(phoneNumber, parameterName, maxStringLength, false);
            CheckPhoneNumber(phoneNumber, parameterName);
        }

        /// <summary>
        /// Check if a Dictionary object has an exact Count, also checking it is not empty
        /// </summary>
        /// <param name="countToMatch">int to match</param>
        /// <param name="coll">the ICollection to match</param>
        /// <param name="parameterName">Parameter name</param>
        public static void CheckICollectionCount(int countToMatch, ICollection coll, string parameterName)
        {
            CheckIsValidId(countToMatch, "countToMatch");
            CheckIsValidString(parameterName, "parameterName", false);
            CheckICollectionIsNotNullOrEmpty(coll, parameterName);

            if (coll.Count != countToMatch)
            {
                throw new ArgumentException(string.Format("{0} count {1} does not match countToMatch {2}", parameterName, coll.Count, countToMatch));
            }
        }

        /// <summary>
        /// Check if a Dictionary object has is null or empty
        /// </summary>
        /// <param name="coll">the ICollection to check</param>
        /// <param name="parameterName">Parameter name</param>
        public static void CheckICollectionIsNotNullOrEmpty(ICollection coll, string parameterName)
        {
            if (coll == null)
            {
                throw new ArgumentException(string.Format("{0} cannot be null", parameterName));
            }

            if (coll.Count == 0)
            {
                throw new ArgumentException(string.Format("{0} cannot be empty", parameterName));
            }
        }

        /// <summary>
        /// Check a value is in the range for an specified enum
        /// </summary>
        /// <param name="en">Enum to use</param>
        /// <param name="value">Value to check</param>
        /// <param name="parameterName">parameter name</param>
        public static void CheckIsValidEnumValue(Type en, int value, string parameterName)
        {
            if (!en.IsEnum)
            {
                throw new ArgumentException("Type parameter is not an enum");
            }

            Array values = Enum.GetValues(en);

            for (int i = 0; i < values.Length; i++)
            {
                if ((int)values.GetValue(i) == value)
                {
                    return;
                }
            }

            throw new ArgumentException(string.Format("value ({0}) of {1} is not in range for Enum", value, parameterName));
        }

        /// <summary>
        /// Checks the parameter is not null
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        public static void CheckIsNotNullParameter(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentException(string.Format("{0} cannot be null", parameterName));
            }
        }

        /// <summary>
        /// Check a value is a valid byte array
        /// </summary>
        /// <param name="testArray">array to be tested</param>
        /// <param name="testArrayType">name of array to be tested</param>
        /// <param name="nullAllowed">if set to <c>true</c> nulls are allowed</param>
        public static void CheckIsValidByteArray(byte[] testArray, string testArrayType, bool nullAllowed)
        {
            if (nullAllowed)
            {
                if (testArray == null)
                {
                    return;
                }
            }

            if (testArray == null)
            {
                throw new ArgumentException(string.Format("{0}: is not specified", testArrayType));
            }

            if (testArray.Length == 0)
            {
                throw new ArgumentException(string.Format("{0}: is empty", testArrayType));
            }
        }

        private static bool DoesStringHaveSpacesAtStartOrEnd(string testString)
        {
            if (testString.StartsWith(" "))
            {
                return true;
            }

            if (testString.EndsWith(" "))
            {
                return true;
            }

            return false;
        }

        private static void CheckEmailAddressOnly(string testString, string stringName)
        {
            // if (!Regex.IsMatch(testString, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            if (!Regex.IsMatch(testString, @"^([\w-\.]+\+*[\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([\w]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                throw new InvalidEmailAddressException(string.Format("{0}: {1} is not a valid email address", stringName, testString));
            }
        }

        private static void CheckPhoneNumber(string phoneNumber, string parameterName)
        {
            if (Regex.IsMatch(phoneNumber, "^[+]{0,1}[0-9]{11,12}$"))
            {
                return;
            }

            throw new ArgumentException(string.Format("{0}: {1} is invalid", parameterName, phoneNumber));
        }
    }
}
