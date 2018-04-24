using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.interfaces;
using Tracage.Models;

namespace TracageAlmentaireWeb.BL.Components.PDF
{
    public class PdfQRWriter
    {
        private Document document;
        private FileStream fs;
        private PdfWriter writer;




        public void AddInfo(Product p)
        {

            using (document)
            {
                document.Open();
                document.SetMarginMirroring(true);
                Paragraph p1 = new Paragraph((p.Name + " : " + p.Description));
                Paragraph p2 = new Paragraph(p.QRCode);
                var pic1 = iTextSharp.text.Image.GetInstance(QrGenerator.GenerateQRCodeBitmap(p.QRCode, 250), System.Drawing.Imaging.ImageFormat.Jpeg);
                var pic2 = iTextSharp.text.Image.GetInstance(QrGenerator.GenerateQRCodeBitmap(p.QRCode, 100), System.Drawing.Imaging.ImageFormat.Jpeg);

                p1.Alignment = Element.ALIGN_CENTER;
                p2.Alignment = Element.ALIGN_CENTER;
                pic1.Alignment = Element.ALIGN_CENTER;
                pic2.Alignment = Element.ALIGN_CENTER;

                document.Add(p1);
                document.Add(p2);
                document.Add(pic1);
                document.Add(pic2);

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