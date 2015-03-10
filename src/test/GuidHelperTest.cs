using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Codentia.Common.Helper.Test
{
    /// <summary>
    /// This class contains the unit tests for the static class GuidHelper
    /// </summary>
    [TestFixture]
    public class GuidHelperTest
    {
        /// <summary>
        /// Scenario: Call WriteNullableGuid with Guid.Empty and Guid.NewGuid()
        /// Expected: Returns DBNull.Value for Guid.Empty and Guid.NewGuid()
        /// </summary>
        [Test]
        public void _001_WriteNullableGuid()
        {
            Assert.That(GuidHelper.WriteNullableGuid(Guid.Empty), Is.EqualTo(DBNull.Value));
            Guid guid = Guid.NewGuid();
            Assert.That(GuidHelper.WriteNullableGuid(guid), Is.EqualTo(guid));
        }

        /// <summary>
        /// Scenario: Call GetNullableGuid with DBNull.Value and Guid.NewGuid()
        /// Expected: Returns Guid.Empty for DBNull.Value and Guid.NewGuid()
        /// </summary>
        [Test]
        public void _002_GetNullableGuid()
        {
            Assert.That(GuidHelper.GetNullableGuid(DBNull.Value), Is.EqualTo(Guid.Empty));
            Guid guid = Guid.NewGuid();
            Assert.That(GuidHelper.GetNullableGuid(guid), Is.EqualTo(guid));
            string guidString = "01234567-aabb-ccff-2233-987654321000";
            Guid guidcheck = new Guid(guidString);
            Assert.That(GuidHelper.GetNullableGuid(guidString), Is.EqualTo(guidcheck));
        }
    }
}
