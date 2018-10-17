/**
 * @file      BBCodeDecoder.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Decodes BBCode messages provided the data and format.
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
    public class BBCodeDecoder
    {
        /**
         * @brief Parses payload values from the given string. Delimiter is single space and space character can be escaped with backslash (as well as the backslash itself).
         * @param payloadString the string that includes all payloads, i.e. "3 foo -6.9 hello\ world". A payload can be empty string too.
         * @param payloadArray is filled with all payload strings. A payload can be empty string too.
         * @return true on success or no-payload.
         */
        private static bool ParsePayloadsFromString(string payloadString, out List<string> payloadArray)
        {
            // Check string length.
            if (payloadString.Length > 0)
            {
                // Regexp matcher splits the string by unescaped spaces.
                MatchCollection matchPayloads = Regex.Matches(payloadString, @"((?:(?:[\\]{2})|(?:[\\][ ])|[^\s])*)(?:$|[\s])");

                // Create an array.
                payloadArray = new List<string>(matchPayloads.Count);

                // Pass the found payloads to the created array.
                for (int i = 0; i < matchPayloads.Count; i++)
                    payloadArray.Add(UnescapePayloadString(matchPayloads[i].Groups[1].Value));

                // Return success if the payloadString could be parsed.
                return (payloadArray.Count > 0);
            }
            else
            {
                // Return empty array when no payload is present.
                payloadArray = new List<string>();
                return true;
            }
        }

        /**
         * @brief Unescape the escaped "\\" and "\ " characters, assuming the escapedString has no "\n" character.
         * @param escapedString
         * @return unescapedString
         */
        private static string UnescapePayloadString(string escapedString)
        {
            // First change the escaped escape character (\), unescape other escaped characters ( ) then change escape character (\) back.
            return escapedString.Replace(@"\\", @"\n").Replace(@"\ ", @" ").Replace(@"\n", @"\");
        }

        /**
         * @brief Extracts shortCode and payloads from the encoded message.
         * @param encodedMessage the string that includes the shortcode and all payloads, i.e. "SC3 foo -6.9 hello\ world". A payload can be empty string too.
         * @param shortCode first 2 letters of the encoded message.
         * @param payloadArray is filled with all payload strings. A payload can be empty string too.
         * @return true on success.
         */
        public static bool GetShortCodeAndPayloadsFromEncodedMessage(string encodedMessage, out string shortCode, out List<string> payloadArray)
        {
            // Split the encoded message into shortCode and the rest.
            Match matchShortCode = Regex.Match(encodedMessage, @"^([a-zA-Z]{2})(.*)");
            if (matchShortCode.Success)
            {
                // If success, assign the shortcode to the parameter.
                shortCode = matchShortCode.Groups[1].Captures[0].Value;

                // Get the payload string and get it parsed.
                string payloadString = matchShortCode.Groups[2].Captures[0].Value;
                return ParsePayloadsFromString(payloadString, out payloadArray);
            }
            else
            {
                // Assign empty array and empty shortcode on failure.
                shortCode = "";
                payloadArray = new List<string>();

                // Return false.
                return false;
            }
        }

        /**
         * @brief Substitutes the payload data into BBCode format using BBCodeFormatPrimitive.
         * @param formatPrimitiveArray list of format primitives for the BBCodeObject that will be used to construct the decoded message.
         * @param payloadArray is filled with all payload strings. A payload can be empty string too.
         * @return true on success.
         */
        public static string SubstitutePayloadsIntoFormat(ref List<string> payloadArray, ref List<BBCodeFormatPrimitive> formatPrimitiveArray)
        {
            // Cosntruct the decoded message.
            string decodedMessage = "";
            for (int i = 0; i < formatPrimitiveArray.Count; i++)
            {
                // Cosntruct string from each format primitive.
                decodedMessage += formatPrimitiveArray[i].ToString(ref payloadArray);
            }

            return decodedMessage;
        }
    }
}
