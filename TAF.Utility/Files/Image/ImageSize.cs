// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageSize.cs" company="">
//   
// </copyright>
// <summary>
//   尺寸
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Files
{
    /// <summary>
    /// 尺寸
    /// </summary>
    public struct ImageSize
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSize"/> struct. 
        /// 初始化尺寸
        /// </summary>
        /// <param name="width">
        /// 宽度
        /// </param>
        /// <param name="height">
        /// 高度
        /// </param>
        public ImageSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            get;
        }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height
        {
            get;
        }
    }
}