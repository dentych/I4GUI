using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    public class EndWindowVM : IViewModel
    {
        private int score;
        private bool newhigh;
        private int previousscore;

        public int FinalScore
        {
            get { return score; }
            private set
            {
                score = value;
                Notify("FinalScore");
            }
        }
        public bool NewHighscore
        {
            get { return newhigh; }
            private set
            {
                newhigh = value;
                Notify("NewHighscore");
            }
        }
        public int PreviousHighscore
        {
            get { return previousscore; }
            private set
            {
                previousscore = value;
                Notify("PreviousHighscore");
            }
        }

        public EndWindowVM(int points)
        {
            int currentHighscore = Properties.Settings.Default.Highscore;
            FinalScore = points;
            if (points < currentHighscore)
            {
                NewHighscore = true;
                PreviousHighscore = currentHighscore;
                Properties.Settings.Default.Highscore = points;
                Properties.Settings.Default.Save();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName]string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
