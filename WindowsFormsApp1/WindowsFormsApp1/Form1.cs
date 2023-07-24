using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //按下按鈕與感測器連線
            this.serialPort1.Close();                       //關閉COM連線
            //自動偵測COM
            string[] comPorts = SerialPort.GetPortNames();
            for (int i = 0; i < comPorts.Length;i++)
            {
                richTextBox1.Text = comPorts[i];
            }
            string str = richTextBox1.Text;
            Console.WriteLine(str);
            if (serialPort1.IsOpen == false)
            {
                this.serialPort1.PortName = str; // 設定使用的 PORT
                if (!serialPort1.IsOpen)                   // 檢查 PORT 是否關閉
                    this.serialPort1.Close();
                this.serialPort1.BaudRate = 9600;            // baud rate = 9600
                this.serialPort1.Parity = Parity.None;       // Parity = none
                this.serialPort1.StopBits = StopBits.One;    // stop bits = one
                this.serialPort1.DataBits = 8;               // data bits = 8                
                this.serialPort1.ReadTimeout = 2000;
                this.serialPort1.Encoding = Encoding.Default;
                try
                {
                    serialPort1.Open(); // 打開 PORT
                    if (serialPort1.IsOpen == true)
                    {
                        richTextBox1.Text += "連線成功\r\n";
                    }
                    else
                    {
                        richTextBox1.Text = "NOT CONNECTED\r\n";
                    }
                }
                catch
                {
                    MessageBox.Show("Error: " + "連線失敗", "ERROR");
                }
            }
            else
            {
                this.serialPort1.Close();// 關閉 PORT
                richTextBox1.Text = "離開連線成功";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.serialPort1.Close();                       //關閉COM連線
            if (serialPort1.IsOpen == false)
            {
                this.serialPort1.PortName = textBox1.Text; // 設定使用的 PORT
                if (!serialPort1.IsOpen)                   // 檢查 PORT 是否關閉
                    this.serialPort1.Close();
                this.serialPort1.BaudRate = 9600;            // baud rate = 9600
                this.serialPort1.Parity = Parity.None;       // Parity = none
                this.serialPort1.StopBits = StopBits.One;    // stop bits = one
                this.serialPort1.DataBits = 8;               // data bits = 8                
                this.serialPort1.ReadTimeout = 2000;
                this.serialPort1.Encoding = Encoding.Default;
                try
                {
                    serialPort1.Open(); // 打開 PORT
                    if (serialPort1.IsOpen == true)
                    {
                        richTextBox1.Text += "連線成功\r\n";
                    }
                    else
                    {
                        richTextBox1.Text = "NOT CONNECTED\r\n";
                    }
                }
                catch
                {
                    MessageBox.Show("Error: " + "連線失敗", "ERROR");
                }
            }
            else
            {
                this.serialPort1.Close();// 關閉 PORT
                richTextBox1.Text = "離開連線成功";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
