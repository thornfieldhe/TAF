// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Image.cs" company="">
//   
// </copyright>
// <summary>
//   图片操作
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Files
{
    using System.IO;

    /// <summary>
    /// 图片操作
    /// </summary>
    public class Image
    {
        /// <summary>
        /// 图片文件的绝对路径
        /// </summary>
        /// <param name="filePath">
        /// 图片文件的绝对路径
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static Image FromFile(string filePath)
        {
            return FromFile(filePath);
        }

        /// <summary>
        /// 图片文件流
        /// </summary>
        /// <param name="stream">
        /// 流
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static System.Drawing.Image FromStream(Stream stream)
        {
            return FromStream(stream);
        }

        /// <summary>
        /// 图片文件流
        /// </summary>
        /// <param name="buffer">
        /// 字节流
        /// </param>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        public static System.Drawing.Image FromStream(byte[] buffer)
        {
            using (var stream = new MemoryStream(buffer))
            {
                return FromStream(stream);
            }
        }
    }
}