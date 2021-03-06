﻿namespace TAF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    /// <summary>
    /// The pager.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class Pager<T> where T : new()
    {
        /// <summary>
        /// </summary>
        public Pager()
        {
            PageSize = 20;
        }

        /// <summary>
        /// </summary>
        /// <param name="pageIndex">
        /// The page Index.
        /// </param>
        /// <param name="pageSize">
        /// The page Size.
        /// </param>
        public Pager(int pageIndex = 1, int pageSize = 20)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        /// <summary>
        /// Gets or sets the datas.
        /// </summary>
        public List<T> Datas
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the page index.
        /// </summary>
        public int PageIndex
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        public int Total
        {
            get; set;
        }

        public bool IsFirst
        {
            get
            {
                return PageIndex == 1;
            }
        }

        public bool IsLast
        {
            get
            {
                var temp0 = Total % PageSize; //总页数是否能够整出每页数
                var temp1 = temp0 == 0 ? Total / PageSize : Total / PageSize + 1; //总分页数;
                return temp1 <= PageIndex;
            }
        }

        public int FirstPage
        {
            get
            {
                return 1;
            }
        }

        public int LastPage
        {
            get
            {
                var temp0 = Total % PageSize;//总页数是否能够整出每页数
                return temp0 == 0 ? Total / PageSize : Total / PageSize + 1; //总分页数;
            }
        }

        /// <summary>
        /// 表单页脚需要显示的页码
        /// </summary>
        public List<int> ShowIndex
        {
            get; set;
        }

      /// <summary>
        /// The get show index.
        /// </summary>
        public void GetShowIndex()
        {
            var showMaxPages = 5; //显示5条页码
            ShowIndex = new List<int>();
            var totalPage = Total % PageSize == 0 ? Total / PageSize : Total / PageSize + 1; //总分页数;

            for (int j = 0; j < totalPage; j++)
            {
                if (showMaxPages * j < PageIndex && showMaxPages * (j + 1) >= PageIndex)
                {
                    for (int p = showMaxPages * j + 1;
                         p <= (showMaxPages * (j + 1) < totalPage ? showMaxPages * (j + 1) : totalPage);
                         p++)
                    {
                        ShowIndex.Add(p);
                    }
                }
            }
            if (ShowIndex.Count == 0)
            {
                ShowIndex.Add(1);
            }
        }
    }
}