using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Codentia.Common.Helper
{
    /// <summary>
    /// Utilities for String manipulation
    /// </summary>
    public static class StringHelper
    {
        private const string WordString = "beanouchseatplancallkeysdoornailbookposeroserisepoempoetpastricepainkillsagataletailwashsealstophaltrainhailsnowbluehighrockhellburnpokemesstapeaidecarelovewailthisweepreapfussfretyolknosechinbrowporepoordoorstabslobslabwearscarmarkstayokayfeelpeelwinknoonmoonsoonsenddebtwraptrappumptiretoreritecardlamppanssalthandfootshoebootmapsforkcoldsnowcapenosedeerbirdtreelovehatebestnestkissfoodlisabingtearhearopenhandsingfatedatewashzonepeekgleegearfearbookjokedoorhopemindyourconesamegamejumpveinhurtexitcamecanetamesellkeepkewlbookmoongoshhosefootjunksaidgoneuponquithidetimepagehighdashbitekitehairpoetjivebookfacesandclaysongfilegoldflaysaltkisscarddiscrockneckfiredumbpearfootreallamehairbonefakewinewallclambeeryardcrabchewlovemeatnaillustbeefyardhairflagtentcookwandwashwereballlaceborebarncampcoatchatpraysingraincanetoolroadlanepanepainlinelonewolfbearkissloveboatshipgoatcomecopysaid";
        private const string PunctuationString = "!$?*#";
        private const string NumberString = "8204675823942689356783154272765302528356898490849018403680377387897389789893487981753126728934048367862798729589823637987583789739895378567836758936785163876235423178367476786487643";
        private static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
        
        /// <summary>
        /// Convert StringCollection To StringBuilder
        /// </summary>
        /// <param name="sc">string collection</param>
        /// <param name="separator">separator to be used</param>
        /// <returns>StringBuilder after conversion</returns>
        public static StringBuilder ConvertStringCollectionToStringBuilder(StringCollection sc, string separator)
        {            
            StringBuilder sb = new StringBuilder();

            if (sc != null)
            {
                if (sc.Count > 0)
                {
                    for (int i = 0; i < sc.Count; i++)
                    {
                        sb.Append(sc[i]);
                        if (!string.IsNullOrEmpty(separator))
                        {
                            sb.Append(separator);
                        }
                    }                   
                }
            }

            return sb;
        }

        /// <summary>
        /// Html encode stringToEncode and replace ampersand with ~~
        /// </summary>
        /// <param name="stringToEncode">String To Encode</param>
        /// <returns>string encoded for sql</returns>
        public static string EncodeForSavingToSql(string stringToEncode)
        {
            string a = HttpUtility.HtmlEncode(stringToEncode);
            return a.Replace("&", "~~");
        }

        /// <summary>
        /// replace ~~ with ampersand then Html decode
        /// </summary>
        /// <param name="stringToDecode">String To Decode</param>
        /// <returns>string after being decoded</returns>
        public static string DecodeRetrievingFromSql(string stringToDecode)
        {
            string a = stringToDecode.Replace("~~", "&");            
            return HttpUtility.HtmlDecode(a);          
        }

        /// <summary>
        /// Convert a given string to an MD5 Hash
        /// </summary>
        /// <param name="content">string to be hashed</param>
        /// <returns>string (MD5 Hash)</returns>
        public static string GetMD5Hash(string content)
        {
            if (content != null)
            {
                byte[] sourceBytes = ASCIIEncoding.Default.GetBytes(content);
                byte[] encodedBytes = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(sourceBytes);

                StringBuilder output = new StringBuilder();
                for (int i = 0; i < encodedBytes.Length; i++)
                {
                    output.Append(encodedBytes[i].ToString("x2"));
                }

                return output.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Generate a short form (e.g CookieCode) from a string by:
        /// 1. Using the capital letters (if at least minLength)
        /// 2. Using the initial letters of each word (if enough words)
        /// 3. using the first N letters upto a given maximum
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="minLength">Length of the min.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <returns>string in short form</returns>
        public static string GetShortForm(string source, int minLength, int maxLength)
        {
            StringBuilder shortForm = new StringBuilder();

            if (!string.IsNullOrEmpty(source))
            {
                // uppercase letters
                for (int i = 0; i < source.Length; i++)
                {
                    if (char.IsUpper(source, i))
                    {
                        shortForm.Append(source[i]);
                    }
                }

                // do we have more than 1 letter?
                if (shortForm.Length < minLength)
                {
                    // start again, using words
                    shortForm = new StringBuilder();

                    string[] words = source.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (words.Length >= minLength)
                    {
                        for (int i = 0; i < words.Length; i++)
                        {
                            shortForm.Append(words[i][0]);
                        }
                    }
                    else
                    {
                        // fall back to a simple truncation
                        shortForm.Append(source.Substring(0, minLength));
                    }
                }
            }

            return shortForm.Length <= maxLength ? shortForm.ToString() : shortForm.ToString().Substring(0, maxLength);
        }

        /// <summary>
        /// Strip a string of all non-alphanumeric numeric characters
        /// </summary>
        /// <param name="text">text to strip</param>
        /// <param name="stripSpaces">if true, strip out spaces from the string</param>
        /// <param name="stripNumbers">if true, strip out numbers from the string</param>
        /// <returns>string containing oinly alphanumeric characters</returns>
        public static string AlphanumericOnly(string text, bool stripSpaces, bool stripNumbers)
        {
            List<int> allowableASCIICodes = new List<int>();

            if (!stripSpaces)
            {
                // space
                allowableASCIICodes.Add(32); 
            }

            if (!stripNumbers)
            {
                // 0 to 9
                for (int i = 48; i <= 57; i++) 
                {
                    allowableASCIICodes.Add(i);
                }
            }

            // A to Z
            for (int i = 65; i <= 90; i++) 
            {
                allowableASCIICodes.Add(i);
            }

            // a to z
            for (int i = 97; i <= 122; i++) 
            {
                allowableASCIICodes.Add(i);
            }

            StringBuilder newTextStringBuilder = new StringBuilder();
            foreach (char c in text)
            {
                if (allowableASCIICodes.Contains((int)c))
                {
                    newTextStringBuilder.Append(c);
                }                               
            }
           
            return newTextStringBuilder.ToString();
        }

        /// <summary>
        /// Strips the HTML tags.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>string after html tags stripped</returns>
        public static string StripHtmlTags(string text)
        {
            string result = _htmlRegex.Replace(text, string.Empty);

            // also strip any double spaces
            result = result.Replace("  ", " ");
            return result.Trim();
        }

        /// <summary>
        /// Generate a friendly password based on word list
        /// </summary>
        /// <param name="passwordLength">Length of the password - must be between 6 and 15</param>
        /// <returns>String - random password</returns>
        public static string GenerateFriendlyPassword(int passwordLength)
        {
            if ((passwordLength < 6) || (passwordLength > 15))
            {
                throw new ArgumentException(string.Format("passwordLength is {0}: it must be between 6 and 15 characters", passwordLength));
            }

            Random random = new Random(DateTime.Now.Millisecond);
            Random random1 = new Random(DateTime.Now.AddMilliseconds(random.Next(0, 150000)).Millisecond);
            Random random2 = new Random(DateTime.Now.AddMilliseconds(random1.Next(150001, 300000)).Millisecond);
            Random random3 = new Random(DateTime.Now.AddMilliseconds(random2.Next(300001, 900000)).Millisecond);
            Random random4 = new Random(DateTime.Now.AddMilliseconds(random3.Next(900001, 2000000)).Millisecond);

            bool hasSecondWord = false;
            bool hasThirdWord = false;

            switch (passwordLength)
            {
                case 10:
                case 11:
                case 12:
                case 13:
                    hasSecondWord = true;
                    break;
                case 14:
                case 15:
                    hasSecondWord = true;
                    hasThirdWord = true;
                    break;
            }                

            int firstWordPos = random1.Next(0, 70);
            int secondWordPos = random2.Next(71, 140);
            int thirdWordPos = random3.Next(141, 221);
            int puncCharPos = random4.Next(0, 4);

            string firstWord = WordString.Substring(firstWordPos * 4, 4);
            firstWord = string.Format("{0}{1}", firstWord.Substring(0, 1).ToUpper(), firstWord.Substring(1, 3));

            string secondWord = string.Empty;
            string thirdWord = string.Empty;

            if (hasSecondWord)
            {
                secondWord = WordString.Substring(secondWordPos * 4, 4).ToUpper();
            }

            if (hasThirdWord)
            {
                thirdWord = WordString.Substring(thirdWordPos * 4, 4).ToLower();
            }            

            string puncChar = PunctuationString.Substring(puncCharPos, 1);

            string nonNumericString = string.Format("{0}{1}{2}{3}", firstWord, secondWord, thirdWord, puncChar);

            int numbersToCreate = passwordLength - nonNumericString.Length;

            StringBuilder sbNumber = new StringBuilder(); 
            int count = 0;
            int numberPos = 0;           
            for (int i = 0; i < numbersToCreate; i++)
            {               
                switch (count)
                {
                    case 0:                         
                        numberPos = random4.Next(0, 60);
                        break;
                    case 1:
                        random4 = new Random(DateTime.Now.AddMilliseconds(random2.Next(2000001, 4000000)).Millisecond);
                        numberPos = random4.Next(61, 120);
                        break;
                    case 2:
                        random4 = new Random(DateTime.Now.AddMilliseconds(random3.Next(4000001, 6000000)).Millisecond);
                        numberPos = random4.Next(61, 120);
                        break;
                }                               

                sbNumber.Append(NumberString.Substring(numberPos, 1));
                count++;
            }

            return string.Format("{0}{1}", nonNumericString, sbNumber.ToString());
        }
    }
}
