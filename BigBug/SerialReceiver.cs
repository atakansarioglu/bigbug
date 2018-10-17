/**
 * @file      SerialReceiver.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Serial communication class for receiption and placement of BBCode
 *            formatted messages from the debugged device.
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

using System.Data;
using System.IO.Ports;
using System.Linq;
using System;

namespace BigBug
{
    class SerialReceiver
    {
        SerialPort serialPort; ///< Serial port object.
        DataTable dataTable; ///< Internal reference of the data table.
        string serialData = ""; ///< String holds the unprocessed serial port data i.e. incomplete line data that is not terminated with new line character.

        /**
         * @brief Constructor initializes the data table.
         * @param dataTable Data table to fill the received data.
         */
        public SerialReceiver(ref DataTable dataTable)
        {
            //-- Data Table.
            dataTable.Columns.Add("Time", typeof(string));
            dataTable.Columns.Add("Event", typeof(string));
            this.dataTable = dataTable;
        }

        /**
         * @brief Opens serial port connection.
         * @param portName i.e. COM5.
         * @param baudText i.e. 9600.
         * @param hasParity
         * @param evenParity
         * @return true on success
         */
        public bool OpenPort(string portName, string baudText, bool hasParity, bool evenParity)
        {
            // If port is already open, return true.
            if((serialPort != null) && serialPort.IsOpen)
                return true;

            // Checkk if the requested port exists on the system.
            if(!SerialPort.GetPortNames().Contains(portName))
                return false;

            // Get baud as integer.
            int baudInt;
            if (int.TryParse(baudText, out baudInt))
            {
                try
                {
                    // Create serial port object with provided parameters.
                    serialPort = new SerialPort(portName, baudInt, hasParity ? (evenParity ? Parity.Even : Parity.Odd) : Parity.None);
                    serialPort.ReadBufferSize = 65536;

                    // Enable DTR and RTS to provide power for parasit-powered isolated serial port adapter
                    // such as expleined in Texas Instruments TIDU298 <http://www.ti.com/lit/ug/tidu298/tidu298.pdf>
                    serialPort.DtrEnable = true;
                    serialPort.RtsEnable = true;

                    // Attemp to open the port.
                    serialPort.Open();

                    // Clear the buffer.
                    serialData = "";

                    // Return true if everything is fine.
                    return true;
                }
                catch { }
            }

            // A problem occured.
            return false;
        }

        /**
         * @brief Closes serial port connection.
         */
        public void ClosePort()
        {
            // Close if it is open.
            if(IsOpen())
                serialPort.Close();
        }

        /**
         * @brief Checks if port is open.
         * @return true if the searial port is open.
         */
        public bool IsOpen()
        {
            return ((serialPort != null) && serialPort.IsOpen);
        }

        /**
         * @brief Process the received line of data.
         * @param receivedLine the unprocessed line data.
         * @param dataTable Data table to fill the received data.
         * @param bbCodeBase BBCode database to process the received data.
         */
        private bool ProcessReceivedLine(string receivedLine, ref BBCodeBase bbCodeBase, ref DataTable dataTable)
        {
            // Decode the message.
            string decodedMessage = bbCodeBase.GetDecodedMessage(receivedLine);

            // Check message length.
            if (decodedMessage != "")
            {
                // Add time.
                dataTable.Rows.Add(new object[] { DateTime.Now.ToString("HH:mm:ss.ffffff"), decodedMessage });

                // Return true.
                return true;
            }
            else
            {
                // Return false.
                return false;
            }
        }

        /**
         * @brief Keeps the data table rows less that the given number.
         * @param dataTable data table to limit.
         * @param maxDataRows data table line number limit.
         */
        private void DeleteOldLines(ref DataTable dataTable, int maxDataRows)
        {
            // Remove the first element while data table has more than allowed rows.
            while (dataTable.Rows.Count > maxDataRows)
                dataTable.Rows.RemoveAt(0);
        }

        /**
         * @brief Periodically called serial data receiver and parser.
         * @param bbCodeBase BBCode database to process the received data.
         * @param maxDataRows data table line number limit.
         * @return true if new message received and processed.
         */
        public bool GetSerialData(ref BBCodeBase bbCodeBase, int maxDataRows)
        {
            // Save the time on entry to measure performance.
            DateTime timeEntry = DateTime.Now;

            // Keep a flag that is true when a new line received.
            bool newDataReceived = false;

            // Null check for serial port. Then check if there is data ready to be read.
            if ((serialPort != null) && (serialPort.IsOpen) && (serialPort.BytesToRead > 0))
            {
                // Read and append all available data into buffer.
                serialData += serialPort.ReadExisting();

                // Split the the buffer data into lines.
                string[] serialLines = serialData.Split('\n');
                
                // Start dataTable update.
                dataTable.BeginLoadData();

                // Process all lines except the last element, because it is not completed yet.
                for (int i = 0; i < (serialLines.Length - 1); i++)
                {
                    // Process the received line of text.
                    newDataReceived |= ProcessReceivedLine(serialLines[i], ref bbCodeBase, ref dataTable);

                    // Remove the old lines to limit the data table size.
                    DeleteOldLines(ref dataTable, maxDataRows);
                }

                // Assign the unused (last) element to the buffer.
                serialData = serialLines[serialLines.Length - 1];
            }

            // If new data received and processed successfully.
            if (newDataReceived)
            {
                // Conclude dataTable update.
                dataTable.EndLoadData();

                // Log the time spent here.
                TimeSpan timeSpan = DateTime.Now.Subtract(timeEntry);
                Console.WriteLine("GetSerialData time[ms]: " + timeSpan.TotalMilliseconds + " Rows: " + dataTable.Rows.Count);
            }

            // Return.
            return newDataReceived;
        }
    }
}
