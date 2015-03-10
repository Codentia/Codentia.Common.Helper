using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
        /// <summary>
        /// Test CurrencyHelper
        /// </summary>
        [TestFixture]
        public class CurrencyHelperTest
        {
            /// <summary>
            /// Scenario: Currency Helper - direct from AppSettings
            /// Expected: correct values returned
            /// </summary>
            [Test]
            public void _001_CurrencyHelper_FromConfig()
            {
                Assert.That(CurrencyHelper.Symbol, Is.EqualTo("$"));
                Assert.That(CurrencyHelper.SymbolHtml, Is.EqualTo("$"));
                Assert.That(CurrencyHelper.SymbolISO, Is.EqualTo("USD"));
                Assert.That(CurrencyHelper.Culture, Is.EqualTo("en-US"));
            }    

            /// <summary>
            /// Scenario: Currency Helper - Set culture en-GB
            /// Expected: correct values returned
            /// </summary>
            [Test]
            public void _002_CurrencyHelper_SetCulture_enGB()
            {
                CurrencyHelper.Culture = "en-GB";

                Assert.That(CurrencyHelper.Symbol, Is.EqualTo("£"));
                Assert.That(CurrencyHelper.SymbolHtml, Is.EqualTo("&#163;"));
                Assert.That(CurrencyHelper.SymbolISO, Is.EqualTo("GBP"));
                Assert.That(CurrencyHelper.Culture, Is.EqualTo("en-GB"));
            }

            /// <summary>
            /// Scenario: Currency Helper - Set culture fr-FR (euro)
            /// Expected: correct values returned
            /// </summary>
            [Test]
            public void _003_CurrencyHelper_SetCulture_frFR()
            {
                CurrencyHelper.Culture = "fr-FR";

                Assert.That(CurrencyHelper.Symbol, Is.EqualTo("€"));
                Assert.That(CurrencyHelper.SymbolHtml, Is.EqualTo("€"));
                Assert.That(CurrencyHelper.SymbolISO, Is.EqualTo("EUR"));
                Assert.That(CurrencyHelper.Culture, Is.EqualTo("fr-FR"));
            }

            /// <summary>
            /// Scenario: Currency Helper - Set culture en-US (dollar)
            /// Expected: correct values returned
            /// </summary>
            [Test]
            public void _004_CurrencyHelper_SetCulture_enUS()
            {
                CurrencyHelper.Culture = "en-US";

                Assert.That(CurrencyHelper.Symbol, Is.EqualTo("$"));
                Assert.That(CurrencyHelper.SymbolHtml, Is.EqualTo("$"));
                Assert.That(CurrencyHelper.SymbolISO, Is.EqualTo("USD"));
                Assert.That(CurrencyHelper.Culture, Is.EqualTo("en-US"));
            }                   
        }
}
