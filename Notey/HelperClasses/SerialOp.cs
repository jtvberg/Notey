namespace Notey
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Media.Imaging;

    [Serializable]
    public static class SerialOp
    {
        public static object DeserializeOject(string serializedObject)
        {
            try
            {
                var serialString = serializedObject;
                var bytes = Convert.FromBase64CharArray(serialString.ToCharArray(), 0, serialString.Length);
                var bf = new BinaryFormatter();
                var ms = new MemoryStream(bytes);
                return bf.Deserialize(ms);
            }
            catch
            {
                
            }
            return string.Empty;
        }

        public static string SerializeObject(object serializableObject)
        {
            try
            {
                var bf = new BinaryFormatter();
                var ms = new MemoryStream();
                bf.Serialize(ms, serializableObject);
                var bytes = ms.ToArray();
                return Convert.ToBase64String(bytes);
            }
            catch
            {

            }
            return string.Empty;
        }

        public static BitmapImage ConvertToImage(byte[] array)
        {
            try
            {
                using (var ms = new System.IO.MemoryStream(array))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }
            catch
            {

            }
            return null;
        }

        public static byte[] ConvertToBytes(BitmapImage bitmapImage)
        {
            try
            {
                byte[] data;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }
                return data;
            }
            catch
            {

            }
            return null;
        }
    }

}
