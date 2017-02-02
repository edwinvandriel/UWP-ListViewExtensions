using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EvD.ListViewExtensions.SampleApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<string> SampleCollection{ get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            SampleCollection = new ObservableCollection<string>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Fill();
            SampleListView.ItemsSource = SampleCollection;
        }

        private void ClearButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SampleCollection.Clear();
        }

        private void FillButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Fill();
        }

        private void Fill()
        {
            for (var idx = 0; idx < 1000; idx++)
            {
                SampleCollection.Add($"This is row number {idx}");
            }
        }
    }
}
