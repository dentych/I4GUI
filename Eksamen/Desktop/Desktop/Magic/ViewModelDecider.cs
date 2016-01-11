using Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Magic
{
    public class ViewModelDecider : INotifyPropertyChanged
    {
        private static ViewModelDecider instance;

        public static ViewModelDecider Instance
        {
            get
            {
                return instance ?? (instance = new ViewModelDecider());
            }
        }

        private IViewModel viewmodel;
        public IViewModel ViewModel
        {
            get
            {
                return viewmodel;
            }
            set
            {
                viewmodel = value;
                Notify("ViewModel");
            }
        }

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
