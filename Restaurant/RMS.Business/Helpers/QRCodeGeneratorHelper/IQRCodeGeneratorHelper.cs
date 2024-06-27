namespace RMS.Business.Helpers.QRCodeGeneratorHelper
{
    public interface IQRCodeGeneratorHelper
    {
        byte[] GenerateQRCode(string text);
    }
}
