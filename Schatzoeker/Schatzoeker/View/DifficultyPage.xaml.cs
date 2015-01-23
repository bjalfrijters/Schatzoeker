using Schatzoeker.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Schatzoeker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DifficultyPage : Page
    {
        public int difficultyValue;
        public DifficultyPage()
        {
            this.InitializeComponent();
            difficultyValue = 2;

            if(difficultyValue != 0 || difficultyValue != 1)
            {
                Start.IsEnabled = false;
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            difficultyValue = 0;
            Start.IsEnabled = true;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            difficultyValue = 1;
            Start.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var difficulty = difficultyValue.ToString();
            this.Frame.Navigate(typeof (MapScreen), difficulty);
        }
    }
}
