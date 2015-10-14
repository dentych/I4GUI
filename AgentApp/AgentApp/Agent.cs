using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using System.Runtime.CompilerServices;

namespace I4GUI
{
    public class Agents : ObservableCollection<Agent>, INotifyPropertyChanged
    {
        #region Commands
        ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                return _nextCommand ?? (_nextCommand = new RelayCommand(
              () => ++CurrentIndex,
              () => CurrentIndex < (Count - 1)));
            }
        }

        ICommand _previousCommand;
        public ICommand PreviousCommand
        {
            get
            {
                return _previousCommand ?? (_previousCommand = new RelayCommand(
              () => --CurrentIndex,
              () => CurrentIndex > 0));
            }
        }

        ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(AddAgent)); }
        }
        private void AddAgent()
        {
            Add(new Agent("", "New agent", "", "", ""));
            CurrentIndex = Count - 1;
            Notify("Count");
        }

        ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new RelayCommand(
              DeleteAgent,
              () => CurrentIndex >= 0));
            }
        }
        private void DeleteAgent()
        {
            RemoveAt(CurrentIndex);
            Notify("Count");
        }
        #endregion

        #region Properties
        int currentIndex = -1;
        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                if (currentIndex != value)
                {
                    currentIndex = value;
                    Notify();
                }
            }
        }
        #endregion

        #region INotify stuff
        public new event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string propName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
    };  // Just to reference it from xaml

    [Serializable]
    public class Agent
    {
        string id;
        string codeName;
        string speciality;
        string assignment;

        public Agent()
        {
        }

        public Agent(string aId, string aName, string aAddress, string aSpeciality, string aAssignment)
        {
            id = aId;
            codeName = aName;
            speciality = aSpeciality;
            assignment = aAssignment;
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string CodeName
        {
            get
            {
                return codeName;
            }
            set
            {
                codeName = value;
            }
        }

        public string Speciality
        {
            get
            {
                return speciality;
            }
            set
            {
                speciality = value;
            }
        }

        public string Assignment
        {
            get
            {
                return assignment;
            }
            set
            {
                assignment = value;
            }
        }
    }
}
