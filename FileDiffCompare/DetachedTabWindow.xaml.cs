using System.Windows;
using System.Windows.Controls;

namespace FileDiffCompare
{
    /// <summary>
    /// Interaction logic for DetachedTabWindow.xaml
    /// </summary>
    public partial class DetachedTabWindow : Window
    {
        public DetachedTabWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Clear the content so it doesn't get disposed with the window
            TabContent.Content = null;
        }

        // New Reattach Button Click Event
        private void ReattachButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (Application.Current.MainWindow as MainWindow);
            if (mainWindow != null)
            {
                // If the content is a TextBox, retrieve its text (i.e., the old tab's name)
                string oldTabName = (TabContent.Content as TextBox)?.Text ?? "Restored Tab";

                // Create the new tab based on the method you provided
                mainWindow.CreateAndAddTab(oldTabName, TabContent.Content);

                // Close the detached window after reattaching
                this.Close();
            }
        }
    }
}

