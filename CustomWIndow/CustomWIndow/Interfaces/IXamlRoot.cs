using System.Collections.Generic;

using Microsoft.UI.Xaml;

namespace CustomWIndow.Interfaces
{
    public interface IXamlRoot
    {
        public static List<XamlRoot> XamlRootList { get; } = new();
    }
}
