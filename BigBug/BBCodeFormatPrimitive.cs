/**
 * @file      BBCodeFormatPrimitive.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Represents each element in a BBCode descriptor.
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

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BigBug
{
    public class BBCodeFormatPrimitive
    {
        public string directString; ///< String literal that will be added to the decoded message as-is.
        public int replacementIndex; ///< Index number of the payload to substitute.
        public Dictionary<int, string> enumDescriptor; ///< Enumeration dictionary fo enum types.

        /**
         * @brief Constructor
         * @param directString String literal that will be added to the decoded message as-is.
         * @param replacementIndex Index number of the payload to substitute.
         *        Format: {0} where 0 is the payload index number to be used.
         * @param enumDescriptorString Enumeration descriptor. Possible formats:
         *        Implicit: {0:Option0,Option1,Option2} where 0 is the payload index number to be used.
         *        Explicit: {0:(2)Option0,(3)Option1,(5)Option2} where (2) is the enum mapping index. Missing mapping indexes are possible.
         */
        public BBCodeFormatPrimitive(string directString, int replacementIndex, string enumDescriptorString)
        {
            // Set directString.
            this.directString = directString;

            // Set replacement index.
            this.replacementIndex = replacementIndex;

            // Parse and set enumDescriptor.
            if (enumDescriptorString != "")
                ParseEnumDescriptor(enumDescriptorString);
        }

        /**
         * @brief Parses enumDescriptorString into enum dictionary.
         * @param enumDescriptorString Enumeration descriptor. Possible formats:
         *        Implicit: {0:Option0,Option1,Option2} where 0 is the payload index number to be used.
         *        Explicit: {0:(2)Option0,(3)Option1,(5)Option2} where (2) is the enum mapping index. Missing mapping indexes are possible.
         */
        private void ParseEnumDescriptor(string enumDescriptorString)
        {
            // Create an empty dictionary.
            enumDescriptor = new Dictionary<int, string>();

            // Split enumDescriptorString using comma delimiter.
            string[] enumDescriptorArray = enumDescriptorString.Split(',');

            // Iterate all enum sub-items.
            for(int i = 0; i < enumDescriptorArray.Length; i++)
            {
                // Check for explicit enum type.
                if ((enumDescriptorArray[i].Length > 0) && (enumDescriptorArray[i][0] == '(')) {

                    // Parse the sub-item string to find enum mapping index descriptor if present.
                    Match match = Regex.Match(enumDescriptorArray[i], @"^\(([0-9]+)\)(.*)");
                    if (match.Success)
                    {
                        // Parse the mapping index.
                        int index = -1;
                        int.TryParse(match.Groups[1].Captures[0].Value, out index);

                        // Add the mapping index and value to the dictionary as a pair.
                        if (index >= 0)
                            enumDescriptor.Add(index, match.Groups[2].Captures[0].Value);
                    }
                }
                else
                {
                    // Add implicit enum type to the dictionary.
                    enumDescriptor.Add(i, enumDescriptorArray[i]);
                }
            }
        }

        /**
         * @brief Construct the primitive as string provided the payload array.
         * @param payloadArray List of payload data.
         * @return string.
         */
        public string ToString(ref List<string> payloadArray)
        {
            // Check if the primitive is string literal type.
            if (directString != "")
            {
                return directString;
            }
            else if((replacementIndex >= 0) && (payloadArray.Count > replacementIndex))
            {
                // If the type is not enum, return the payload at replacementIndex. 
                if (enumDescriptor == null)
                {
                    return payloadArray[replacementIndex];
                }
                else
                {
                    // Parse the mapping index from the payload at replacementIndex.
                    int enumIndex = -1;
                    int.TryParse(payloadArray[replacementIndex], out enumIndex);

                    // If enumDescriptor has the index, return the value.
                    if ((enumIndex >= 0) && enumDescriptor.ContainsKey(enumIndex))
                    {
                        return enumDescriptor[enumIndex];
                    }
                }
            }

            // Return empty if things went wrong.
            return "";
        }
    }
}
