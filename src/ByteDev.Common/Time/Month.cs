using System;
using System.Globalization;

namespace ByteDev.Common.Time
{
    public class Month
    {
        public Month(int monthNumber)
        {
            if (monthNumber < 1 || monthNumber > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(monthNumber), "Month number must be between 1 and 12.");
            }
            Number = monthNumber;
        }

        public int Number { get; }

        /// <summary>
        /// The month full name.
        /// </summary>
        public string Name
        {
            get { return CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[Number - 1]; }
        }

        /// <summary>
        /// Three letter abbreviation of name.
        /// </summary>
        public string ShortName
        {
            get { return CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[Number - 1]; }
        }
    }
}