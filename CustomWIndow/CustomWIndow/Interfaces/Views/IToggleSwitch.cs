using System.Collections.Generic;

using Microsoft.UI.Xaml.Controls;

namespace CustomWIndow.Interfaces.Views
{
    public interface IToggleSwitch
    {
        public static List<ToggleSwitch> ToggleSwitchList { get; } = new();
    }
}
