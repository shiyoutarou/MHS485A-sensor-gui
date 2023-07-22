using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
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
            //這是一個連結資料讀取的程式碼~~
            byte[] modbus_data = new byte[100]; //宣告變數格式
            modbus_data[0] = 0X0A; //sensor address
            modbus_data[1] = 0x04; //Function  code
            modbus_data[2] = 0x00; //starting address  H
            modbus_data[3] = 0X04; //starting address  L  
            modbus_data[4] = 0x00; //quantity  H
            modbus_data[5] = 0x01; //quantity  L
            modbus_data[6] = 0x71; //CRC H
            modbus_data[7] = 0x70; //CRC L
            //資料傳送
            serialPort1.DiscardInBuffer();
            byte[] buffer_databus = new byte[10];  //宣告變數格式
            serialPort1.Write(modbus_data, 0, 8); //寫入資料
            //資料回傳
            Thread.Sleep(100); //Delay 0.1秒.
            byte[] buffer = new byte[20]; //宣告變數格式
            serialPort1.Read(buffer, 0, 7);       //Read COMPORT 
            //資料解析
            double value = 0;
            value = Convert.ToDouble((buffer[3] * 256 + buffer[4]) * 0.0625);
            value = System.Math.Round(value, 2);  //小數點
            serialPort1.DiscardInBuffer();
            label1.Text = Convert.ToString(value);


            //    //Format
            //Format_Q12_4  = 0.0625;
            //Format_UQ8_8  = 0.00390625;
            //Format_UQ16_0 = 1;

            //modbus_data[6] = Convert.ToByte(crc_chk(modbus_data, 5) % 256);  //CRC H
            //modbus_data[7] = Convert.ToByte(crc_chk(modbus_data, 5) / 256);  //CRC L
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
