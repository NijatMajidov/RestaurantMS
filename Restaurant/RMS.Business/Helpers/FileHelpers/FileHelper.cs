namespace RMS.Business.Helpers.FileHelpers
{
    public class FileHelper
    {
        public static void DeleteFile(string path)
        {
            if(!File.Exists(path))
            {
                throw new RMS.Business.Exceptions.FileNotFoundException("ImageFile","Image File Not found!!");
            }
            
                File.Delete(path);
            
        }
    }
}
