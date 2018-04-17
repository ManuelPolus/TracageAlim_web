using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.interfaces;

namespace TracageAlmentaireWeb.BL.Components.PDF
{
    public class PdfQRWriter
    {
        private Document document;
        private FileStream fs;
        private PdfWriter writer;




        public void AddInfo(List<string> infos, List<Bitmap> QRs)
        {

            using (document)
            {
                document.Open();
                for (int i = 0; i < infos.Count; i++)
                {
                    document.Add(new Paragraph(infos.ElementAt(i)));
                    iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(QRs.ElementAt(i), System.Drawing.Imaging.ImageFormat.Jpeg);
                    document.Add(pic);
                }
                document.Close();
            }

        }

        public void CreateOrRefreshDocument()
        {
            this.document = new Document();
            document.AddAuthor("alimTracingLabel");
            if (!Directory.Exists("C:\\\\TEMP\\"))
            {
                Directory.CreateDirectory("C:\\\\TEMP\\");
            }
            int i = 0;
            string path = "C:\\\\TEMP\\qrBatch" + i + ".pdf";
            while (File.Exists(path))
            {
                i++;
                path = "C:\\\\TEMP\\qrBatch" + i + ".pdf";
            }

            fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            writer = PdfWriter.GetInstance(document, fs);
            document.AddAuthor("alimTracingLabel");
        }
    }
}