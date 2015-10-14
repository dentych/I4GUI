using MvvmFoundation.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AgentApp
{
    class ColorControl
    {
        ICommand _changeBackground;
        public ICommand ChangeBackground
        {
            get { return _changeBackground ?? (_changeBackground = new RelayCommand<string>(NewBackground)); }
        }

        public void NewBackground(string color)
        {
            Brush brush;
            Color newColor = SystemColors.WindowColor;
            
            if (color != "default")
            {
                newColor = (Color) ColorConverter.ConvertFromString(color);
            }

            brush = new SolidColorBrush(newColor);
            Application.Current.MainWindow.Resources["backgroundBrush"] = brush;
        }
    }
}
