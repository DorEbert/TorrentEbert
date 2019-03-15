using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Models
{
    public class TaskPackage
    {
        public int TaskID { get; set; }
        public int peers { get; }
        public double size { get; }
        public string chosenFileName { get; set; }
        public string IPAdress { get; set; }
        public int Port { get; set; }
        public Task<byte[]> Task { get; set; }
        public byte[] Bytes { get; set; }

        public TaskPackage(int id,int peers,int port, double size,string chosenFileName,string ipAdress)
        {
            TaskID = id;
            this.peers = peers;
            this.size = size;
            this.chosenFileName = chosenFileName;
            IPAdress = ipAdress;
            Port = port;

            double from = TaskID / (double) peers;
            int final_from = Convert.ToInt32(from * size);
            double to = (TaskID + 1) / (double) peers;
            int final_to = Convert.ToInt32(to * size);

            Task = new Task<byte[]>(() => { return  Connect(final_from, final_to, chosenFileName, IPAdress); });
           
        }

        private byte[] Connect(int from, int to, string fileName, string ipAddress)
        {

            String server = ipAddress;
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                TcpClient client = new TcpClient(server, Port);
                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] dataFrom = BitConverter.GetBytes(from);
                Byte[] dataTo = BitConverter.GetBytes(to);
                Byte[] dataFileName = System.Text.Encoding.ASCII.GetBytes(fileName);
                Byte[] data = new Byte[dataFrom.Length + dataTo.Length + dataFileName.Length];
                Array.Copy(dataFrom, data, dataFrom.Length);
                Array.Copy(dataTo, 0, data, dataTo.Length, dataTo.Length);
                Array.Copy(dataFileName, 0, data, dataFrom.Length + dataTo.Length, dataFileName.Length);

                // Get a client stream for reading and writing.
                // Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();
                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);
                Bytes = new byte[Math.Abs(to - from)];
                stream.Read(Bytes, 0, Bytes.Length);
                Thread.Sleep(3000);
                // Close everything.
                stream.Close();
                client.Close();
                return Bytes;

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            return null;
        }
    }
}
