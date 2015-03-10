using System;

namespace Codentia.Common.Helper
{
    /// <summary>
    /// This static class encapsulates utility methods for dealing with Guids
    /// </summary>
    public static class GuidHelper
    {
        /// <summary>
        /// Writes the nullable guid.
        /// </summary>
        /// <param name="guid">The guid.</param>
        /// <returns>object - if guid is Guid.Empty returns DBNull.Value otherwise guid is returned</returns>
        public static object WriteNullableGuid(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                return DBNull.Value;
            }
            else
            {
                return guid;
            }
        }

        /// <summary>
        /// Gets the nullable guid.
        /// </summary>
        /// <param name="guid">The guid.</param>
        /// <returns>Guid - if guid is DBNull.Value then returns Guid.Empty otherwise guid is returned</returns>
        public static Guid GetNullableGuid(object guid)
        {
            if (guid == DBNull.Value)
            {
                return Guid.Empty;
            }
            else
            {
                return new Guid(Convert.ToString(guid));
            }
        }
    }
}
