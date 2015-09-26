using System;
using System.IO;
using System.Web;

namespace Numerous.Api
{
    public class Image
    {
        public string MediaType { get; internal set; }
        public byte[] Data { get; internal set; }

        public class Edit
        {
            public string MediaType { get; set; }
            public byte[] Data { get; set; }

            public static Edit FromFile(string filePath)
            {
                return new Edit
                {
                    MediaType = MimeMapping.GetMimeMapping(Path.GetFileName(filePath)),
                    Data = File.ReadAllBytes(filePath)
                };
            }

            public static Edit FromStream(string mediaType, Stream stream, int length = 0)
            {
                var actualLength = length != 0 ? length : (int) (stream.Length - stream.Position);
                var data = new Byte[actualLength];
                stream.Read(data, 0, actualLength);

                return new Edit
                {
                    MediaType = mediaType,
                    Data = data
                };
            }
        }
    }
}