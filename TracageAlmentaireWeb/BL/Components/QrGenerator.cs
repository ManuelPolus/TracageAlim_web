using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Tracage.Models;
using ZXing;
using ZXing.Common;

namespace TracageAlmentaireWeb.BL.Components
{
    public class QrGenerator
    {
        public static Bitmap GenerateQRCodeBitmap(string code,int size)
        {
            var qrWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions { Height = size, Width = size }
            };
            var bitmap = new Bitmap(qrWriter.Write(code));
            return bitmap;
        }

        public static string GenerateQRCodeString(Product p)
        {
            Guid g = Guid.NewGuid();
            string randString = Convert.ToBase64String(g.ToByteArray());
            randString = randString.Replace("=", "");
            randString = randString.Replace("+", "");
            randString = randString.Replace("/", "");
            randString = randString.Replace("?", "");
            return p.Name + "-" + randString + new Random().Next(0, 9999);
        }
       
    }
}