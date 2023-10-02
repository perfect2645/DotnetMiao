namespace Utils.Img
{
    public static class JpgUtil
    {
        public static string Base64ToImgSource(string base64Content)
        {
            var imgSource = Encryptor.Base64Decode(base64Content);

            return imgSource;
        }
    }
}
