using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RMS.Business.Services.Concretes
{
   /* public class QRCodeService
    {
        public byte[] GenerateQRCode(string data)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QrCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20); // 20 pixel genişlik

            using (var stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }*/
}
