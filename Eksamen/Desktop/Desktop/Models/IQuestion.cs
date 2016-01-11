using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Desktop.Models
{
    public interface IQuestion : INotifyPropertyChanged
    {
        IDictionary<string, string> Answers { get; }
        string Text { get; set; }
        string Correct { get; set; }
        int Mistakes { get; }
        int Points { get; }
        bool Answered { get; }

        void SetAnswer(string option, string text, bool correct = false);

        ICommand AnswerButton { get; }
    }
}
