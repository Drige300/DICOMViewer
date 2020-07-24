using Dicom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DicomViewer.Models
{
    public class DicomDb
    {
        private const string QUERY_PREFIX =
            "https://services.cancerimagingarchive.net/services/v4/TCIA/query";
        private const string DEFAULT_SERIES_UID = 
            "1.3.6.1.4.1.14519.5.2.1.2135.6389.169014984339712898259516354025";

        public async Task<IEnumerable<DicomHeader>> GetDicomsAsync(string seriesUid = DEFAULT_SERIES_UID)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(
                $"{QUERY_PREFIX}/getSOPInstanceUIDs?SeriesInstanceUID={seriesUid}");
            string json = await response.Content.ReadAsStringAsync();
            IList<DicomHeader> dicoms = JsonConvert.DeserializeObject<IList<DicomHeader>>(json);
            int n = 1;

            foreach (DicomHeader dicom in dicoms)
            {
                dicom.SeriesInstanceUID = seriesUid;
                dicom.Name = $"Image #{n:D3}";
                n++;
            }

            return dicoms;
        }

        public async Task<DicomFile> ReadDicomFileAsync(DicomHeader dicom)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(
                $"{QUERY_PREFIX}/getSingleImage?SeriesInstanceUID={dicom.SeriesInstanceUID}&SOPInstanceUID={dicom.SOPInstanceUID}");
            Stream stream = await response.Content.ReadAsStreamAsync();
            DicomFile file = await DicomFile.OpenAsync(stream, readOption:FileReadOption.ReadAll);

            return file;
        }
    }
}
