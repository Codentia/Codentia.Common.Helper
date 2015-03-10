using System.Configuration;
using System.Globalization;
using System.Web;

namespace Codentia.Common.Helper
{
    /// <summary>
    /// Currency Helper
    /// </summary>
    public static class CurrencyHelper
    {
        private static string _symbol = string.Empty;
        private static string _htmlSymbol = string.Empty;
        private static string _isoSymbol = string.Empty;
        private static string _culture = string.Empty;

        /// <summary>
        /// Initializes static members of the CurrencyHelper class.
        /// </summary>
        static CurrencyHelper()
        {
            _culture = "en-GB";
            string config = ConfigurationManager.AppSettings["CurrencyCulture"];

            if (!string.IsNullOrEmpty(config))
            {
                _culture = config;
            }

            SetUp();
        }

        /// <summary>
        /// Gets or sets Culture
        /// </summary>
        public static string Culture
        {
            get 
            { 
                return _culture; 
            }

            set
            {
                if (_culture != value)
                {
                    _culture = value;
                    SetUp();
                }
            }
        }

        /// <summary>
        /// Gets Symbol
        /// </summary>
        public static string Symbol
        {
            get { return _symbol; }
        }

        /// <summary>
        /// Gets HtmlSymbol
        /// </summary>
        public static string SymbolHtml
        {
            get { return _htmlSymbol; }
        }

        /// <summary>
        /// Gets ISOSymbol
        /// </summary>
        public static string SymbolISO
        {
            get { return _isoSymbol; }
        }
       
        private static void SetUp()
        {
            CultureInfo ci = new CultureInfo(_culture);
            RegionInfo ri = new RegionInfo(ci.LCID);

            _symbol = ri.CurrencySymbol;
            _htmlSymbol = HttpUtility.HtmlEncode(ri.CurrencySymbol);
            _isoSymbol = ri.ISOCurrencySymbol;      
        }
    }
}
