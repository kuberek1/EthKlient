using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PPSKLab1
{
    class Connection
    {
        
        public TcpClient startServerConnection(Int32 port) 
        {
            TcpClient client = new TcpClient();
            client.Connect(IPAddress.Parse("192.168.1.37"), port);
            return client;
        }

        public NetworkStream streamServer(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            return stream;
        }

        public BinaryWriter binaryWriterServer(NetworkStream stream)
        {
            BinaryWriter Swrite = new BinaryWriter(stream);
            return Swrite;
        }
        public BinaryReader binaryReaderServer(NetworkStream stream)
        {
            BinaryReader Sread = new BinaryReader(stream);
            return Sread;
        }
       

        public void stopServerConnection(TcpClient client, NetworkStream stream)
        {

            stream.Close();
            client.Close();
        }
    }
}
