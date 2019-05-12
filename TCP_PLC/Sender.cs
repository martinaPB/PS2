using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCP_PLC
{
    public class Sender
    {

        BackgroundWorker _senderWorker;
        Simulator.Simulator _process;


        public Sender(BackgroundWorker senderWorker, Simulator.Simulator Process)
        {
            _senderWorker = senderWorker;
            _process = Process;
        }

        public void Send()
        {
            try
            {
                //Se creaza un TCP client cu adresa de IP si portul 
                TcpClient client = new TcpClient("127.0.0.1", 3000);

                while (true)
                {
                    NetworkStream nwStream = client.GetStream();
                    byte[] bytesToSend = new byte[8];
                    bytesToSend[1] = _process.GetState()[0];

                    //se apeleaza metoda report progress pentru a face update pe UI (adica in aplicatia consola).
                    _senderWorker.ReportProgress(0, string.Format("Sending bytes[1]= {0} and bytes[6] = {1}: ", bytesToSend[1].ToString(), bytesToSend[6].ToString()));
                    nwStream.Write(bytesToSend, 0, bytesToSend.Length);

                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                _senderWorker.ReportProgress(0, ex.ToString());
            }

        }


    }

}
