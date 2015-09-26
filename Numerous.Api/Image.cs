#region References

using System;
using System.IO;
using System.Web;

#endregion

namespace Numerous.Api
{
    /// <summary>
    /// Represents an image type and data.
    /// </summary>
    /// <remarks>
    /// This object is read-only. Use <see cref="Image.Edit"/> to create a new image.
    /// </remarks>
    public class Image
    {
        #region Properties

        /// <summary>
        /// Gets the media type of the image.
        /// </summary>
        /// <value>
        /// The media type of the image.
        /// </value>
        public string MediaType { get; internal set; }

        /// <summary>
        /// Gets the image data.
        /// </summary>
        /// <value>
        /// The image data.
        /// </value>
        public byte[] Data { get; internal set; }

        #endregion

        /// <summary>
        /// Represents an image creation or update.
        /// </summary>
        public class Edit
        {
            #region Properties

            /// <summary>
            /// Gets the media type of the image.
            /// </summary>
            /// <value>
            /// The media type of the image.
            /// </value>
            public string MediaType { get; set; }

            /// <summary>
            /// Gets the image data.
            /// </summary>
            /// <value>
            /// The image data.
            /// </value>
            public byte[] Data { get; set; }

            #endregion

            #region Methods

            /// <summary>
            /// Loads an image from a file.
            /// </summary>
            /// <param name="filePath">The file path.</param>
            /// <returns>The image.</returns>
            public static Edit FromFile(string filePath)
            {
                return new Edit
                {
                    MediaType = MimeMapping.GetMimeMapping(Path.GetFileName(filePath)),
                    Data = File.ReadAllBytes(filePath)
                };
            }

            /// <summary>
            /// Loads an image from a data stream.
            /// </summary>
            /// <param name="mediaType">The media type of the image.</param>
            /// <param name="stream">The data stream.</param>
            /// <param name="length">The data length. If not set, the entire stream is loaded.</param>
            /// <returns>The image.</returns>
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

            #endregion
        }
    }
}