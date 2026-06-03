using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Main.ViewModels;

namespace Main.Views;

public partial class UserView : UserControl
{
    public UserView()
    {
        InitializeComponent();
        DataContext = new UserViewModel();
    }
}