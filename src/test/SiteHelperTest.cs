using System;
using System.Collections.Generic;
using System.Text;
using Codentia.Common.Helper;
using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
    /// <summary>
    /// Unit testing framework for SiteHelper
    /// </summary>
    public class SiteHelperTest
    {
        /// <summary>
        /// Scenario: Property evaluated for all valid values
        /// Expected: Correct return
        /// </summary>
        [Test]
        public void _001_SiteEnvironment()
        {
            SiteHelper.BaseEnvironment = null;
            Assert.That(SiteHelper.SiteEnvironment, Is.EqualTo("DEV"));

            SiteHelper.BaseEnvironment = "DEV";
            Assert.That(SiteHelper.SiteEnvironment, Is.EqualTo("DEV"));

            SiteHelper.BaseEnvironment = "TEST";
            Assert.That(SiteHelper.SiteEnvironment, Is.EqualTo("TEST"));

            SiteHelper.BaseEnvironment = "UAT";
            Assert.That(SiteHelper.SiteEnvironment, Is.EqualTo("UAT"));

            SiteHelper.BaseEnvironment = "LIVE";
            Assert.That(SiteHelper.SiteEnvironment, Is.EqualTo("LIVE"));

            SiteHelper.BaseEnvironment = "DEMO";
            Assert.That(SiteHelper.SiteEnvironment, Is.EqualTo("LIVE"));
        }

        /// <summary>
        /// Scenario: Property evaluated for all valid values
        /// Expected: Correct return
        /// </summary>
        [Test]
        public void _002_PaymentEnvironment()
        {
            SiteHelper.BaseEnvironment = null;
            Assert.That(SiteHelper.PaymentEnvironment, Is.EqualTo("DEV"));

            SiteHelper.BaseEnvironment = "DEV";
            Assert.That(SiteHelper.PaymentEnvironment, Is.EqualTo("DEV"));

            SiteHelper.BaseEnvironment = "TEST";
            Assert.That(SiteHelper.PaymentEnvironment, Is.EqualTo("TEST"));

            SiteHelper.BaseEnvironment = "UAT";
            Assert.That(SiteHelper.PaymentEnvironment, Is.EqualTo("UAT"));

            SiteHelper.BaseEnvironment = "LIVE";
            Assert.That(SiteHelper.PaymentEnvironment, Is.EqualTo("LIVE"));

            SiteHelper.BaseEnvironment = "DEMO";
            Assert.That(SiteHelper.PaymentEnvironment, Is.EqualTo("DEMO"));
        }

        /// <summary>
        /// Scenario: Call BuildSiteMap with valid arguments
        /// Expected: Correct sitemap xml
        /// </summary>
        [Test]
        public void _003_BuildSiteMap_Valid()
        {
            string output = SiteHelper.BuildSiteMap("http://www.test.com/", ".", ".", new string[] { "*.*" }, new string[] { "Codentia.Common.Helper.dll" }, new string[] { "excludefolder" });

            // Assert.That(output, Is.EqualTo("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>http://www.test.com/Coverage.Log</loc><lastmod>2011-07-25</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Data.dll</loc><lastmod>2010-12-15</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Data.pdb</loc><lastmod>2010-12-15</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Data.xml</loc><lastmod>2010-12-15</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Helper.dll</loc><lastmod>2011-07-26</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Helper.pdb</loc><lastmod>2011-07-26</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Helper.Test.dll</loc><lastmod>2011-07-26</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Helper.Test.dll.config</loc><lastmod>2010-09-30</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Helper.Test.dll.temp.config</loc><lastmod>2011-07-26</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Helper.Test.dll.VisualState.xml</loc><lastmod>2011-07-25</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Helper.Test.pdb</loc><lastmod>2011-07-26</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Helper.Test.XML</loc><lastmod>2011-07-26</lastmod></url><url><loc>http://www.test.com/Codentia.Common.Helper.xml</loc><lastmod>2011-07-26</lastmod></url><url><loc>http://www.test.com/Codentia.Test.dll</loc><lastmod>2010-08-16</lastmod></url><url><loc>http://www.test.com/Codentia.Test.pdb</loc><lastmod>2010-08-16</lastmod></url><url><loc>http://www.test.com/Codentia.Test.xml</loc><lastmod>2009-08-06</lastmod></url><url><loc>http://www.test.com/nunit.framework.dll</loc><lastmod>2010-04-22</lastmod></url><url><loc>http://www.test.com/nunit.framework.xml</loc><lastmod>2010-04-22</lastmod></url><url><loc>http://www.test.com/TestResult.xml</loc><lastmod>2011-07-25</lastmod></url><url><loc>http://www.test.com/assets/testimage.jpg</loc><lastmod>2011-07-25</lastmod></url></urlset>"));
        }
    }
}
