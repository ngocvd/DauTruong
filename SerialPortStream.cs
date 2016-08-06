using System;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace zap
{


    public sealed class SerialPortStream : IDisposable
    {
        private const int BufferSize = 1024;
        private readonly object m_syncRoot = new object();
        private SerialPort m_serialPort;
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

        public SerialPortStream(string portName, int baudRate,
            Parity parity, int databits,
            System.IO.Ports.StopBits stopBits, string newLine, int timeout)
        {
            if (portName == null)
            {
                throw new ArgumentNullException("portName");
            }

            if (!Regex.IsMatch(portName, @"^COM[0-9]+$", RegexOptions.IgnoreCase | RegexOptions.Singleline))
            {
                throw new ArgumentException("Incorrect port name.", "portName");
            }

            if (newLine == null)
            {
                throw new ArgumentNullException("newLine");
            }

            if (timeout < -1)
            {
                throw new ArgumentException("Incorrect timeout.", "timeout");
            }
            this.Baudrate = baudRate;
            this.Parity = parity;
            this.Databits = databits;
            this.Stopbits = stopBits;
            this.PortName = portName;
            this.NewLine = newLine;
            this.Timeout = timeout;
        }

        public string PortName
        {
            get;
            private set;
        }

        public string NewLine
        {
            get;
            private set;
        }

        public int Timeout
        {
            get;
            private set;
        }
        
        public int Baudrate
        {
            get;
            private set;
        }
        public System.IO.Ports.Parity Parity
        {
            get;
            private set;
        }
        public int Databits
        {
            get;
            private set;
        }
        public System.IO.Ports.StopBits Stopbits
        {
            get;
            private set;
        }
        public bool IsOpen
        {
            get
            {
                lock (this.m_syncRoot)
                {
                    return (this.m_serialPort != null &&
                            this.m_serialPort.IsOpen);
                }
            }
        }

        public void Open()
        {
            lock (this.m_syncRoot)
            {
                if (this.m_serialPort == null)
                {
                    this.m_serialPort = new SerialPort(
                        //this.PortName, 115200, System.IO.Ports.Parity.None, 
                        //8, System.IO.Ports.StopBits.One
                        )
                    {
                        PortName = this.PortName,
                        BaudRate=this.Baudrate,
                        Parity=this.Parity,
                        DataBits=this.Databits,
                        StopBits=this.Stopbits,
                        NewLine = this.NewLine,
                        ReadTimeout = this.Timeout,
                        ReadBufferSize = BufferSize,
                        WriteTimeout = this.Timeout,
                        WriteBufferSize = BufferSize
                    };
                    this.m_serialPort.Open();
                }
            }
        }

        public void Close()
        {
            lock (this.m_syncRoot)
            {
                if (this.m_serialPort != null)
                {
                    try
                    {
                        this.m_serialPort.Close();
                        this.m_serialPort.Dispose();
                    }
                    catch
                    {
                    }
                }
            }
        }
        public void Dispose()
        {
            this.Close();
        }
        public byte[] Send(byte[] command, bool waitForResponse)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (this.IsOpen)
            {
                this.m_serialPort.DiscardOutBuffer();
                this.m_serialPort.DiscardInBuffer();

                var state = new AsyncState(this.m_serialPort, command);
                this.m_serialPort.BaseStream.BeginWrite(state.Buffer, 0, state.Buffer.Length, m_endWriteCallback, state);

                if (waitForResponse)
                {
                    state = new AsyncState(this.m_serialPort);
                    lock (state)
                    {
                        this.m_serialPort.BaseStream.BeginRead(state.Buffer, 0, state.Buffer.Length, m_endReadCallback, state);
                        if (Monitor.Wait(state, this.Timeout))
                        {
                            return state.Data;
                        }

                        throw new TimeoutException();
                    }
                }

                return null;
            }

            throw new IOException("Serial port is closed.");
        }
        public byte[] Read()
        {
            if (this.IsOpen)
            {
                //this.m_serialPort.DiscardOutBuffer();
                //this.m_serialPort.DiscardInBuffer();

                var state = new AsyncState(this.m_serialPort);
                state = new AsyncState(this.m_serialPort);
                lock (state)
                {
                    this.m_serialPort.BaseStream.BeginRead(state.Buffer, 0, state.Buffer.Length, m_endReadCallback, state);
                    if (Monitor.Wait(state, this.Timeout))
                    {
                        return state.Data;
                    }

                    throw new TimeoutException();
                }
                
            }else
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
    }

}
