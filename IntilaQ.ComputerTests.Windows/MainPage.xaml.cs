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
using IntilaQ.ComputerTests.Client.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IntilaQ.ComputerTests.Windows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        private void AnswersTestListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = DataContext as MainViewModdel;

            var suggestedAnswersListView = sender as ListView;

            if (suggestedAnswersListView != null)
            //if (suggestedAnswersListView?.SelectedItem != null)
            {
                vm.CandidateUser.AnswerTests
                    .First(test => test.SuggestedAnswers == suggestedAnswersListView.ItemsSource)
                    .ChosenAnswer =
                    suggestedAnswersListView.SelectedItem as string;
            }

            vm.SetNumberOfAnsweredQuestionsCommand.Execute(null);
        }
    }
}
