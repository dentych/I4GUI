using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace AgentApp
{
    public class Ticker : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _now;

        public Ticker()
        {
            _now = DateTime.Now.ToString();
            var timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += timer_Tick;
            timer.Start();
        }

        public string Now
        {
            get { return _now; }
            private set
            {
                _now = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Now"));
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Now = DateTime.Now.ToString();
        }
    }
}