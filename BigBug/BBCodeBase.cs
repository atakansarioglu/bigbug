/**
 * @file      BBCodeBase.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Keeps a dictionary of all known BBCode objects and provides API
 *            for searching, inserting and decoding of BigBug messages.
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
using System.Collections.Generic;

namespace BigBug
{
    public class BBCodeBase
    {
        private Dictionary<string, BBCodeObject> dataBase = new Dictionary<string, BBCodeObject>(); ///< Keeps all identified BBCode descriptors and searching by shortCode possible.

        /**
         * @brief Method to add new BBCodeObject into the dictionary.
         * @param bbCode The new object to add (if it is not null).
         * @return false if bbCode is null or duplicate is found with different format. true otherwise.
         */
        public bool AddBBCode(BBCodeObject bbCode)
        {
            // Return false if bbCode is null.
            if(bbCode == null)
                return false;

            // Search the shortCode in the map.
            if (dataBase.ContainsKey(bbCode.shortCode))
            {
                // If the shortCode is already in the dictionary, compare the formatStrings.
                if (dataBase[bbCode.shortCode].formatString != bbCode.formatString)
                {
                    // If format strings mismatch, give a warning and keep the first added one.
                    Console.WriteLine("WARNING: BB[" + bbCode.shortCode + "] defined in " + dataBase[bbCode.shortCode].sourceFile + ":" + dataBase[bbCode.shortCode].sourceFileLine + " has redefined in " + bbCode.sourceFile + ":" + bbCode.sourceFileLine);
                    return false;
                }
            }
            else
            {
                // Add the bbCode to the dictionary.
                dataBase[bbCode.shortCode] = bbCode;
            }

            // Return true.
            return true;
        }

        /**
         * @brief Method to fetch a BBCodeObject by its shortCode.
         * @param shortCode
         * @return object or null if no match found.
         */
        public BBCodeObject FindBBCode(string shortCode)
        {
            // Search the dictionary.
            if (dataBase.ContainsKey(shortCode))
            {
                // Return the found object.
                return dataBase[shortCode];
            }
            else
            {
                // Return null if no match is found.
                return null;
            }
        }

        /**
         * @brief Method to decode an encoded BBCode message.
         * @param encodedMessage
         * @return decoded message if it could be decoded or the encoded message as-is.
         */
        public string GetDecodedMessage(string encodedMessage)
        {
            // Check for zero length.
            if (encodedMessage.Length > 0)
            {
                // Parse the decoded message to get its shortcode and payload.
                string shortCode;
                List<string> payloadArray;
                BBCodeDecoder.GetShortCodeAndPayloadsFromEncodedMessage(encodedMessage, out shortCode, out payloadArray);

                // Search the shortCode of the encoded message in teh dictionary.
                if (dataBase.ContainsKey(shortCode))
                {
                    // If match found, use the matched BBCodeObject and payloads from the encoded message to decode it.
                    return BBCodeDecoder.SubstitutePayloadsIntoFormat(ref payloadArray, ref dataBase[shortCode].formatPrimitiveArray);
                }
            }

            // Return the message as-is if decoding failed.
            return encodedMessage;
        }

        /**
         * @brief Gets the number of identified BBCodeObject.
         * @return the count.
         */
        public int GetCodeCount()
        {
            return dataBase.Count;
        }
    }
}
