using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.UI.Xaml.Controls;

namespace CustomWIndow.Interfaces.Views
{
    public interface IComboBox
    {
        public static List<ComboBox> ComboBoxList { get; } = new(); 
    }
}
