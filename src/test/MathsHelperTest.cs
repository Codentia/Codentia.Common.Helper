using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
    /// <summary>
    /// TestFixture for MathsHelper
    /// <seealso cref="MathsHelper"/>
    /// </summary>
    [TestFixture]
    public class MathsHelperTest
    {       
        /// <summary>
        /// Set values required for all tests
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        /// <summary>
        /// Scenario: 
        /// Expected: 
        /// </summary>
        [Test]
        public void _001_ClassicRounding_InvalidDecimalPlaces()
        {
            Assert.That(delegate { MathsHelper.ClassicRounding(3.456M, 0); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("decimalPlaces must be 1 or 2"));
            Assert.That(delegate { MathsHelper.ClassicRounding(3.456M, 3); }, Throws.InstanceOf<ArgumentException>().With.Message.EqualTo("decimalPlaces must be 1 or 2"));
        }

        /// <summary>
        /// Scenario: 
        /// Expected: 
        /// </summary>
        [Test]
        public void _002_ClassicRounding_Zero()
        {
            decimal newValue = MathsHelper.ClassicRounding(0, 1);
            Assert.That(newValue, Is.EqualTo(0M));
        }

        /// <summary>
        /// Scenario: 
        /// Expected: 
        /// </summary>
        [Test]
        public void _003_ClassicRounding_WholeNumber()
        {
            decimal newValue = MathsHelper.ClassicRounding(10M, 1);
            Assert.That(newValue, Is.EqualTo(10M));
        }

        /// <summary>
        /// Scenario: 
        /// Expected: 
        /// </summary>
        [Test]
        public void _004_ClassicRounding_1DecimalPlace()
        {
            decimal newValue = MathsHelper.ClassicRounding(0.10M, 1);           
            Assert.That(newValue, Is.EqualTo(0.1M));

            newValue = MathsHelper.ClassicRounding(0.11M, 1);
            Assert.That(newValue, Is.EqualTo(0.1M));

            newValue = MathsHelper.ClassicRounding(1.12M, 1);            
            Assert.That(newValue, Is.EqualTo(1.1M));

            newValue = MathsHelper.ClassicRounding(1.13M, 1);            
            Assert.That(newValue, Is.EqualTo(1.1M));

            newValue = MathsHelper.ClassicRounding(10.14M, 1);            
            Assert.That(newValue, Is.EqualTo(10.1M));

            newValue = MathsHelper.ClassicRounding(10.15M, 1);           
            Assert.That(newValue, Is.EqualTo(10.2M));

            newValue = MathsHelper.ClassicRounding(100.16M, 1);            
            Assert.That(newValue, Is.EqualTo(100.2M));

            newValue = MathsHelper.ClassicRounding(1000.17M, 1);
            Assert.That(newValue, Is.EqualTo(1000.2M));

            newValue = MathsHelper.ClassicRounding(10000.18M, 1);            
            Assert.That(newValue, Is.EqualTo(10000.2M));

            newValue = MathsHelper.ClassicRounding(1234.19M, 1);            
            Assert.That(newValue, Is.EqualTo(1234.2M));
        }

        /// <summary>
        /// Scenario: 
        /// Expected: 
        /// </summary>
        [Test]
        public void _005_ClassicRounding_2DecimalPlaces()
        {
            decimal newValue = MathsHelper.ClassicRounding(2.100M, 2);            
            Assert.That(newValue, Is.EqualTo(2.1M));

            newValue = MathsHelper.ClassicRounding(3.111M, 2);            
            Assert.That(newValue, Is.EqualTo(3.11M));

            newValue = MathsHelper.ClassicRounding(4.123M, 2);
            
            Assert.That(newValue, Is.EqualTo(4.12M));

            newValue = MathsHelper.ClassicRounding(5.245M, 2);            
            Assert.That(newValue, Is.EqualTo(5.25M));

            newValue = MathsHelper.ClassicRounding(10.99M, 2);
            Assert.That(newValue, Is.EqualTo(10.99M));

            newValue = MathsHelper.ClassicRounding(10.994M, 2);            
            Assert.That(newValue, Is.EqualTo(10.99M));

            newValue = MathsHelper.ClassicRounding(10.995M, 2);
            Assert.That(newValue, Is.EqualTo(11M));

            newValue = MathsHelper.ClassicRounding(10.999M, 2);            
            Assert.That(newValue, Is.EqualTo(11M));

            newValue = MathsHelper.ClassicRounding(10.155M, 2);            
            Assert.That(newValue, Is.EqualTo(10.16M));

            newValue = MathsHelper.ClassicRounding(100.165M, 2);            
            Assert.That(newValue, Is.EqualTo(100.17M));

            newValue = MathsHelper.ClassicRounding(1000.179M, 2);            
            Assert.That(newValue, Is.EqualTo(1000.18M));

            newValue = MathsHelper.ClassicRounding(1000.596M, 2);            
            Assert.That(newValue, Is.EqualTo(1000.60M));

            // the cafejac issue specifically!!
            newValue = MathsHelper.ClassicRounding(0.381M, 2);
            Assert.That(newValue, Is.EqualTo(0.38));
            newValue = MathsHelper.ClassicRounding(0.382M, 2);
            Assert.That(newValue, Is.EqualTo(0.38));
            newValue = MathsHelper.ClassicRounding(0.383M, 2);
            Assert.That(newValue, Is.EqualTo(0.38));
            newValue = MathsHelper.ClassicRounding(0.384M, 2);
            Assert.That(newValue, Is.EqualTo(0.38));
            newValue = MathsHelper.ClassicRounding(0.385M, 2);
            Assert.That(newValue, Is.EqualTo(0.39));
            newValue = MathsHelper.ClassicRounding(0.386M, 2);
            Assert.That(newValue, Is.EqualTo(0.39));
            newValue = MathsHelper.ClassicRounding(0.387M, 2);
            Assert.That(newValue, Is.EqualTo(0.39));
            newValue = MathsHelper.ClassicRounding(0.388M, 2);
            Assert.That(newValue, Is.EqualTo(0.39));
            newValue = MathsHelper.ClassicRounding(0.389M, 2);
            Assert.That(newValue, Is.EqualTo(0.39));

            newValue = MathsHelper.ClassicRounding(1234.567890123456789M, 2);
            Assert.That(newValue, Is.EqualTo(1234.57));

            newValue = MathsHelper.ClassicRounding(555.555M, 2);            
            Assert.That(newValue, Is.EqualTo(555.56));
        }
    }
}
