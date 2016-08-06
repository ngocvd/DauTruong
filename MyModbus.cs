using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.Threading;
//using Modbus.Device;
//using Modbus.Serial;

namespace zap
{
    //public class StateObjClass
    //{
    //    // Used to hold parameters for calls to TimerTask.
    //    public int SomeValue;
    //    public System.Timers.Timer TimerReference;
    //    public bool TimerCanceled;
    //}
    public class MyTime
    {
        public double TimeToDouble
        { get; set; }
        public string TimeString
        { get; set; }
        public string[] IDs;
        public MyTime()
        {
            IDs = new string[4];
            TimeToDouble = 0.0;
            TimeString = "";
        }
    }
    class MyModbus
    {
        private Regex regex = new Regex(@"(\d+):(\d{2}):(\d{2})\.(\d+)");
        private Regex regex01 = new Regex(@"\[(\d+:\d{2}:\d{2}\.\d+)\]([\[\[ID:\d+\][\[\d+\]]+\]]+)");
        private Regex regex1 = new Regex(@"\[\[ID:(\d+)\]([\[\d+\]]+)\]");
        private Regex regex2 = new Regex(@"\[(\d+)\]");
        private Modbus master = new Modbus();
        public List<byte> IDList = new List<byte>();
        //private SerialPortAdapter adapter = null;
        //private IModbusSerialMaster master = null;
        private char[] delimiterChars = { ',', ';' };
        public bool Checked
        {
            get; set;
        }
        public MyModbus()
        {
            IsDead = false;
            Checked = false;
            master.Open(Properties.Settings.Default.SerialPort,
                Properties.Settings.Default.BaudRate,
                Properties.Settings.Default.DataBits,
                Properties.Settings.Default.Parity,
                Properties.Settings.Default.StopBits
                );
            try
            {
                string[] numbers = Properties.Settings.Default.IDs.Split(delimiterChars);
                //IDList.Clear();
                foreach (string ID in numbers)
                {
                    IDList.Add(Convert.ToByte(ID.Trim()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }
        private bool disposed;
        public bool IsDead { get; set; }

        /// <summary>
        /// Destructor
        /// </summary>
        ~MyModbus()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// The dispose method that implements IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The virtual dispose method that allows
        /// classes inherithed from this one to dispose their resources.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    IsDead = true;
                    master.Dispose();
                    master = null;
                }

                // Dispose unmanaged resources here.
            }

            disposed = true;
        }
        public string CheckId()
        {
            string[] numbers = Properties.Settings.Default.IDs.Split(delimiterChars);
            string s = "";
            IDList.Clear();
            ushort[] values = new ushort[2];
            ushort k = Convert.ToUInt16("FFFF", 16);
            
            foreach (string ID in numbers)
            {
                byte id = Convert.ToByte(ID.Trim());
                try
                {
                    //bool r = master.SendFc3(id, 0, 1, ref values);
                    values[0] = k;
                    values[1] = k;
                    //if (master.ReadInputRegisters(id, 0, 1, ref values))
                    if (master.WriteMultipleRegisters(id, 0, 2, ref values))
                    {
                        IDList.Add(id);
                        values[0] = 0;
                        values[1] = 0;
                        master.WriteMultipleRegisters(id, 0, 2, ref values);
                    }
                    else
                    {
                        if (s == "")
                            s += id.ToString();
                        else
                            s += "," + id.ToString();
                    }
                }
                catch 
                {
                    if (s == "")
                        s += id.ToString();
                    else
                        s += "," + id.ToString();
                }
                
            }
            Checked = true;
            return s;
        }
        public void LedTurnOnx(string text)
        {
            ushort[] registers = { 1, 3 };
            master.ReadInputRegistersX(1, 0, (ushort)registers.Length, ref registers);
            Thread.Sleep(40);
            master.ReadInputRegistersX(2, 0, (ushort)registers.Length, ref registers);
            //master.ReadInputRegistersX(3, 0, (ushort)registers.Length, ref registers);
            //master.ReadInputRegistersX(4, 0, (ushort)registers.Length, ref registers);
        }
        public void LedTurnOn(MyTime time)
        {
            //Convert all to Hex text.
            //text="[[ID:1][11010111][11111111][11111111][00011111]][[ID:2][11011011][01100111][01011111][11010100]][[ID:3][11111111][11111111][11111111][11111111]][[ID:4][01111000][11101101][11110111][11111111]]"
            //Regex regex1 = new Regex(@"\[\[ID:(\d+)\]([\[\d+\]]+)\]");
            //Regex regex2 = new Regex(@"\[(\d+)\]");
            //int l;
            //Push to Mosbus RTU Exists(x => x.PartId == 1444));
            
            ushort startAddress = 0;
            string text = null;
            int len = 0;
            int pad = 0;
            //short[] values = new short[2];
            for (byte i=0;i<4;i++)
            {
                if (!IDList.Exists(x => x == i + 1))
                    continue;
                text = time.IDs[i];
                len = text.Length;
                pad = len % 4;
                if (pad > 0)
                {
                    pad = 4 - pad;
                    text.PadRight(len + pad, '0');
                    len = text.Length;
                }
                ushort[] registers = new ushort[len/4];
                for (int j = 0; j < len/4; j++)
                {
                   registers[j] = Convert.ToUInt16(text.Substring(j*4, 4), 16);
                }

                master.WriteMultipleRegisters((byte)(i+1), startAddress, (ushort)registers.Length, ref registers);
            }
        }
        public void LedTurnOn(string text)
        {
            //text="[[ID:1][11010111][11111111][11111111][00011111]][[ID:2][11011011][01100111][01011111][11010100]][[ID:3][11111111][11111111][11111111][11111111]][[ID:4][01111000][11101101][11110111][11111111]]"
            //Regex regex1 = new Regex(@"\[\[ID:(\d+)\]([\[\d+\]]+)\]");
            //Regex regex2 = new Regex(@"\[(\d+)\]");
            //int l;
            //Push to Mosbus RTU Exists(x => x.PartId == 1444));

            string last = "00000000";
            ushort startAddress = 0;
            //short[] values = new short[2];
            foreach (Match match in regex1.Matches(text))
            {
                if (match.Groups.Count > 1)
                {
                    byte slaveId = Convert.ToByte(match.Groups[1].Value);
                    MatchCollection m = regex2.Matches(match.Groups[2].Value);
                    int len = (int)Math.Ceiling(m.Count / 2.0);
                    if(IDList.Exists(x=>x== slaveId))
                    {
                        ushort[] registers = new ushort[len];
                        for (int i = 0; i < len; i++)
                        {
                            if (i < len - 1)
                                registers[i] = Convert.ToUInt16(m[i * 2].Groups[1].Value + m[i * 2 + 1].Groups[1].Value, 2);
                            else
                            {
                                if (2 * len != m.Count)
                                {
                                    registers[i] = Convert.ToUInt16(m[i * 2].Groups[1].Value + last, 2);
                                }
                                else
                                    registers[i] = Convert.ToUInt16(m[i * 2].Groups[1].Value + m[i * 2 + 1].Groups[1].Value, 2);
                            }
                        }
                        
                        master.WriteMultipleRegisters(slaveId, startAddress, (ushort)registers.Length, ref registers);
                    }
                    
                }
            }
        }
    }
}
