/**
 * @file      BBCodeObject.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Represents a description of BigBug object.
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

namespace BigBug
{
    public class BBCodeObject
    {
        public string shortCode; ///< 2-letter short code to identify the message.
        public string formatString; ///< Format descriptor string.
        public string sourceFile; ///< Source file that has descriptor for this BBCodeObject.
        public int sourceFileLine; ///< Line number of the source file.
        public List<BBCodeFormatPrimitive> formatPrimitiveArray; ///< List of format primitives that are described in the description of the format string.

        /**
        * @brief Constructor.
        * @param shortCode 2-letter short code to identify the message.
        * @param formatString Format descriptor string.
        * @param sourceFile Source file that has descriptor for this BBCodeObject.
        * @param sourceFileLine Line number of the source file
        */
        public BBCodeObject(string shortCode, string formatString, string sourceFile, int sourceFileLine)
        {
            // Assign direct values.
            this.shortCode = shortCode;
            this.formatString = formatString;
            this.sourceFile = sourceFile;
            this.sourceFileLine = sourceFileLine;

            // Fill the format primitive list using the parser.
            this.formatPrimitiveArray = BBCodeDescriptorParser.ParseFormatString(formatString);
        }
    }
}
