/**
 * @file      DataFilter.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Provides method for filtering the displayed decoded messages.
 *
 * @copyright Copyright (c) 2018 Atakan SARIOGLU ~ www.atakansarioglu.com
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),
 *  to deal in the Software without restriction, including without limitation
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,
 *  and/or sell copies of the Software, and to permit persons to whom the
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 *  DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Data;
using System.Text.RegularExpressions;

namespace BigBug
{
    class DataFilter
    {
        /**
         * @brief provides basic filter functuonality to the messages.
         * @param dataTable The data table to apply the filter.
         * @param filterText Filter text. OR condition is possible using '|' character.
         *        Example: "Dat|ter" matches "Data" and "Filter"
         */
        public static void Set(ref DataTable dataTable, string filterText)
        {
            // Null check.
            if ((dataTable != null) && (filterText != null))
            {
                // Split the filterText with pipe character.
                string[] filterConditions = filterText.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                // Construct the query.
                string queryString = "";
                foreach (string condition in filterConditions)
                {
                    // Use OR between different conditions.
                    if (queryString != "")
                    {
                        queryString += " OR ";
                    }

                    // Use LIKE compariosn and wildcards on both sides of the query value.
                    string escapedCondition = Regex.Replace(condition, "[*%[]|]", "[$0]").Replace("'", "''");
                    queryString += "Event LIKE '%" + escapedCondition + "%'";
                }

                // Make the table case insensitive and apply the constructed row filter.
                dataTable.CaseSensitive = false;
                dataTable.DefaultView.RowFilter = queryString;
            }
        }
    }
}
