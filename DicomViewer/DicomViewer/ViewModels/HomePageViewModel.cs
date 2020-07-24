using DicomViewer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DicomViewer.ViewModels
{
    public class HomePageViewModel : DefaultNotifyPropertyChanged
    {
        private DicomDb db;
        private IEnumerable<DicomHeader> dicoms; 

        public HomePageViewModel()
        {
            _ = InitializeAsync();
        }

        public IEnumerable<DicomHeader> Dicoms
        {
            get => dicoms;
            set => SetProperty(ref dicoms, value);
        }

        private async Task InitializeAsync()
        {
            db = new DicomDb();
            Dicoms = await db.GetDicomsAsync();
        }
    }
}
