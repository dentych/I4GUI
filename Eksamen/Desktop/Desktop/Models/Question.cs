using Desktop.Magic;
using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Desktop.Models
{
    class Question : IQuestion
    {
        private int points;

        /// <summary>
        /// List of possible answers
        /// </summary>
        public IDictionary<string, string> Answers { get; private set; }
        public string Correct { get; set; }
        public string Text { get; set; }
        public int Mistakes { get; set; }
        public int Points
        {
            get { return points; }
            set
            {
                points = value;
                Notify("Points");
            }
        }
        public bool Answered { get; private set; }
        public int Index { get; set; }

        // Constructor
        public Question()
        {
            Answers = new Dictionary<string, string>();
            Answers.Add("A", string.Empty);
            Answers.Add("B", string.Empty);
            Answers.Add("C", string.Empty);
            Answers.Add("D", string.Empty);
        }

        public Question(QuestionDto dto)
        {
            Answers = new Dictionary<string, string>();
            Text = dto.Question;
            Answers["A"] = dto.A;
            Answers["B"] = dto.B;
            Answers["C"] = dto.C;
            Answers["D"] = dto.D;
            Correct = dto.Correct;
        }

        public void SetAnswer(string option, string text, bool correct = false)
        {
            if (!Answers.ContainsKey(option)) return;

            Answers[option] = text;
            if (correct)
            {
                Correct = option;
            }
        }

        #region Commands
        private ICommand _answerButton;
        public ICommand AnswerButton
        {
            get
            {
                return _answerButton ?? (_answerButton = new RelayCommand<Button>(Answer));
            }
        }
        #endregion

        #region Command methods
        private void Answer(Button button)
        {
            button.IsEnabled = false;

            if (button.Name == Correct)
            {
                var style = Application.Current.FindResource("CorrectButtonStyle") as Style;
                button.Style = style;
                Answered = true;
            }
            else
            {
                //var style = Application.Current.FindResource("IncorrectButtonStyle") as Style;
                //button.Style = style;
                button.Content = "";
                Mistakes++;
                switch (Mistakes)
                {
                    case 0:
                        Points = 0;
                        break;
                    case 1:
                        Points = 2;
                        break;
                    case 2:
                        Points = 4;
                        break;
                    case 3:
                        Points = 8;
                        break;
                }
            }
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
