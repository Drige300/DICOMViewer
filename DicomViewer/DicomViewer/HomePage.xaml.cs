using DicomViewer.Models;
using System.ComponentModel;
using Xamarin.Forms;

namespace DicomViewer
{
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void ListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            NavigationPage navigationPage = (NavigationPage)Parent;

            await navigationPage.GoToPreviewPageAsync((DicomHeader)e.Item);
        }
    }
}
