// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSize.cs" company="">
//   
// </copyright>
// <summary>
//   文件大小
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Files
{
    using TAF.Utility;

    /// <summary>
    /// 文件大小
    /// </summary>
    public struct FileSize
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileSize"/> struct. 
        /// 初始化文件大小
        /// </summary>
        /// <param name="size">
        /// 文件字节大小
        /// </param>
        /// <param name="unit">
        /// 文件字节大小
        /// </param>
        public FileSize(long size, FileUnit unit = FileUnit.Byte)
        {
            this.Size = GetSize(size, unit);
        }

        /// <summary>
        /// 文件字节长度
        /// </summary>
        public long Size
        {
            get;
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="unit">
        /// The unit.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        private static long GetSize(long size, FileUnit unit)
        {
            switch (unit)
            {
                case FileUnit.K:
                    return size * 1024;
                case FileUnit.M:
                    return size * 1024 * 1024;
                case FileUnit.G:
                    return size * 1024 * 1024 * 1024;
                default:
                    return size;
            }
        }

        /// <summary>
        /// 获取文件大小，单位：G
        /// </summary>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double GetSizeByG()
        {
            return (this.Size / 1024.0 / 1024.0 / 1024.0).ToString().ToDouble().ToFixed(2);
        }

        /// <summary>
        /// 获取文件大小，单位：M
        /// </summary>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double GetSizeByM()
        {
            return (this.Size / 1024.0 / 1024.0).ToString().ToDouble().ToFixed(2);
        }

        /// <summary>
        /// 获取文件大小，单位：K
        /// </summary>
        /// <returns>
        /// The <see cref="double"/>.
        /// </returns>
        public double GetSizeByK()
        {
            return (this.Size / 1024.0).ToString().ToDouble().ToFixed(2);
        }

        /// <summary>
        /// 获取文件大小，单位：字节
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetSize()
        {
            return (int)Size;
        }

        /// <summary>
        /// 输出描述
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            if (this.Size >= 1024 * 1024 * 1024)
            {
                return string.Format("{0} {1}", GetSizeByG(), FileUnit.G.Description());
            }

            if (this.Size >= 1024 * 1024)
            {
                return string.Format("{0} {1}", GetSizeByM(), FileUnit.M.Description());
            }

            if (this.Size >= 1024)
            {
                return string.Format("{0} {1}", GetSizeByK(), FileUnit.K.Description());
            }

            return string.Format("{0} {1}", this.Size, FileUnit.Byte.Description());
        }
    }
}