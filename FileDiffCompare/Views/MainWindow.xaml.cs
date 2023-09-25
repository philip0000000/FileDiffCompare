using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FileDiffCompare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnAboutClick(object sender, RoutedEventArgs e)
        {
            // Create a new window to act as the dialog
            Window aboutWindow = new Window
            {
                Title = "About FileDiffCompare",
                Width = 300,
                Height = 200,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this, // Make the main window the owner so that this window is modal
                ResizeMode = ResizeMode.NoResize // Disable resizing
            };

            // Create a StackPanel to hold the controls
            StackPanel panel = new StackPanel
            {
                Margin = new Thickness(10)
            };

            // Create a TextBox to display the description
            TextBox descriptionTextBox = new TextBox
            {
                Text = "FileDiffCompare is a tool that allows users to compare two or more files and view their differences. Created by philip0000000",
                IsReadOnly = true,
                TextWrapping = TextWrapping.Wrap,
                BorderThickness = new Thickness(0) // No border
            };
            panel.Children.Add(descriptionTextBox);

            // Create an OK button to close the dialog
            Button okButton = new Button
            {
                Content = "OK",
                Width = 75,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Right
            };
            okButton.Click += (s, args) => aboutWindow.Close(); // Close the dialog when the button is clicked
            panel.Children.Add(okButton);

            // Add the StackPanel to the window and show it
            aboutWindow.Content = panel;
            aboutWindow.ShowDialog();
        }

        // ---

        private int tabCount = 2; // Start with 2 because of the initial tabs

        public void CreateAndAddTab(string tabName, object content)
        {
            tabCount++;  // Assuming tabCount is a class-level variable

            // Create the Close Button
            Button closeButton = new Button
            {
                Content = "X",
                Padding = new Thickness(2),
                Margin = new Thickness(5, 0, 0, 0)
            };
            closeButton.Click += CloseTabButton_Click;

            // Create the Drop Button
            Button dropButton = new Button
            {
                Content = "^",
                Padding = new Thickness(2),
                Margin = new Thickness(5, 0, 0, 0)
            };
            dropButton.Click += DropTabButton_Click;

            // Create the Left Move Button
            Button leftMoveButton = new Button
            {
                Content = "<",
                Padding = new Thickness(2),
                Margin = new Thickness(5, 0, 0, 0)
            };
            leftMoveButton.Click += LeftTabButton_Click;

            // Create the Right Move Button
            Button rightMoveButton = new Button
            {
                Content = ">",
                Padding = new Thickness(2),
                Margin = new Thickness(5, 0, 0, 0)
            };
            rightMoveButton.Click += RightTabButton_Click;

            // Use the passed tabName for the TextBlock
            TextBlock headerText = new TextBlock
            {
                Text = tabName,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Create StackPanel and add TextBlock and Buttons to it
            StackPanel headerPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            headerPanel.Children.Add(headerText);
            headerPanel.Children.Add(leftMoveButton);
            headerPanel.Children.Add(rightMoveButton);
            headerPanel.Children.Add(dropButton);
            headerPanel.Children.Add(closeButton);

            TabItem newTab = new TabItem
            {
                Header = headerPanel,
                Content = content  // Use the passed content
            };

            // Add the new TabItem to the TabControl
            tabControl.Items.Add(newTab);
            tabControl.SelectedItem = newTab; // Set focus to the new tab
        }

        private void OnNewTab(object sender, RoutedEventArgs e)
        {
            tabCount++;

            // Create the Close Button
            Button closeButton = new Button
            {
                Content = "X",
                Padding = new Thickness(2),
                Margin = new Thickness(5, 0, 0, 0)
            };
            closeButton.Click += CloseTabButton_Click;

            // Create the Drop Button
            Button dropButton = new Button
            {
                Content = "^",
                Padding = new Thickness(2),
                Margin = new Thickness(5, 0, 0, 0)
            };
            dropButton.Click += DropTabButton_Click;

            // Create the Left Move Button
            Button leftMoveButton = new Button
            {
                Content = "<",
                Padding = new Thickness(2),
                Margin = new Thickness(5, 0, 0, 0)
            };
            leftMoveButton.Click += LeftTabButton_Click;

            // Create the Right Move Button
            Button rightMoveButton = new Button
            {
                Content = ">",
                Padding = new Thickness(2),
                Margin = new Thickness(5, 0, 0, 0)
            };
            rightMoveButton.Click += RightTabButton_Click;

            // Create TextBlock for the tab header
            TextBlock headerText = new TextBlock
            {
                Text = "Tab " + tabCount,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Create StackPanel and add TextBlock and Buttons to it
            StackPanel headerPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };
            headerPanel.Children.Add(headerText);
            headerPanel.Children.Add(leftMoveButton);
            headerPanel.Children.Add(rightMoveButton);
            headerPanel.Children.Add(dropButton);
            headerPanel.Children.Add(closeButton);

            // Create the TabItem and set its properties
            TabItem newTab = new TabItem
            {
                Header = headerPanel,
                Content = new TextBox { Text = "Content for Tab " + tabCount }
            };

            // Add the new TabItem to the TabControl
            tabControl.Items.Add(newTab);
            tabControl.SelectedItem = newTab; // Set focus to the new tab
        }

        private void LeftTabButton_Click(object sender, RoutedEventArgs e)
        {
            // Identify the button that was clicked
            Button moveLeftButton = sender as Button;

            // Find the parent TabItem
            TabItem currentTab = FindParent<TabItem>(moveLeftButton);
            if (currentTab == null) return;

            // Get the index of the current tab
            int currentIndex = tabControl.Items.IndexOf(currentTab);

            // Ensure there's a tab to the left
            if (currentIndex > 0)
            {
                // Remove the current tab from the current position
                tabControl.Items.Remove(currentTab);

                // Insert the tab at the position to the left
                tabControl.Items.Insert(currentIndex - 1, currentTab);

                // Set focus to the moved tab
                tabControl.SelectedItem = currentTab;
            }
        }

        private void RightTabButton_Click(object sender, RoutedEventArgs e)
        {
            // Identify the button that was clicked
            Button moveRightButton = sender as Button;

            // Find the parent TabItem
            TabItem currentTab = FindParent<TabItem>(moveRightButton);
            if (currentTab == null) return;

            // Get the index of the current tab
            int currentIndex = tabControl.Items.IndexOf(currentTab);

            // Ensure there's a tab to the right
            if (currentIndex < tabControl.Items.Count - 1)
            {
                // Remove the current tab from the current position
                tabControl.Items.Remove(currentTab);

                // Insert the tab at the position to the right
                tabControl.Items.Insert(currentIndex + 1, currentTab);

                // Set focus to the moved tab
                tabControl.SelectedItem = currentTab;
            }
        }

        private void DropTabButton_Click(object sender, RoutedEventArgs e)
        {
            // Identify the button that was clicked
            Button dropButton = sender as Button;

            // Find the parent TabItem
            TabItem tabToDetach = FindParent<TabItem>(dropButton);
            if (tabToDetach == null) return;

            // Create a new DetachedTabWindow
            DetachedTabWindow detachedWindow = new DetachedTabWindow();

            // Set the content of the DetachedTabWindow to the content of the tab
            detachedWindow.TabContent.Content = tabToDetach.Content;

            // Remove the tab from the TabControl
            tabControl.Items.Remove(tabToDetach);

            // Show the new window
            detachedWindow.Show();
        }

        private void CloseTabButton_Click(object sender, RoutedEventArgs e)
        {
            Button closeButton = sender as Button;
            TabItem tab = FindParent<TabItem>(closeButton);

            if (tab != null)
            {
                tabControl.Items.Remove(tab);
            }
        }

        /// <summary>
        /// Utility function to get parent of a given type
        /// </summary>
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            // Get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // Return the parent item if it matches the type we're looking for
            if (parentObject == null) return null;

            if (parentObject is T parent)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(parentObject);
            }
        }

        // ---


    }
}
