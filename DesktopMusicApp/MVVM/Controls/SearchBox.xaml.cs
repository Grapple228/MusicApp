using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Desktop.MVVM.Controls;

public partial class SearchBox : UserControl
{
    public SearchBox()
    {
        InitializeComponent();
    }

    private void SearchBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if(sender is not TextBox textBox) return;

        if (e.Key == Key.Enter)
        {
            if (textBox.Text.Replace(" ", "") == "")
            {
                textBox.Text = "";
                return;
            }
            
            MessageBox.Show(((TextBox)sender).Text.Trim());
        }
    }
}