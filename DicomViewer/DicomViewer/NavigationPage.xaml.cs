using DicomViewer.Models;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DicomViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationPage : TabbedPage
    {
        public NavigationPage()
        {
            InitializeComponent();
        }

        public async Task GoToPreviewPageAsync(DicomHeader dicom)
        {
            PreviewPage previewPage = (PreviewPage)Children[1];

            await previewPage.LoadDicomAsync(dicom);
            CurrentPage = previewPage;
        }
    }
}