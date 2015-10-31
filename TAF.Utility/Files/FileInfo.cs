﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileInfo.cs" company="">
//   
// </copyright>
// <summary>
//   文件信息
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Files
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    using TAF.Utility;

    /// <summary>
    /// 文件信息
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfo"/> class. 
        /// 初始化文件信息
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
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        protected FileInfo(string filePath, byte[] fileBytes, long? fileSize, string fileName)
        {
            FilePath = filePath;
            FileName = GetFileName(filePath, fileName);
            Extension = GetExtension(filePath);
            Length = new FileSize(fileSize.SafeValue());
            if (fileBytes == null)
            {
                return;
            }

            FileBytes = fileBytes;
            Length = new FileSize(fileBytes.Length);
        }

        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string FilePath
        {
            get;
        }

        /// <summary>
        /// 文件名，不包括扩展名
        /// </summary>
        public string FileName
        {
            get; private set;
        }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension
        {
            get; private set;
        }

        /// <summary>
        /// 文件字节流
        /// </summary>
        public byte[] FileBytes
        {
            get; private set;
        }

        /// <summary>
        /// 文件长度,单位：字节
        /// </summary>
        public FileSize Length
        {
            get; private set;
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="filePath">
        /// The file Path.
        /// </param>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetFileName(string filePath, string fileName)
        {
            return fileName.IsEmpty() ? GetFileNameWithoutExtension(filePath) : fileName;
        }

        /// <summary>
        /// 创建文件信息
        /// </summary>
        /// <param name="filePath">
        /// 文件相对路径
        /// </param>
        /// <param name="fileSize">
        /// 文件大小
        /// </param>
        /// <param name="fileName">
        /// 文件名
        /// </param>
        /// <returns>
        /// The <see cref="FileInfo"/>.
        /// </returns>
        public static FileInfo Create(string filePath, long? fileSize, string fileName = "")
        {
            return new FileInfo(filePath, null, fileSize, fileName);
        }

        /// <summary>
        /// 创建文件信息
        /// </summary>
        /// <param name="filePath">
        /// 文件相对路径
        /// </param>
        /// <param name="fileBytes">
        /// 文件字节流
        /// </param>
        /// <param name="fileName">
        /// 文件名
        /// </param>
        /// <returns>
        /// The <see cref="FileInfo"/>.
        /// </returns>
        public static FileInfo Create(string filePath, byte[] fileBytes, string fileName = "")
        {
            return new FileInfo(filePath, fileBytes, fileBytes.Length, fileName);
        }

        /// <summary>
        /// 获取文件的绝对路径,范例：c:/a.jpg
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetPhysicalPath()
        {
            return Web.GetPhysicalPath(FilePath);
        }

        /// <summary>
        /// 合并路径
        /// </summary>
        /// <param name="filePath">
        /// 文件路径
        /// </param>
        /// <param name="fileName">
        /// 文件名
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string Join(string filePath, string fileName)
        {
            return Path.Combine(filePath, fileName).Replace("\\", "/");
        }

        /// <summary>
        /// 从文件路径中获取文件名(包含扩展名)
        /// </summary>
        /// <param name="filePath">
        /// 文件路径
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        /// <summary>
        /// 从文件路径中获取文件名(不包含扩展名)
        /// </summary>
        /// <param name="filePath">
        /// 文件路径
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        /// <summary>
        /// 从文件路径中获取扩展名
        /// </summary>
        /// <param name="filePath">
        /// 文件路径
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetExtension(string filePath)
        {
            filePath = Path.GetExtension(filePath);
            if (filePath.IsEmpty())
            {
                return string.Empty;
            }

            return filePath.Replace(".", string.Empty);
        }

        /// <summary>
        /// 从文件路径中获取目录
        /// </summary>
        /// <param name="filePath">
        /// 文件路径
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetDirectoryName(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }

        /// <summary>
        /// 获取更安全的文件名，过滤无效字符，将汉字转成拼音简码，且对文件名添加时分秒，更不易重复，范例：@中国*.jpg，结果为zg-112233.jpg
        /// </summary>
        /// <param name="fileName">
        /// 文件名，包含扩展名，范例：c.jpg
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetSafeName(string fileName)
        {
            ValidateFileName(fileName);
            var result = new StringBuilder();
            result.AppendFormat("{0}-{1}", FilterFileName(fileName), Time.GetDateTime().ToString("HHmmss"));
            result.AppendFormat(".{0}", GetExtension(fileName));
            return result.ToString();
        }

        /// <summary>
        /// 验证文件名
        /// </summary>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        private static void ValidateFileName(string fileName)
        {
            if (fileName.IsEmpty())
            {
                throw new ArgumentNullException("fileName");
            }

            if (GetExtension(fileName).IsEmpty())
            {
                throw new ArgumentException("文件扩展名不能为空");
            }
        }

        /// <summary>
        /// 过滤文件名
        /// </summary>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string FilterFileName(string fileName)
        {
            fileName = GetFileNameWithoutExtension(fileName);
            fileName = Regex.Replace(fileName, "\\W", string.Empty);
            return fileName.GetChineseSpell();
        }
    }
}