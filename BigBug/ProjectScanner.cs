/**
 * @file      ProjectScanner.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Scans all files in a directory or a single file for BBCode.
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

using System.IO;
using System.Linq;

namespace BigBug
{
    class ProjectScanner
    {
        /**
         * @brief Scans a project location of file and fills the BBCodeBase.
         * @param projectPath is path to a directory or a single file.
         * @param supportedFileExtensions string array of extensions.
         * @param bbCodeBase clears all entries in this object and adds the new entries.
         * @return true if entries are found.
         */
        public static bool Scan(string projectPath, string[] supportedFileExtensions, ref BBCodeBase bbCodeBase)
        {
            // Create an empty BBCode database.
            bbCodeBase = new BBCodeBase();

            // Check if the given is a file or directory.
            if (File.Exists(projectPath))
            {
                // Scan single file.
                ScanSingleFile(projectPath, ref bbCodeBase);
            }
            else
            {
                // Create empty file list.
                string[] fileList = { };

                // Try to get file names under the selected path.
                try
                {
                    fileList = Directory.GetFiles(projectPath, "*.*", SearchOption.AllDirectories);
                }
                catch { };

                // Get the selected path and make sure that its not empty.
                if (fileList.Length > 0)
                {
                    // Iterate all files under the selected path.
                    foreach (string fileName in fileList)
                    {
                        // Check the file extension for allowed extensions.
                        if (File.Exists(fileName) && ((fileList.Length == 1) || supportedFileExtensions.Contains(Path.GetExtension(fileName).TrimStart('.'))))
                        {
                            ScanSingleFile(fileName, ref bbCodeBase);
                        }
                    }
                }
            }

            // Success if descriptors are found.
            return (bbCodeBase.GetCodeCount() > 0);
        }

        /**
         * @brief Scans a project location of file and fills the BBCodeBase.
         * @param fileName is path to a directory or a single file.
         * @param bbCodeBase clears all entries in this object and adds the new entries.
         * @return true on successful scan.
         */
        private static bool ScanSingleFile(string fileName, ref BBCodeBase bbCodeBase)
        {
            try
            {
                // Open the file as text.
                StreamReader sr = File.OpenText(fileName);

                // Iterate all lines until the end.
                string lineText;
                int lineNumber = 0;
                while (!sr.EndOfStream)
                {
                    // Read single line.
                    lineText = sr.ReadLine();
                    lineNumber++;

                    // Try to parse the line with descriptor parser. Result is not null if successful.
                    BBCodeObject bbCode = BBCodeDescriptorParser.Parse(lineText, fileName, lineNumber);

                    // Add to the database (only if not null).
                    bbCodeBase.AddBBCode(bbCode);
                }

                // Close the file.
                sr.Close();

                // Everything is OK.
                return true;
            }
            catch { }

            // Problem occured.
            return false;
        }
    }
}
