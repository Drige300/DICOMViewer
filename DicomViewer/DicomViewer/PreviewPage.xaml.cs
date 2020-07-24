using Android.Graphics;
using Dicom;
using Dicom.Imaging;
using DicomViewer.Models;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DicomViewer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreviewPage : ContentPage
    {
        private DicomDb db;
        private bool isLoading;

        public PreviewPage()
        {
            InitializeComponent();
            db = new DicomDb();
        }

        public async Task LoadDicomAsync(DicomHeader dicom)
        {
            if (!isLoading)
            {
                isLoading = true;

                try
                { 
                    DicomFile file = await db.ReadDicomFileAsync(dicom);
                    DicomImage image = new DicomImage(file.Dataset);
                    Bitmap bitmap = image.RenderImage().As<Bitmap>();
                    string path = System.IO.Path.Combine(
                        System.IO.Path.GetTempPath(), "dicom.png");
                    Stream stream = new FileStream(path, FileMode.Create);

                    await bitmap.CompressAsync(Bitmap.CompressFormat.Png, 100, stream);
                    stream.Close();

                    titleLabel.Text = dicom.Name;
                    dicomImage.Source = ImageSource.FromFile(path);
                }
                finally
                {
                    isLoading = false;
                }
            }
        }
    }
}