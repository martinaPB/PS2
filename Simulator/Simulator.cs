using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulator
{

    public enum Command
    {
        Stopped = 0,
        Started = 1,
        SmallDelay = 2,
        LongDelay = 8,
        PumpOne = 32,
        PumpTwo = 64
    }

    /// <summary>
    /// Enumerare folosita pentru a modela starea procesului
    /// </summary>
    public enum ProcessState
    {
        PumpOne = 1,
        PumpTwo = 2,
        Level_1 = 4,
        Level_2 = 8,
        Level_3 = 16,
        Level_4 = 32,
        Level_5 = 64,
        Stopped = 128
    }




    public class Simulator
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        private byte[] _state = new byte[] { 0x00, 0x00 };

        private void ExecuteLongDelayCommand(int Delay)
        {
            System.Threading.Thread.Sleep(Delay);
            _state[0] = (int)ProcessState.PumpOne;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.PumpOne
                | (int)ProcessState.PumpTwo;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.PumpOne
                | (int)ProcessState.PumpTwo
                | (int)ProcessState.Level_1;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.PumpOne
                | (int)ProcessState.PumpTwo
                | (int)ProcessState.Level_1
                | (int)ProcessState.Level_2;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.PumpOne
                | (int)ProcessState.PumpTwo
                | (int)ProcessState.Level_1
                | (int)ProcessState.Level_2
                | (int)ProcessState.Level_3;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.PumpOne
                | (int)ProcessState.PumpTwo
                | (int)ProcessState.Level_1
                | (int)ProcessState.Level_2
                | (int)ProcessState.Level_3
                | (int)ProcessState.Level_4;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.PumpOne
                | (int)ProcessState.PumpTwo
                | (int)ProcessState.Level_1
                | (int)ProcessState.Level_2
                | (int)ProcessState.Level_3
                | (int)ProcessState.Level_4
                | (int)ProcessState.Level_5;
            System.Threading.Thread.Sleep(Delay);
        }

        private void ExecuteShortDelayCommand(int Delay)
        {
            _state[0] = (int)ProcessState.PumpOne
                | (int)ProcessState.PumpTwo
                | (int)ProcessState.Level_1
                | (int)ProcessState.Level_2
                | (int)ProcessState.Level_3
                | (int)ProcessState.Level_4
                | (int)ProcessState.Level_5;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.PumpTwo
                | (int)ProcessState.Level_1
                | (int)ProcessState.Level_2
                | (int)ProcessState.Level_3
                | (int)ProcessState.Level_4
                | (int)ProcessState.Level_5;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.Level_1
                | (int)ProcessState.Level_2
                | (int)ProcessState.Level_3
                | (int)ProcessState.Level_4
                | (int)ProcessState.Level_5;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.Level_1
                | (int)ProcessState.Level_2
                | (int)ProcessState.Level_3
                | (int)ProcessState.Level_4;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.Level_1
                | (int)ProcessState.Level_2
                | (int)ProcessState.Level_3;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.Level_1
                | (int)ProcessState.Level_2;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.Level_1;
            System.Threading.Thread.Sleep(Delay);

            _state[0] = (int)ProcessState.Stopped;
            System.Threading.Thread.Sleep(Delay);
        }

        public void UpdateState(int CommandState)
        {
            int longDelayForBothPump = (int)Command.LongDelay
                | (int)Command.PumpOne
                | (int)Command.PumpTwo
                | (int)Command.Started;

            int shortDelayForBothPump = (int)Command.SmallDelay
                | (int)Command.PumpOne
                | (int)Command.PumpTwo
                | (int)Command.Stopped;

            if (CommandState == longDelayForBothPump)
            {
                ExecuteLongDelayCommand(5000);
            }
            else if (CommandState == shortDelayForBothPump)
            {
                ExecuteShortDelayCommand(2000);
            }
        }

        public byte[] GetState()
        {
            return _state;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }


    }
}
