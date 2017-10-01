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

namespace SerialPortAnalyzer
{
    public partial class Form1 : Form
    {
        static SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { 
            comboBox_PortName.Items.AddRange(SerialPort.GetPortNames());
            comboBox_PortName.SelectedIndex = 0;

            comboBox_DataBits.Items.AddRange(new object[] { 5, 6, 7, 8 });
            comboBox_DataBits.SelectedIndex = 3;

            comboBox_BaudRate.Items.AddRange(new object[]
            {
                1200,2400,4800,9600,
                19200,38400,57600,115200,
                230400,460800
            });
            comboBox_BaudRate.SelectedIndex = 7;

            comboBox_Parity.Items.AddRange(new object[]
            {
                Parity.None,
                Parity.Odd,
                Parity.Even,
                Parity.Mark,
                Parity.Space
            });
            comboBox_Parity.SelectedIndex = 0;

            comboBox_StopBits.Items.AddRange(new object[]
            {
                //StopBits.None,
                StopBits.One,
                StopBits.Two,
                StopBits.OnePointFive
            });
            comboBox_StopBits.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string portName = (string)comboBox_PortName.SelectedItem;
            int baudRate = (int)comboBox_BaudRate.SelectedItem;
            Parity parity = (Parity)comboBox_Parity.SelectedItem;
            int dataBits = (int)comboBox_DataBits.SelectedItem;
            StopBits stopBit = (StopBits)comboBox_StopBits.SelectedItem;

            serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBit);

            if (serialPort != null)
                serialPort.Open();

            if (!serialPort.IsOpen)
            {
                label_portState.Text = "Порт занят или отсутствует!";
            }
            else
            {
                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                label_portState.Text = "Порт открыт!";
                

                label_portState.Update();
            }
        }

        private void DataReceivedHandler(
                       object sender,
                       SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[2];
            (sender as SerialPort).Read(buffer,0,2);
            int indata;
            indata = buffer[0];
            indata |= buffer[1] << 8;
            Console.WriteLine("Data Received:");
            Console.Write(indata);
            //richTextBox1.Text += indata;
            //if (richTextBox1.TextLength == 10000)
            //    richTextBox1.ResetText();
            richTextBox1.Invoke(new Action(() => { richTextBox1.Text += (indata.ToString() + ' '); }));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.DataReceived -= DataReceivedHandler;
                serialPort.Close();
            }
                
        }
    }
}
