using System.Linq;
using Windows.UI.Xaml.Controls;
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
