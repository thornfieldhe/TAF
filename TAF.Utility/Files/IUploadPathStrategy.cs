﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUploadPathStrategy.cs" company="">
//   
// </copyright>
// <summary>
//   上传路径策略
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Files
{
    /// <summary>
    /// 上传路径策略
    /// </summary>
    public interface IUploadPathStrategy
    {
        /// <summary>
        /// 获取上传路径
        /// </summary>
        /// <param name="fileName">
        /// 文件名，包含扩展名
        /// </param>
        /// <param name="fileCategory">
        /// 文件分类目录
        /// </param>
        /// <param name="baseCategory">
        /// 基分类目录
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        string GetPath(string fileName, string fileCategory = "", string baseCategory = "");
    }
}