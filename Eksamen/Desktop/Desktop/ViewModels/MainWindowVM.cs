using Desktop.Magic;
using Desktop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Desktop.ViewModels
{
    public class MainWindowVM : IViewModel
    {
        private int points;

        public IList<IQuestion> Questions { get; set; }
        public int Points
        {
            get { return points; }
            set
            {
                points = value;
                Notify("Points");
            }
        }

        public MainWindowVM()
        {
            JSONReader reader = new JSONReader();
            Questions = reader.Deserialize();
        }

        private int GetPoints()
        {
            int sum = 0;

            foreach (var question in Questions)
            {
                sum += question.Points;
            }

            return sum;
        }

        #region Commands
        private ICommand _answerButton;
        public ICommand AnswerButton
        {
            get
            {
                return _answerButton ?? (_answerButton = new RelayCommand<Button>(AnswerButtonClicked));
            }
        }

        private void AnswerButtonClicked(Button button)
        {
            MessageBox.Show("WUHU! Name of box: " + button.Name);
        }

        private ICommand _complete;
        public ICommand CompleteBtn
        {
            get { return _complete ?? (_complete = new RelayCommand(Complete, CanCalculatePoints)); }
        }

        private void Complete()
        {
            ViewModelDecider.Instance.ViewModel = new EndWindowVM(GetPoints());
        }
        private bool CanCalculatePoints()
        {
            foreach (var q in Questions)
            {
                if (!q.Answered) return false;
            }

            return true;
        }
        #endregion

        #region Event stuff
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify([CallerMemberName]string propName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
    }
}
