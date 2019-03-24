using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace psIercan
{
    public partial class Form1 : Form
    {
        NetworkStream stream;
        BackgroundWorker bck;
        TcpClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessage((int)Command.LongDelay | (int)Command.PumpOne | (int)Command.PumpTwo | (int)Command.Started);
            }
            catch (Exception ex)
            {
                richTextBox1.Text = richTextBox1 + ex.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Start Listening";
            bck = new BackgroundWorker();
            bck.WorkerReportsProgress = true;
            bck.ProgressChanged += Bck_ProgressChanged;
            bck.DoWork += Bck_DoWork;
            bck.RunWorkerAsync();

        }

        private void Bck_DoWork(object sender, DoWorkEventArgs e)
        {
            TcpListener server = null;
            try {
                Int32 port = 3000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, port);

                //Start listening for client requests.
                server.Start();

                //Buffer for reading data
                Byte[] bytes = new Byte[8];

                //Enter the listening loop.
                while (true) {
                    bck.ReportProgress(0, "Waiting for a connection... ");

                    //Accept TcpClient
                    TcpClient client = server.AcceptTcpClient();
                    bck.ReportProgress(0, "Connected!");


                    stream = client.GetStream();

                    int i;

                    //Get all data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        //display received data by reporting progress to the background worker
                        bck.ReportProgress(0, bytes[1].ToString() + ", " + bytes[6].ToString());
                    }

                    //Shutdown and end connection
                    client.Close();


                }

            } catch (Exception ex) {
                bck.ReportProgress(0, string.Format("SocketException: {0}", ex.ToString()));


            }


        }

        private void Bck_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string data = (string)e.UserState;

            richTextBox1.Text = string.Format("Received: {0}", data) + Environment.NewLine + richTextBox1.Text;

        }

        private void SendMessage(int Message)
        {
            //change IP address to the machine where you want to send the message to
            if (client == null)
            {
                client = new TcpClient("127.0.0.1", 3000);
            }

            NetworkStream nwStream = client.GetStream();
            byte[] bytesToSend = new byte[8];
            bytesToSend[1] = (byte)Message;

            nwStream.Write(bytesToSend, 0, bytesToSend.Length);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SendMessage((int)Command.SmallDelay | (int)Command.PumpOne | (int)Command.PumpTwo | (int)Command.Stopped);
            }
            catch (Exception ex)
            {
                richTextBox1.Text = richTextBox1 + ex.ToString();
            }
        }
    }
}
