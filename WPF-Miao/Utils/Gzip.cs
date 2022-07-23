using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Gzip
    {
        public static byte[] Compress(byte[] data)
        {
            using (var compressedStream = new MemoryStream())
            {
                using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
                {
                    zipStream.Write(data, 0, data.Length);
                    return compressedStream.ToArray();
                }
            }
        }
        public static byte[] Decompress(byte[] data)
        {
            using (var compressedStream = new MemoryStream(data))
            {
                using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                {
                    using (var resultStream = new MemoryStream())
                    {
                        zipStream.CopyTo(resultStream);
                        return resultStream.ToArray();
                    }
                }
            }
        }

        public static string Decompress(Stream contentStream)
        {
            using (var zipStream = new GZipStream(contentStream, CompressionMode.Decompress))
            {
                StreamReader sr = new StreamReader(zipStream, Encoding.UTF8);
                var retStr = sr.ReadToEnd();
                sr.Close(); 
                sr.Dispose();
                return retStr;
            }
        }

        public static string StreamToString(Stream ream, bool isGzip = false)
        {
            var retStr = string.Empty;
            if (ream == null)
            {
                return null;
            }

            var body = new List<byte>();
            int b = -1;
            var bts = new byte[102400];
            while ((b = ream.Read(bts, 0, bts.Length)) > 0) body.AddRange(bts.Take(b));
            ream.Close(); ream.Dispose();
            ream = new MemoryStream(body.ToArray());
            if (isGzip)
            {
                ream = new GZipStream(ream, CompressionMode.Decompress);
            }
            StreamReader sr = new StreamReader(ream, Encoding.UTF8);
            retStr = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
            ream.Close();
            ream.Dispose();
            return retStr;
        }
    }
}
