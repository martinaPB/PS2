using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCP_PLC
{
    public class Listener
    {

        Simulator.Simulator _process;

        public Listener(Simulator.Simulator Process)
        {
            _process = Process;
        }

        public void Listen()
        {
            TcpListener server = null;
            try
            {
                Int32 port = 2000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);

                //Se asteapta noi requesturi.
                server.Start();

                //Buffer folosit pentru receptionarea de date
                Byte[] bytes = new Byte[256];

                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");

                    //Se creaza un TCP client nou
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    // se obtine o noua referinta catre streamul folosit pentru comunicare
                    NetworkStream stream = client.GetStream();

                    int i;

                    //se citesc datele prrimite
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        //se afiseaza pe UI (in aplicatia consola) datele primite
                        _process.UpdateState(bytes[1]);

                        Console.WriteLine(string.Format("Received: {0}, {1}", bytes[1].ToString(), bytes[6].ToString()));
                    }

                    //la final se inchide conexiunea
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(string.Format("SocketException: {0}", e.ToString()));
            }
            finally
            {
                //se opreste serverul.
                server.Stop();
            }

            Console.WriteLine("Hit enter to continue...");
        }
    }
}
