﻿using System.IO;
using System.Runtime.Remoting.Channels;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
            if (!Directory.Exists("~/PDF_Files"))
            {
                Directory.CreateDirectory("~/PDF_Files");
            }
            int i = 0;
            string path = "~/PDF_Files/qrBatch" + i + ".pdf";
            while (File.Exists(path))
            {
                i++;
                path = "~/PDF_Files/qrBatch" + i + ".pdf";
            }

            fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            writer = PdfWriter.GetInstance(document, fs);
            document.AddAuthor("alimTracingLabel");
        }
    }
}