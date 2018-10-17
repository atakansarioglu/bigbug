/**
 * @file      Settings.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Wrapper for saving and loading settings from/to file.
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

using inifile;

namespace BigBug
{
    class Settings
    {
        private IniFile settings;///< Ini file object.

        /**
         * @brief Constructor.
         * @param fileName Settings file to open.
         */
        public Settings(string fileName)
        {
            //-- Initialize the settings file.
            settings = new IniFile(fileName);
        }

        /**
         * @brief Loads the requested setting from file.
         * @param settingName i.e. General.Baud means Baud key of the General section.
         * @return the value of the key or empty string if key doesnt exist.
         */
        public string LoadSetting(string settingName)
        {
            string value = "";
            string[] keys = settingName.Split('.');

            if (keys.Length == 2)
            {
                if (settings.KeyExists(keys[1], keys[0]))
                {
                    value = settings.Read(keys[1], keys[0]);
                }
            }

            return value;
        }

        /**
         * @brief Saves/overwrites the requested setting to file.
         * @param settingName i.e. General.Baud means Baud key of the General section.
         * @param value Value as string.
         */
        public void SaveSetting(string settingName, string value)
        {
            string[] keys = settingName.Split('.');

            if (keys.Length == 2)
            {
                settings.Write(keys[1], value, keys[0]);             
            }
        }
    }
}
