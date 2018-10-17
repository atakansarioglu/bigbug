/**
 * @file      MainForm.cs
 * @author    Atakan S.
 * @date      01/01/2018
 * @version   1.0
 * @brief     Tool for testing BigBug by providing virtual input.
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
using System.IO.Ports;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        int textLen;

        public MainForm()
        {
            InitializeComponent();
            textLen = text.TextLength;
            string[] ports = SerialPort.GetPortNames();
            if(ports.Length > 0) port.Text = ports[0];
        }

        private void connect_CheckedChanged(object sender, EventArgs e)
        {
            if (connect.Checked)
            {
                try
                {
                    sp = new SerialPort(port.Text, int.Parse(baud.Text), (parity.CheckState == CheckState.Unchecked ? Parity.None : (parity.CheckState == CheckState.Checked ? Parity.Odd : Parity.Even)));
                    sp.WriteTimeout = 3000;
                    sp.Open();

                    if (sp.IsOpen)
                    {
                        text.Enabled = true;
                        repeat.Enabled = true;
                    }
                }
                finally { }
            }else
            {
                if(sp != null)
                {
                    sp.Close();
                }

                text.Enabled = false;
                repeat.Enabled = false;
            }
        }

        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        private void text_TextChanged(object sender, EventArgs e)
        {
            if (auto.Checked)
            {
                int addedLength = text.TextLength - textLen;

                if (addedLength > 0)
                {
                    string character = text.Text.Substring(text.TextLength - addedLength, addedLength);
                    if (sp != null)
                    {
                        sp.Write(character);
                        textLen = text.TextLength;
                    }
                }
                else
                {
                    textLen = text.TextLength;
                }
            }
        }

        private void clear_Click(object sender, EventArgs e)
        {
            text.Text = "";
            textLen = 0;
        }

        private void repeat_Click(object sender, EventArgs e)
        {
            text.Enabled = false;
            count.Enabled = false;
            for (int i = 0; i < count.Value; i++)
            {
                try
                {
                    sp.Write(text.Text);
                }catch(Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    break;
                }
            }
            text.Enabled = true;
            count.Enabled = true;
        }

        private void auto_CheckedChanged(object sender, EventArgs e)
        {
            if (auto.Checked)
            {
                textLen = text.TextLength;
            }
        }

        private void text_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }
    }
}
