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
        public int choice;
        public int correctAnswer;
        private int sum1;
        private int sum2;
        private int answerSum;
        private int score = 100;
        private string message;
        private string playerName;

        public PuzzleScreen()
        {
            this.InitializeComponent();
            choice = 0;
            correctAnswer = 0;
            Random rng = new Random();
            sum1 = rng.Next(5);
            sum2 = rng.Next(5);
            answerSum = sum2 + sum1;
            int wrong = rng.Next(10);
            while (wrong == answerSum) {
                wrong = rng.Next(10);
            }
            int temp = rng.Next(2);
            if (temp == 0)
            {
                check1.Content = wrong.ToString();
                check2.Content = answerSum.ToString();
            }
            else
            {
                check1.Content = answerSum.ToString();
                check2.Content = wrong.ToString();
            }
            quest.Text = string.Format("{0}+{1}=", sum1, sum2);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (this.Frame.BackStack.Last().SourcePageType == typeof(MapScreen))
            {
                if (e.Parameter != null)
                {
                    var messageVar = e.Parameter;
                    message = (String)messageVar;
                    char[] splitToken = { ':' };
                    string[] messageSplit = message.Split(splitToken);
                    playerName = messageSplit[0];
                    string scoreString = messageSplit[1];
                    score = Int32.Parse(scoreString);
                    Debug.WriteLine(message + "puzzlescreen");

                }
            }
            
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            choice = Convert.ToInt32(check1.Content);

        }

        private void Correct_Checked(object sender, RoutedEventArgs e)
        {
            choice = Convert.ToInt32(check2.Content);
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Correct_PopUp.IsOpen && !Wrong_PopUp.IsOpen && choice == answerSum)
            {
                Correct_PopUp.IsOpen = true;
                correctAnswer = 1;
            }
            else if (!Correct_PopUp.IsOpen && !Wrong_PopUp.IsOpen && choice != answerSum)
            {
                Correct_PopUp.IsOpen = false;
                Wrong_PopUp.IsOpen = true;
                correctAnswer = 0;
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (Correct_PopUp.IsOpen || Wrong_PopUp.IsOpen)
            {
                
                    if (Correct_PopUp.IsOpen)
                        score += 100;
                    else
                        score += 50;

                    message = playerName + ":" + score;
                    this.Frame.Navigate(typeof(MapScreen), message);
            }
        }

        private void Stop2_Click(object sender, RoutedEventArgs e)
        {
            if (Correct_PopUp.IsOpen || Wrong_PopUp.IsOpen)
            {

                if (Correct_PopUp.IsOpen)
                    score += 100;
                else
                    score += 50;

                message = playerName + ":" + score;
                this.Frame.Navigate(typeof(HighscoreScreen), message);
            }
        }
    }
}
