using Desktop.Magic;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    class HighscoreVM : IViewModel
    {
        private int highscore;
        public int Highscore
        {
            get { return Properties.Settings.Default.Highscore; }
            set
            {
                highscore = value;
                Notify("Highscore");
            }
        }

        public HighscoreVM()
        {
            Highscore = Properties.Settings.Default.Highscore;
        }

        private ICommand _reset;
        public ICommand Reset_Click
        {
            get { return _reset ?? (_reset = new RelayCommand(ResetClicked)); }
        }

        private void ResetClicked()
        {
            Properties.Settings.Default.Reset();
            Highscore = Properties.Settings.Default.Highscore;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName]string propName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
