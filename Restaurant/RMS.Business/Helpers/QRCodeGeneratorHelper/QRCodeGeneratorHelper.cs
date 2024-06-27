using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace RMS.Business.Helpers.QRCodeGeneratorHelper
{
    public class QRCodeGeneratorHelper : IQRCodeGeneratorHelper
    {
        public byte[] GenerateQRCode(string text)
        {
            byte[] QRCode= new byte[0];
            if (!string.IsNullOrEmpty(text))
            {
                QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                QRCodeData data = qRCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qrcode = new BitmapByteQRCode(data);
                QRCode = qrcode.GetGraphic(20);

                using (var bitmap = new Bitmap(new MemoryStream(QRCode)))
                {
                    using (var stream = new MemoryStream())
                    {
                        bitmap.Save(stream, ImageFormat.Png);
                        QRCode = stream.ToArray();
                    }
                }
            }
            return QRCode;
        }
    }
}
