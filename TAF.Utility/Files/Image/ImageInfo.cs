// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageInfo.cs" company="">
//   
// </copyright>
// <summary>
//   图片信息
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Utility
{
    using TAF.Files;

    /// <summary>
    /// 图片信息
    /// </summary>
    public class ImageInfo : FileInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageInfo"/> class. 
        /// 初始化图片信息
        /// </summary>
        /// <param name="filePath">
        /// The file Path.
        /// </param>
        /// <param name="fileBytes">
        /// The file Bytes.
        /// </param>
        /// <param name="fileSize">
        /// The file ImageSize.
        /// </param>
        /// <param name="width">
        /// The width.
        /// </param>
        /// <param name="height">
        /// The height.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        private ImageInfo(string filePath, byte[] fileBytes, long? fileSize, int width, int height, string fileName)
            : base(filePath, fileBytes, fileSize, fileName)
        {
            this.ImageSize = new Files.ImageSize(width, height);
        }

        /// <summary>
        /// 尺寸
        /// </summary>
        public ImageSize ImageSize
        {
            get; private set;
        }

        /// <summary>
        /// 初始化图片信息
        /// </summary>
        /// <param name="filePath">
        /// 文件相对路径
        /// </param>
        /// <param name="fileSize">
        /// 文件大小
        /// </param>
        /// <param name="width">
        /// 宽度
        /// </param>
        /// <param name="height">
        /// 高度
        /// </param>
        /// <param name="fileName">
        /// 文件名
        /// </param>
        /// <returns>
        /// The <see cref="ImageInfo"/>.
        /// </returns>
        public static ImageInfo Create(string filePath, long? fileSize, int width, int height, string fileName = "")
        {
            return new ImageInfo(filePath, null, fileSize, width, height, fileName);
        }

        /// <summary>
        /// 初始化图片信息
        /// </summary>
        /// <param name="filePath">
        /// 文件相对路径
        /// </param>
        /// <param name="fileBytes">
        /// 文件字节流
        /// </param>
        /// <param name="width">
        /// 宽度
        /// </param>
        /// <param name="height">
        /// 高度
        /// </param>
        /// <param name="fileName">
        /// 文件名
        /// </param>
        /// <returns>
        /// The <see cref="ImageInfo"/>.
        /// </returns>
        public static ImageInfo Create(string filePath, byte[] fileBytes, int width, int height, string fileName = "")
        {
            return new ImageInfo(filePath, fileBytes, fileBytes.Length, width, height, fileName);
        }
    }
}