using System;
using System.Text;

namespace Codentia.Common.Helper
{
    /// <summary>
    /// IFormatProvider implementation for formatting an array of type int[] to a string with an optional delimiter.
    /// </summary>
    public class IntArrayFormatter : IFormatProvider, ICustomFormatter
    {
        private string _delimiter = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntArrayFormatter"/> class using no delimiter
        /// </summary>
        public IntArrayFormatter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntArrayFormatter"/> class using string delimiter
        /// </summary>
        /// <param name="delimiter">delimiter (for string)</param>
        public IntArrayFormatter(string delimiter)
        {
            this._delimiter = delimiter;
        }

        #region IFormatProvider Members

        /// <summary>
        /// Retrieve the format for a given type
        /// </summary>
        /// <param name="formatType">format Type</param>
        /// <returns>object (formatted)</returns>
        public object GetFormat(Type formatType)
        {
            //// if (formatType == typeof(ICustomFormatter))
            //// {
                return this;
            //// }
        }

        #endregion

        #region ICustomFormatter Members

        /// <summary>
        /// Format an array of integers into a string.
        /// </summary>
        /// <param name="format">Format to output into</param>
        /// <param name="arg">Argument parameter (object)</param>
        /// <param name="formatProvider">FormatProvider to use</param>
        /// <returns>string after using provider</returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            //// if (formatProvider == this)
            //// {
                StringBuilder builder = new StringBuilder();

                int[] argument = (int[])arg;

                for (int i = 0; i < argument.Length; i++)
                {
                    if (i > 0)
                    {
                        builder.Append(this._delimiter);
                    }

                    builder.Append(argument[i]);
                }

                return builder.ToString();
            /* }
            else
            {
                return null;
            } */
        }

        #endregion
    }
}
