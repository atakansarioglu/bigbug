/**
 * @file      Visuals.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Deals with the form and switches between visual states.
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

namespace BigBug
{
    public partial class MainForm
    {
        [Flags] public enum VisualStates /// Enumeration to represent differen visual layouts.
        {
            Default = 0x01 << -1,
            ProjectOpen = 0x01 << 0,
            SerialEnabled = 0x01 << 1,
            DataUpdate = 0x01 << 2,
        }

        private VisualStates visualState = VisualStates.Default;///< Keeps the up-to-date visual layout.

        /**
         * @brief Configures the visual layout as requested.
         * @param state Visual state of type VisualStates and is possible to combine different flags.
         */
        private void SetVisuals(VisualStates state)
        {
            // Data update.
            if ((state & VisualStates.DataUpdate) == VisualStates.DataUpdate)
            {
                // Bind dataTable to dataGrid.
                dataGrid.DataSource = dataTable;

                // Set column properties, if columns are created.
                if (dataGrid.Columns.Count == 2)
                {
                    if (dataGrid.Columns[0].MinimumWidth != 128)
                        dataGrid.Columns[0].MinimumWidth = 128;
                    if (dataGrid.Columns[0].FillWeight != 1)
                        dataGrid.Columns[0].FillWeight = 1;
                    if (dataGrid.Columns[1].FillWeight != 10000)
                        dataGrid.Columns[1].FillWeight = 10000;

                    // Scroll to the bottom of the view.
                    if (dataGrid.Rows.Count > 0)
                        dataGrid.FirstDisplayedScrollingRowIndex = dataGrid.Rows.Count - 1;
                }

                // Clear DataUpdate state bit.
                state -= VisualStates.DataUpdate;
            }

            // SerialEnabled.
            if ((state & VisualStates.SerialEnabled) == VisualStates.SerialEnabled)
            {
                // Disable controls to prevent change.
                comboPorts.Enabled = false;
                textBaud.Enabled = false;
                buttonNewProject.Enabled = false;
                buttonNewSingleFileProject.Enabled = false;
                buttonSave.Enabled = false;

                // Enable receiption timer.
                timerSerialCommListener.Enabled = true;

                // Change button name.
                buttonPortOpenClose.Text = "&Port Close";
            }
            else
            {
                // Enable controls.
                comboPorts.Enabled = true;
                textBaud.Enabled = true;
                buttonNewProject.Enabled = true;
                buttonNewSingleFileProject.Enabled = true;
                buttonSave.Enabled = true;
                
                // Disable receiption timer.
                timerSerialCommListener.Enabled = false;

                // Change button name.
                buttonPortOpenClose.Text = "&Port Open";
            }

            // ProjectOpen.
            if ((state & VisualStates.ProjectOpen) == VisualStates.ProjectOpen)
            {
                // Show opened projecst name/description.
                labelProjectName.Text = projectName;
            }
            else
            {
                // Hide opened projecst name/description.
                labelProjectName.Text = "";
            }

            // Filter clear button (label) is only visible when filter text is entered.
            labelClearFilter.Visible = (textFilter.Text != "");

            // Save last state.
            visualState = state;
        }

        /**
         * @brief Returns the last-known visual layout descriptor.
         * @return visual state
         */
        private VisualStates GetVisuals()
        {
            return visualState;
        }
    }
}
