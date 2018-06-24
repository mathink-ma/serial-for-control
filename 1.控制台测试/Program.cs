using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace serialfromcontrol
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("计算机存在以下端口:");
            foreach (string ports in SerialPort.GetPortNames())
            {
                Console.WriteLine(ports);
            }
            Console.WriteLine("请输入端口(数字)");
            string i = Console.ReadLine();
            int a = 0;
            while (int.TryParse(i, out a)==false)
            {
                Console.WriteLine("请输入端口(数字)");
                i = Console.ReadLine();
            }
            SerialPort myserial = new SerialPort("COM"+i);
            Console.WriteLine("请输入波特率");
            myserial.BaudRate =Convert.ToInt32(Console.ReadLine());
            myserial.Parity = Parity.None;
            myserial.DataBits = 8;
            myserial.Handshake = Handshake.None;
            myserial.DataReceived += new SerialDataReceivedEventHandler(datahander);
            myserial.Open();
            Console.WriteLine("连接成功");
            Console.WriteLine();
            for(int li=0; li<=1;li++)
            {
                string str1;
                str1 = Console.ReadLine();
                byte[] data = Encoding.Default.GetBytes(str1);
                myserial.Write(data, 0, data.Length);
                Console.WriteLine("发送内容：" + str1);
                li = 0;

               
            }
        }
        private static void datahander(object sender, SerialDataReceivedEventArgs e)

        {
            SerialPort sp = (SerialPort)sender;
            sp.Encoding = Encoding.GetEncoding("GB2312");
            string indata = sp.ReadExisting();
            Console.WriteLine("data rec:"+indata);
        }

       




}
}
