/**
 * @file      BBCodeDescriptorParser.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Parses the BBCode descriptor given in the source code of project.
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
    public class BBCodeDescriptorParser
    {
        /**
         * @brief Try to parse provided text as BBCode descriptor.
         * @param lineText String to parse. Possible BB descriptor lines:
         * ~~~~
         *        1. printf("aZ...igored...//@BB Hello World!
         *        2. puts("mm...igored...//@BB Measured value is {0}.
         *        3. ...igored...@BB[tm] Time is {0}:{1} {2:(0),(1)AM,(2)PM} and today is {3:Mon,Tue,Wed,Thu,Fri,Sat,Sun}.
         * ~~~~
         * @param sourceFile the file name where this line is taken.
         * @param sourceFileLine line number of this text.
         * @return BBCodeObject if descriptor found, null otherwise.
         */
        public static BBCodeObject Parse(string lineText, string sourceFile, int sourceFileLine)
        {
            BBCodeObject bbCode = null;
            Match match;
            
            // Regexp matcher for printf and puts style descriptors.
            match = Regex.Match(lineText, @"(?:printf\(""|puts\("")([a-zA-Z]{2}).*//@BB[\s]+(.*)[\s]*$");

            // If above match fails, try to match //@BB[..] style descriptors.
            if (!match.Success)
                match = Regex.Match(lineText, @"@BB\[([a-zA-Z]{2})\][\s]+(.*)[\s]*$");

            // Check for success.
            if (match.Success)
            {
                // Get the shortcode and format string.
                string shortCode = match.Groups[1].Captures[0].Value;
                string formatString = match.Groups[2].Captures[0].Value;

                // Create a BBCodeObject.
                bbCode = new BBCodeObject(shortCode, formatString, sourceFile, sourceFileLine);
            }

            // Return.
            return bbCode;
        }

        /**
         * @brief Break down the BBCode descriptor's format string into its primitives.
         * @param formatString I.e. "Time is {0}:{1} {2:(0),(1)AM,(2)PM} and today is {3:Mon,Tue,Wed,Thu,Fri,Sat,Sun}.". 
         *        Curly brackets and backslash characters in string should be escaped with backslash.
         * @return list of BBCodeFormatPrimitive loaded with parsed formatters.
         */
        public static List<BBCodeFormatPrimitive> ParseFormatString(string formatString)
        {
            // Create an empty list.
            List<BBCodeFormatPrimitive> formatPrimitiveArray = new List<BBCodeFormatPrimitive>();

            // Regex matcher for format primitive descriptors.
            MatchCollection matches = Regex.Matches(formatString, @"((?:(?:\\\\)|(?:\\\{)|[^{])+)|\{(?:([0-9]+)(?::((?:(?:\\\\)|(?:\\\})|[^}])*))?)\}");

            // Create BBCodeFormatPrimitive from all matches.
            for (int i = 0; i < matches.Count; i++)
            {
                int replacementIndex = -1;
                string enumValuesString = "";
                string directString = "";

                // 1st element is direct string.
                if (matches[i].Groups[1].Captures.Count > 0)
                    directString = UnescapeFormatString(matches[i].Groups[1].Captures[0].Value);

                // 2nd element is the replacement index.
                if (matches[i].Groups[2].Captures.Count > 0)
                    int.TryParse(matches[i].Groups[2].Captures[0].Value, out replacementIndex);

                // 3rd element is enum descriptor string.
                if (matches[i].Groups[3].Captures.Count > 0)
                    enumValuesString = UnescapeFormatString(matches[i].Groups[3].Captures[0].Value);

                // Create a formatPrimitive and insert into the list.
                BBCodeFormatPrimitive bbCodeFormatPrimitive = new BBCodeFormatPrimitive(directString, replacementIndex, enumValuesString);
                formatPrimitiveArray.Add(bbCodeFormatPrimitive);
            }

            // return.
            return formatPrimitiveArray;
        }

        /**
         * @brief Unescape "\\", "\{" and "\}" characters in the given string, assuming the escapedString has no "\n" character.
         * @param escapedString
         * @return unescapedString
         */
        private static string UnescapeFormatString(string escapedString)
        {
            // First change the escaped escape character (\), unescape other escaped characters ({}) then change escape character (\) back.
            return escapedString.Replace(@"\\", @"\n").Replace(@"\{", @"{").Replace(@"\}", @"}").Replace(@"\n", @"\");
        }
    }
}
