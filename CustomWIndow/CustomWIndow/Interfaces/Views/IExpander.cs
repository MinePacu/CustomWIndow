using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.UI.Xaml.Controls;

namespace CustomWIndow.Interfaces.Views
{
    public interface IExpander
    {
        public static List<Expander> ExpanderList { get; } = new();
    }
}
