using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Schatzoeker.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PuzzleScreen : Page
    {
        public int answer;
        public int correctAnswer;
        public PuzzleScreen()
        {
            this.InitializeComponent();
            answer = 0;
            correctAnswer = 0;
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
            answer = 2;
            correctAnswer = 0;

        }

        private void Correct_Checked(object sender, RoutedEventArgs e)
        {
            answer = 4;
            correctAnswer = 1;
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Correct_PopUp.IsOpen && answer != 4 && !Wrong_PopUp.IsOpen)
            {
                Correct_PopUp.IsOpen = true;
            }
            else if (!Correct_PopUp.IsOpen && !Wrong_PopUp.IsOpen && answer == 2)
            {
                Correct_PopUp.IsOpen = false;
                Wrong_PopUp.IsOpen = true;
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (Correct_PopUp.IsOpen || Wrong_PopUp.IsOpen)
            {
                this.Frame.Navigate(typeof (EndScreen), correctAnswer);
            }
        }
    }
}
