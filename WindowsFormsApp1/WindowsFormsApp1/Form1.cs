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
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int i = 0;
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

            //  新增----------------------------------------------
            chart1.Series[0].Points.AddXY(i++, value); //繪點指令

            if (value >= 0 && value <= 100)
            {
                pictureBox1.BackgroundImage = Image.FromFile("picture//Green.png");
            }
            else if (value > 100 && value <= 200)
            {
                pictureBox1.BackgroundImage = Image.FromFile("picture//Yellow.png");
            }
            else
            {
                pictureBox1.BackgroundImage = Image.FromFile("picture//Red.png");
            }

            //    //Format
            //Format_Q12_4  = 0.0625;
            //Format_UQ8_8  = 0.00390625;
            //Format_UQ16_0 = 1;

            //modbus_data[6] = Convert.ToByte(crc_chk(modbus_data, 5) % 256);  //CRC H
            //modbus_data[7] = Convert.ToByte(crc_chk(modbus_data, 5) / 256);  //CRC L
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //開啟timer
            timer1.Enabled = true;
            richTextBox2.Text = "開啟計時";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //關閉timer
            i = 0;
            richTextBox2.Text = "關閉計時";
            serialPort1.DiscardInBuffer();
            timer1.Enabled = false;
            chart1.Series[0].Points.Clear();
        }
        private int crc_chk(byte[] data, int p)
        {
            int j;
            int reg_crc = 0xFFFF;
            for (int i = 0; i < p; i++)
            {
                reg_crc ^= data[i];
                for (j = 0; j < 8; j++)
                {
                    int aa = reg_crc & 0x01;
                    if (aa == 1)
                    {
                        reg_crc = (reg_crc >> 1) ^ 0xA001;
                    }
                    else
                    {
                        reg_crc = reg_crc >> 1;
                    }
                }
            }
            ushort reg1 = Convert.ToUInt16(reg_crc);
            return reg1;
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
