using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Security.Policy;
using ConvertInBitAndCalculationBcc;

namespace Reader_CE208_S7
{
    public partial class Form1 : Form
    {
        DiscardingParity parityCalculation = new DiscardingParity();

        public Form1()
        {
            InitializeComponent();
            progressBar1.Style = ProgressBarStyle.Marquee;
            
        }

        private void buttonUpdate_Click(object sender, EventArgs e) // Получение списка всех доступный COM-портов.
        {
            string[] ports = SerialPort.GetPortNames();
            comboBoxPorts.Text = "";
            comboBoxPorts.Items.Clear();
            
            if(ports.Length != 0 )
            {
                ReadingOutOfSession.Enabled = true;
                buttonConnect.Enabled = true;
                comboBoxPorts.Items.AddRange(ports);
                comboBoxPorts.SelectedIndex = 0;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if(buttonConnect.Text == "Log in to the session") // Вход в сеанс обмена.
            {
                try
                {
                    serialPort_CE208_S7.PortName = comboBoxPorts.Text;
                    serialPort_CE208_S7.Open();
                    buttonConnect.Text = "Disconnect";
                    comboBoxPorts.Enabled = false;
                    buttonUpdate.Visible = false;
                    Connect_CE208.Visible = true;
                    LogText.Visible = true;
                    buttonClear.Visible = true;
                    ReadingOutOfSession.Visible = false;

                    try
                    {
                        serialPort_CE208_S7.Write(parityCalculation.Converter("/?!<CR><LF>"), 0, parityCalculation.Converter("/?!<CR><LF>").Length);
                        Read();
                    }
                    catch (InvalidOperationException)
                    {

                        return;
                    }

                    try
                    {
                        serialPort_CE208_S7.Write(parityCalculation.Converter("<ACK>051<CR><LF>"), 0, parityCalculation.Converter("<ACK>051<CR><LF>").Length);
                        Read();
                    }
                    catch (InvalidOperationException)
                    {

                        return;
                    }

                    byte[] writer4 = parityCalculation.Converter("<SOH>P1<STX>(777777)<ETX><BCC>");

                    try
                    {
                        serialPort_CE208_S7.Write(parityCalculation.Converter("<SOH>P1<STX>(777777)<ETX><BCC>"), 0, parityCalculation.Converter("<SOH>P1<STX>(777777)<ETX><BCC>").Length);
                        Read();
                    }
                    catch (InvalidOperationException)
                    {

                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Connection error");
                }
            }
            else if(buttonConnect.Text == "Disconnect")
            {
                serialPort_CE208_S7.DiscardInBuffer();
                serialPort_CE208_S7.DiscardOutBuffer();
                serialPort_CE208_S7.BaseStream.Dispose();
                serialPort_CE208_S7.Close();
                Ping();
                buttonConnect.Text = "Log in to the session";
                comboBoxPorts.Enabled = true;
                buttonUpdate.Visible = true;
                Connect_CE208.Visible = false;
                LogText.Visible = false;
                buttonClear.Visible = false;
                ReadingOutOfSession.Visible = true;
                MessageBox.Show("Clearing the buffer");
            }

        }

        private async void Ping() // Задержка на удаление всех данных из буфера COM-порта.
        {
            progressBar1.Visible = true;
            buttonConnect.Enabled = false;
            ReadingOutOfSession.Enabled = false;
            await Task.Delay(8000);
            buttonConnect.Enabled = true;
            ReadingOutOfSession.Enabled = true;
            progressBar1.Visible = false;
        }


        private void Connect_CE208_Click(object sender, EventArgs e) // Чтение данных в сеансе.
        {
            LogText.Clear();

            try
            {
                serialPort_CE208_S7.Write(parityCalculation.Converter("<SOH>R1<STX>VOLTA()<ETX><BCC>"), 0, parityCalculation.Converter("<SOH>R1<STX>VOLTA()<ETX><BCC>").Length);
                ReadData();
            }
            catch (InvalidOperationException)
            {

                return;
            }

            try
            {
                serialPort_CE208_S7.Write(parityCalculation.Converter("<SOH>R1<STX>FREQU()<ETX><BCC>"), 0, parityCalculation.Converter("<SOH>R1<STX>FREQU()<ETX><BCC>").Length);
                ReadData();
            }
            catch (InvalidOperationException)
            {

                return;
            }

            try
            {
                serialPort_CE208_S7.Write(parityCalculation.Converter("<SOH>R1<STX>WATCH()<ETX><BCC>"), 0, parityCalculation.Converter("<SOH>R1<STX>WATCH()<ETX><BCC>").Length);
                ReadData();
            }
            catch (InvalidOperationException)
            {

                return;
            }
        }

        public void Read() // Чтение ответа на запрос.
        {
            byte[] bufferOUT = new byte[200];
            try
            {
                Thread.Sleep(200);
                serialPort_CE208_S7.BaseStream.Read(bufferOUT, 0, bufferOUT.Length - 1);
            }
            catch (TimeoutException)
            {
                MessageBox.Show("Reading error! \n Try again.");
                serialPort_CE208_S7.DiscardInBuffer();
                serialPort_CE208_S7.DiscardOutBuffer();
                serialPort_CE208_S7.Dispose();
                buttonConnect.Text = "Log in to the session";
                comboBoxPorts.Enabled = true;
                buttonUpdate.Visible = true;
                Connect_CE208.Visible = false;
                LogText.Visible = false;
                buttonClear.Visible = false;
                ReadingOutOfSession.Visible = true;
            }
        }

        private void ReadData() // Чтение данных на вывод.
        {
            byte[] bufferOUT = new byte[200];
            byte[] buffer = new byte[200];
            string outBuffer = string.Empty;
            try
            {
                Thread.Sleep(300);
                serialPort_CE208_S7.BaseStream.Read(bufferOUT, 0, bufferOUT.Length - 1);

                buffer = parityCalculation.DiscardingParityScore(bufferOUT);

                outBuffer = Encoding.ASCII.GetString(buffer);
                if (outBuffer.Contains("VOLTA")) // Фильтрация полученных данных.
                {
                    outBuffer = outBuffer.Replace("VOLTA(", "Напряжение сети: ");
                    outBuffer = outBuffer.Replace("(", "Напряжение нейтрали: ");
                    outBuffer = outBuffer.Replace(")", " В ");
                    outBuffer = outBuffer.Remove(outBuffer.IndexOf("\u0003"), 2);
                    outBuffer = outBuffer.Remove(outBuffer.IndexOf("\u0002"), 1);
                    LogText.AppendText(outBuffer);
                }
                else if(outBuffer.Contains("FREQU("))
                {
                    outBuffer = outBuffer.Replace("FREQU(", "Частота сети: ");
                    outBuffer = outBuffer.Replace(")", " Гц ");
                    outBuffer = outBuffer.Remove(outBuffer.IndexOf("\u0002"), 1);
                    outBuffer = outBuffer.Remove(outBuffer.IndexOf("\u0003"), 2);
                    LogText.AppendText(outBuffer);
                }
                else if(outBuffer.Contains("WATCH("))
                {
                    outBuffer = outBuffer.Replace("WATCH(", "Время счётчика: ");                   
                    outBuffer = outBuffer.Remove(outBuffer.IndexOf("\u0002"), 1);
                    outBuffer = outBuffer.Replace(",6.", " Дата: ");
                    outBuffer = outBuffer.Remove(outBuffer.IndexOf(")"));
                    LogText.AppendText(outBuffer);
                }

            }
            catch (TimeoutException)
            {
                MessageBox.Show("Reading error! \n Try again.");
                serialPort_CE208_S7.DiscardInBuffer();
                serialPort_CE208_S7.DiscardOutBuffer();
                serialPort_CE208_S7.Dispose();
                buttonConnect.Text = "Log in to the session";
                comboBoxPorts.Enabled = true;
                buttonUpdate.Visible = true;
                Connect_CE208.Visible = false;
                LogText.Visible = false;
                buttonClear.Visible = false;
                ReadingOutOfSession.Visible = true;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            LogText.Clear();
        } // Очистка выведенных даннах.

        private void ReadingOutOfSession_Click(object sender, EventArgs e) // Чтение данных без входа в сеанс. 
        {
            serialPort_CE208_S7.PortName = comboBoxPorts.Text;
            serialPort_CE208_S7.Open();
            LogText.Visible = true;
            buttonClear.Visible = true;
            LogText.Clear();

            try
            {
                serialPort_CE208_S7.Write(parityCalculation.Converter("/?!<SOH>R1<STX>VOLTA()<ETX><BCC>"), 0, parityCalculation.Converter("/?!<SOH>R1<STX>VOLTA()<ETX><BCC>").Length);
                ReadData();
            }
            catch (InvalidOperationException)
            {

                return;
            }

            try
            {
                serialPort_CE208_S7.Write(parityCalculation.Converter("/?!<SOH>R1<STX>FREQU()<ETX><BCC>"), 0, parityCalculation.Converter("/?!<SOH>R1<STX>FREQU()<ETX><BCC>").Length);
                ReadData();
            }
            catch (InvalidOperationException)
            {

                return;
            }

            try
            {
                serialPort_CE208_S7.Write(parityCalculation.Converter("/?!<SOH>R1<STX>WATCH()<ETX><BCC>"), 0, parityCalculation.Converter("/?!<SOH>R1<STX>WATCH()<ETX><BCC>").Length);
                ReadData();
            }
            catch (InvalidOperationException)
            {

                return;
            }
            serialPort_CE208_S7.Close();
        }
    }
}
