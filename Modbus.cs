using System;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
//using System.IO.Ports.SerialPort;
namespace zap
{
    class Modbus: IDisposable
    {
        private SerialPort sp = new SerialPort();
        private string _modbusStatus;
        private const string NewLine = "\r\n";
        private bool disposed = false;
        private const int BufferSize = 16777216;
        private readonly object m_syncRoot = new object();
        private int timeOut = 40;
        //private SerialPort m_serialPort;
        private static readonly AsyncCallback m_endReadCallback = new AsyncCallback(EndRead);
        private static readonly AsyncCallback m_endWriteCallback = new AsyncCallback(EndWrite);
        private class AsyncState
        {
            public readonly SerialPort SerialPort;
            public readonly byte[] Buffer;
            public readonly MemoryStream Stream = new MemoryStream();

            public AsyncState(SerialPort serialPort)
            {
                this.SerialPort = serialPort;
                this.Buffer = new byte[BufferSize];
            }

            public AsyncState(SerialPort serialPort, byte[] buffer)
                : this(serialPort)
            {
                this.Buffer = buffer;
            }

            public byte[] Data
            {
                get;
                set;
            }
        }
        public string modbusStatus
        {
            get { return _modbusStatus; }
            set { _modbusStatus = value; }
        }
        public int Timeout
        {
            get { return timeOut; }
            private set { timeOut = value; }
        }

        #region Constructor / Deconstructor
        public Modbus()
        {
            //sp.NewLine = NewLine;
        }
        ~Modbus()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                if (disposing)
                {
                    sp?.Dispose();
                    sp = null;
                }
                // Note disposing has been done.
                disposed = true;
            }
        }
        #endregion

        #region Open / Close / Send / Read Procedures
        public bool Open(string portName, int baudRate, int databits, Parity parity, StopBits stopBits)
        {
            //Ensure port isn't already opened:
            if (!sp.IsOpen)
            {
                lock (this.m_syncRoot)
                {
                    //Assign desired settings to the serial port:
                    sp.PortName = portName;
                    sp.BaudRate = baudRate;
                    sp.DataBits = databits;
                    sp.Parity = parity;
                    sp.StopBits = stopBits;
                    //These timeouts are default and cannot be editted through the class at this point:
                    sp.ReadTimeout = 1000;
                    sp.WriteTimeout = 1000;
                    //sp.ReceivedBytesThreshold = 1;
                    //sp.RtsEnable = false;

                    //sp.RtsEnable = true;
                    //sp.WriteBufferSize = 1;
                    try
                    {
                        sp.Open();
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error opening " + portName + ": " + err.Message;
                        return false;
                    }
                    modbusStatus = portName + " opened successfully";
                    return true;
                }
            }
            else
            {
                modbusStatus = portName + " already opened";
                return false;
            }
        }
        public bool Close()
        {
            //Ensure port is opened before attempting to close:
            if (sp.IsOpen)
            {
                lock (this.m_syncRoot)
                {
                    try
                    {
                        sp.Close();
                    }
                    catch (Exception err)
                    {
                        modbusStatus = "Error closing " + sp.PortName + ": " + err.Message;
                        return false;
                    }
                    modbusStatus = sp.PortName + " closed successfully";
                    return true;
                }
            }
            else
            {
                modbusStatus = sp.PortName + " is not open";
                return false;
            }
        }
        public byte[] Send(byte[] command, bool waitForResponse, int byteread)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
                
            }

            if (sp.IsOpen)
            {
                this.sp.DiscardOutBuffer();
                this.sp.DiscardInBuffer();

                var state = new AsyncState(this.sp, command);
                this.sp.BaseStream.BeginWrite(state.Buffer, 0, state.Buffer.Length, m_endWriteCallback, state);

                if (waitForResponse)
                {
                    state = new AsyncState(this.sp);
                    lock (state)
                    {
                        this.sp.BaseStream.BeginRead(state.Buffer, 0, byteread, m_endReadCallback, state);
                        if (Monitor.Wait(state, this.Timeout))
                        {
                            return state.Data;
                        }

                        throw new TimeoutException();
                    }
                }

                return null;
            }
            else
                throw new IOException("Serial port is closed.");
        }
        public byte[] Read(int byteread)
        {
            if (this.sp.IsOpen)
            {
                //this.m_serialPort.DiscardOutBuffer();
                //this.m_serialPort.DiscardInBuffer();

                var state = new AsyncState(this.sp);
                state = new AsyncState(this.sp);
                lock (state)
                {
                    this.sp.BaseStream.BeginRead(state.Buffer, 0, byteread, m_endReadCallback, state);
                    if (Monitor.Wait(state, this.Timeout))
                    {
                        return state.Data;
                    }

                    throw new TimeoutException();
                }

            }
            else
            {
                throw new IOException("Serial port is closed.");
            }
            //return null;
        }

        private static void EndRead(IAsyncResult result)
        {
            var state = result.AsyncState as AsyncState;

            lock (state)
            {
                try
                {
                    if (state.SerialPort.IsOpen)
                    {
                        int readed = state.SerialPort.BaseStream.EndRead(result);
                        state.SerialPort.BaseStream.Flush();
                        state.Stream.Write(state.Buffer, 0, readed);
                        state.Stream.Flush();
                        state.Stream.Seek(0, SeekOrigin.Begin);

                        var buffer = new byte[state.Stream.Length];
                        state.Stream.Read(buffer, 0, buffer.Length);

                        var data = (buffer);
                    }
                }
                catch
                {
                    Monitor.Pulse(state);
                }
            }
        }

        private static void EndWrite(IAsyncResult result)
        {
            var state = result.AsyncState as AsyncState;

            lock (state)
            {
                try
                {
                    if (state.SerialPort.IsOpen)
                    {
                        state.SerialPort.BaseStream.EndWrite(result);
                        state.SerialPort.BaseStream.Flush();
                    }
                }
                catch
                {
                }
            }
        }
        #endregion

        #region CRC Computation
        private void GetCRC(byte[] message, ref byte[] CRC)
        {
            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
            //return the CRC values:

            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }
        #endregion

        #region Build Message
        private void BuildMessage(byte address, byte type, ushort start, ushort registers, ref byte[] message)
        {
            //Array to receive CRC bytes:
            byte[] CRC = new byte[2];

            message[0] = address;
            message[1] = type;
            message[2] = (byte)(start >> 8);
            message[3] = (byte)start;
            message[4] = (byte)(registers >> 8);
            message[5] = (byte)registers;

            GetCRC(message, ref CRC);
            message[message.Length - 2] = CRC[0];
            message[message.Length - 1] = CRC[1];
        }
        #endregion

        #region Check Response
        private bool CheckResponse(byte[] response)
        {
            //Perform a basic CRC check:
            byte[] CRC = new byte[2];
            GetCRC(response, ref CRC);
            if (CRC[0] == response[response.Length - 2] && CRC[1] == response[response.Length - 1])
                return true;
            else
                return false;
        }
        #endregion

        #region Get Response
        private void GetResponse(ref byte[] response)
        {
            //There is a bug in .Net 2.0 DataReceived Event that prevents people from using this
            //event as an interrupt to handle data (it doesn't fire all of the time).  Therefore
            //we have to use the ReadByte command for a fixed length as it's been shown to be reliable.
            for (int i = 0; i < response.Length; i++)
            {
                response[i] = (byte)(sp.ReadByte());
            }
        }
        #endregion

        #region Function 16 - Write Multiple Registers
        //public bool SendFc16(byte address, ushort start, ushort registers, short[] values)
        public bool WriteMultipleRegisters(byte address, ushort start, ushort registers, ref ushort[] values)
        {
            //Ensure port is open:
            if (sp.IsOpen)
            {
                //Clear in/out buffers:
                sp.DiscardOutBuffer();
                sp.DiscardInBuffer();
                //Message is 1 addr + 1 fcn + 2 start + 2 reg + 1 count + 2 * reg vals + 2 CRC
                byte[] message = new byte[9 + 2 * registers];
                //Function 16 response is fixed at 8 bytes
                byte[] response = new byte[8];

                //Add bytecount to message:
                message[6] = (byte)(registers * 2);
                //Put write values into message prior to sending:
                for (int i = 0; i < registers; i++)
                {
                    message[7 + 2 * i] = (byte)(values[i] >> 8);
                    message[8 + 2 * i] = (byte)(values[i]);
                }
                //Build outgoing message:
                BuildMessage(address, (byte)16, start, registers, ref message);

                //Send Modbus message to Serial Port:
                try
                {
                    //DateTime t1 = DateTime.Now;
                    
                    sp.Write(message, 0, message.Length);
                    //DateTime t2 = DateTime.Now;
                    Thread.Sleep(1);
                    GetResponse(ref response);
                    //DateTime t3 = DateTime.Now;
                    //Console.WriteLine("Send:");
                    //Console.WriteLine("T1: {0}", t1.ToString("hh:mm:ss.ffff"));
                    //Console.WriteLine("T2: {0}", t2.ToString("hh:mm:ss.ffff"));
                    //Console.WriteLine("T3: {0}", t3.ToString("hh:mm:ss.ffff"));
                    //Console.WriteLine("T1: {0}", t1.ToString("hh:mm:ss.ffff"));
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in write event: " + err.Message;
                    return false;
                }
                //Evaluate message:
                if (CheckResponse(response))
                {
                    modbusStatus = "Write successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }
        }
        #endregion

        #region Function 3 - Read Registers
        //public bool SendFc3(byte address, ushort start, ushort registers, ref short[] values)
        public bool ReadInputRegisters(byte address, ushort start, ushort registers, ref ushort[] values)
        {
            //Ensure port is open:
            if (sp.IsOpen)
            {
                //Clear in/out buffers:
                sp.DiscardOutBuffer();
                sp.DiscardInBuffer();
                //Function 3 request is always 8 bytes:
                byte[] message = new byte[8];
                //Function 3 response buffer:
                byte[] response = new byte[5 + 2 * registers];
                //Build outgoing modbus message:
                BuildMessage(address, (byte)3, start, registers, ref message);
                //Send modbus message to Serial Port:
                try
                {
                    sp.Write(message, 0, message.Length);
                    
                    GetResponse(ref response);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in read event: " + err.Message;
                    return false;
                }
                //Evaluate message:
                if (CheckResponse(response))
                {
                    //Return requested register values:
                    for (int i = 0; i < (response.Length - 5) / 2; i++)
                    {
                        values[i] = response[2 * i + 3];
                        values[i] <<= 8;
                        values[i] += response[2 * i + 4];
                    }
                    modbusStatus = "Read successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }

        }
        public bool ReadInputRegistersX(byte address, ushort start, ushort registers, ref ushort[] values)
        {
            //Ensure port is open:
            if (sp.IsOpen)
            {
                //Clear in/out buffers:
                sp.DiscardOutBuffer();
                sp.DiscardInBuffer();
                //Function 3 request is always 8 bytes:
                byte[] message = new byte[8];
                //Function 3 response buffer:
                byte[] response = new byte[5 + 2 * registers];
                //Build outgoing modbus message:
                BuildMessage(address, (byte)3, start, registers, ref message);
                //Send modbus message to Serial Port:
                try
                {
                    response= this.Send(message, true, response.Length);

                    //GetResponse(ref response);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in read event: " + err.Message;
                    return false;
                }
                //Evaluate message:
                if (CheckResponse(response))
                {
                    //Return requested register values:
                    for (int i = 0; i < (response.Length - 5) / 2; i++)
                    {
                        values[i] = response[2 * i + 3];
                        values[i] <<= 8;
                        values[i] += response[2 * i + 4];
                    }
                    modbusStatus = "Read successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }

        }
        #endregion

    }
}
