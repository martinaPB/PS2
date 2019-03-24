using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_PLC
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        static BackgroundWorker senderWorker;
        static Simulator.Simulator process;

        static void Main(string[] args)
        {
            Console.WriteLine("Press enter to start...");
            Console.ReadLine();

            process = new Simulator.Simulator();


            senderWorker = new BackgroundWorker();
            senderWorker.WorkerReportsProgress = true;
            senderWorker.DoWork += SenderWorker_DoWork;
            senderWorker.ProgressChanged += SenderWorker_ProgressChanged;
            senderWorker.RunWorkerAsync();

            Thread listenThread = new Thread(Listen);
            listenThread.Start();
        }

        private static void SenderWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine(e.UserState);
        }

        private static void SenderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Sender dataSender = new Sender(senderWorker, process);
            dataSender.Send();
        }

        private static void Listen()
        {
            Listener listener = new Listener(process);
            listener.Listen();
        }

    }
}
