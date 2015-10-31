// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileUpload.cs" company="">
//   
// </copyright>
// <summary>
//   文件上传操作
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Files
{
    using System.Collections.Generic;

    using TAF.Utility;

    /// <summary>
    /// 文件上传操作
    /// </summary>
    public interface IFileUpload
    {
        /// <summary>
        /// 上传路径策略
        /// </summary>
        IUploadPathStrategy UploadPathStrategy
        {
            get; set;
        }

        /// <summary>
        /// 获取上传文件
        /// </summary>
        /// <param name="fileCategory">
        /// 文件分类目录
        /// </param>
        /// <param name="baseCategory">
        /// 基分类目录
        /// </param>
        /// <returns>
        /// The <see cref="FileInfo"/>.
        /// </returns>
        FileInfo GetFile(string fileCategory = "", string baseCategory = "");

        /// <summary>
        /// 获取上传图片
        /// </summary>
        /// <param name="fileCategory">
        /// 文件分类目录
        /// </param>
        /// <param name="baseCategory">
        /// 基分类目录
        /// </param>
        /// <returns>
        /// The <see cref="ImageInfo"/>.
        /// </returns>
        ImageInfo GetImage(string fileCategory = "", string baseCategory = "");

        /// <summary>
        /// 获取上传文件集合
        /// </summary>
        /// <param name="fileCategory">
        /// 文件分类目录
        /// </param>
        /// <param name="baseCategory">
        /// 基分类目录
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<FileInfo> GetFiles(string fileCategory = "", string baseCategory = "");

        /// <summary>
        /// 获取上传图片集合
        /// </summary>
        /// <param name="fileCategory">
        /// 文件分类目录
        /// </param>
        /// <param name="baseCategory">
        /// 基分类目录
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<ImageInfo> GetImages(string fileCategory = "", string baseCategory = "");

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileCategory">
        /// 文件分类目录
        /// </param>
        /// <param name="baseCategory">
        /// 基分类目录
        /// </param>
        /// <returns>
        /// The <see cref="FileInfo"/>.
        /// </returns>
        FileInfo UploadFile(string fileCategory = "", string baseCategory = "");

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileCategory">
        /// 文件分类目录
        /// </param>
        /// <param name="baseCategory">
        /// 基分类目录
        /// </param>
        /// <returns>
        /// The <see cref="ImageInfo"/>.
        /// </returns>
        ImageInfo UploadImage(string fileCategory = "", string baseCategory = "");

        /// <summary>
        /// 上传文件集合
        /// </summary>
        /// <param name="fileCategory">
        /// 文件分类目录
        /// </param>
        /// <param name="baseCategory">
        /// 基分类目录
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<FileInfo> UploadFiles(string fileCategory = "", string baseCategory = "");

        /// <summary>
        /// 上传图片集合
        /// </summary>
        /// <param name="fileCategory">
        /// 文件分类目录
        /// </param>
        /// <param name="baseCategory">
        /// 基分类目录
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<ImageInfo> UploadImages(string fileCategory = "", string baseCategory = "");
    }
}