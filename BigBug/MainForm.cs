﻿/**
 * @file      MainForm.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     BigBug debug helper for Embedded Projects.
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
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BigBug
{
    public partial class MainForm : Form
    {
        BBCodeBase bbCodeBase = new BBCodeBase(); ///< Database for all known (scanned) BBCode descriptors.
        DataTable dataTable = new DataTable(); ///< Table of received messages that is shown to the user.
        SerialReceiver serialComm; ///< Serial communication methods.
        int maxDataRows = 100000; ///< Number of maximum rows that will be stored, olders will be deleted.
        string projectName; ///< Name of the opened project.
        string selectedPort; ///< Name of the selected port.

        /**
         * @brief Constructor that loads the interface, sets the default view and loads some settings.
         *        Then it initializes the serial communicator.
         */
        public MainForm()
        {
            // Initialize components and set to default view.
            InitializeComponent();
            SetVisuals(VisualStates.Default);

            // Restore Baud rate and MaxDataRows from the settings.
            textBaud.Text = Properties.Settings.Default.Baud;
            maxDataRows = Properties.Settings.Default.MaxDataRows;
            selectedPort = Properties.Settings.Default.Port;

            // Initialize serial communicator in order to fill dataTable with received data.
            serialComm = new SerialReceiver(ref dataTable);
        }

        /**
         * @brief New Project button event, loads the Recent project path from the settings
         *        and shown folder browser.
         * @param sender
         * @param e
         */
        private void buttonNewProject_Click(object sender, EventArgs e)
        {
            // Load Recent opened folder and set it to the folder browser initial path.
            browserNewProject.SelectedPath = Properties.Settings.Default.RecentProject;

            // Show browser.
            if (browserNewProject.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Get supported file extensions.
                string supportedFileExtensionsString = Properties.Settings.Default.FileExtensions;
                string[] supportedFileExtensions = supportedFileExtensionsString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Use ProjectScanner to scan all files.
                if (!ProjectScanner.Scan(browserNewProject.SelectedPath, supportedFileExtensions, ref bbCodeBase))
                {
                    MessageBox.Show("No BBCode Descriptor has been found under the selected path.");
                }

                // Remember the project name including the number of recognized BBCode entries.
                projectName = Path.GetFileName(browserNewProject.SelectedPath) + " #" + bbCodeBase.GetCodeCount();

                // Set visuals for ProjectOpen state.
                SetVisuals(GetVisuals() | VisualStates.ProjectOpen);

                // Save teh successfuly opened project path.
                Properties.Settings.Default.RecentProject = browserNewProject.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        /**
         * @brief Port open and closing button handler. On successful open, saves baud and port to settings.
         * @param sender
         * @param e
         */
        private void buttonPortOpenClose_Click(object sender, EventArgs e)
        {
            // This button toggles the serial port connection. Check the previous state.
            if (serialComm.IsOpen())
            {
                // Close the port if it is already open.
                serialComm.ClosePort();

                // Clear SerialEnabled bit.
                SetVisuals(GetVisuals() & ~VisualStates.SerialEnabled);
            }
            else
            {
                // If port is not open, try to open with user provided port-name and baud-rate.
                if ((comboPorts.SelectedItem != null) && serialComm.OpenPort(comboPorts.SelectedItem.ToString(), textBaud.Text, false, false))
                {
                    // Save the successfull settings.
                    Properties.Settings.Default.Baud = textBaud.Text.ToString();
                    Properties.Settings.Default.Port = comboPorts.SelectedItem.ToString();
                    Properties.Settings.Default.Save();
                    selectedPort = comboPorts.SelectedItem.ToString();

                    // Set SerialEnabled bit and update visuals.
                    SetVisuals(GetVisuals() | VisualStates.SerialEnabled);
                }
                else
                {
                    // Clear SerialEnabled bit and update usuals.
                    SetVisuals(GetVisuals() & ~VisualStates.SerialEnabled);
                }
            }
        }

        /**
         * @brief Timer for periodically updating serial port selection combo box.
         *        Runs immediately after form loading, then runs every second.
         * @param sender
         * @param e
         */
        private void timerSerialPortListUpdate_Tick(object sender, EventArgs e)
        {
            // Check if combobox is opened by user.
            if (!comboPorts.DroppedDown)
            {
                // Clear all elements of combobox.
                comboPorts.Items.Clear();

                // Get available serial port list.
                string[] ports = SerialPort.GetPortNames();

                // Iterate throughout the ports.
                for (int i = 0; i < ports.Length; i++)
                {
                    // Check if combobox already has this port.
                    if (comboPorts.Items.IndexOf(ports[i]) < 0)
                    {
                        // Add the port to the combobox.
                        comboPorts.Items.Add(ports[i]);
                    }
                }

                // Restore the last opened.
                int index = comboPorts.Items.IndexOf(selectedPort);
                if (index >= 0)
                {
                    comboPorts.SelectedIndex = index;
                }
            }

            // Set self timeout.
            ((Timer)sender).Interval = 1000;
        }

        /**
         * @brief Timer for periodically serial port data receiption.
         * @param sender
         * @param e
         */
        private void serialCommListenerTimer_Tick(object sender, EventArgs e)
        {
            // Call serial data receiver.
            serialComm.GetSerialData(ref bbCodeBase, maxDataRows);

            // Set visuals for DataUpdate state.
            SetVisuals(GetVisuals() | VisualStates.DataUpdate);
        }

        /**
         * @brief Clears the data table deleting all the received messages.
         * @param sender
         * @param e
         */
        private void buttonClear_Click(object sender, EventArgs e)
        {
            // Stop the receiption first.
            timerSerialCommListener.Enabled = false;

            // Clear all messages.
            dataTable.Rows.Clear();

            // Re-enable the receiption.
            timerSerialCommListener.Enabled = true;
        }

        /**
         * @brief Saves all received messages to file.
         * @param sender
         * @param e
         */
        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFile.FileName = "";
            saveFile.InitialDirectory = Properties.Settings.Default.SaveDir;

            if ((saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK) && !timerSerialCommListener.Enabled)
            {
                FileStream fs = new FileStream(saveFile.FileName, FileMode.Create, FileAccess.ReadWrite);
                byte[] bytes;
                foreach (DataRow dr in dataTable.Rows)
                {
                    bytes = Encoding.ASCII.GetBytes(dr[0] + "\t" + dr[1] + "\r\n");
                    fs.Write(bytes, 0, bytes.Length);
                }
                fs.Close();

                //-- Save.
                Properties.Settings.Default.SaveDir = Path.GetDirectoryName(saveFile.FileName);
                Properties.Settings.Default.Save();
            }
        }

        /**
         * @brief Opens a single-file project from the selected location and scans all BBCode descriptors to fill the BBCodeBase.
         * @param sender
         * @param e
         */
        private void buttonNewSingleFileProject_Click(object sender, EventArgs e)
        {
            // Load Recent opened folder and set it to the folder browser initial path.
            browserOpenSingleFile.FileName = Properties.Settings.Default.RecentSingleFile;

            // Show browser.
            if (browserOpenSingleFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Use ProjectScanner to scan the selected file.
                if (!ProjectScanner.Scan(browserOpenSingleFile.FileName, new string[] { }, ref bbCodeBase))
                {
                    MessageBox.Show("No BBCode Descriptor has been found under the selected path.");
                }

                // Remember the project name including the number of recognized BBCode entries.
                projectName = Path.GetFileName(browserOpenSingleFile.FileName) + " #" + bbCodeBase.GetCodeCount();

                // Set visuals for ProjectOpen state.
                SetVisuals(GetVisuals() | VisualStates.ProjectOpen);

                // Save teh successfuly opened project path.
                Properties.Settings.Default.RecentSingleFile = browserOpenSingleFile.FileName;
                Properties.Settings.Default.Save();
            }
        }

        /**
         * @brief Event handler for text change of filter box.
         * @param sender
         * @param e
         */
        private void textFilter_TextChanged(object sender, EventArgs e)
        {
            // Set data filter to the data table.
            DataFilter.Set(ref dataTable, textFilter.Text);

            // Set FilterApplied flag.
            SetVisuals(GetVisuals());
        }

        /**
         * @brief Event handler for filter text clear button.
         * @param sender
         * @param e
         */
        private void labelClearFilter_Click(object sender, EventArgs e)
        {
            textFilter.Text = "";
        }

        /**
         * @brief Event handler for serial port selection changed.
         * @param sender
         * @param e
         */
        private void comboPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the selection.
            selectedPort = comboPorts.SelectedItem.ToString();
        }
    }
}
