namespace TAF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Core;

    /// <summary>
    /// The pager.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class Pager<T>
        where T : new()
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
        public IList<T> Datas
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
                var temp0 = Total % PageSize;//总页数是否能够整出每页数
                var temp1 = temp0 == 0 ? Total / PageSize : Total / PageSize + 1; //总分页数;
                return temp1 <= PageIndex;
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
        /// 分页查询对象列表
        /// </summary>
        /// <typeparam name="K">查询对象</typeparam>
        /// <param name="query">查询表达式</param>
        /// <param name="isAsc">是否是顺序</param>
        public void Load<K>(
            IEnumerable<K> query,
            bool isAsc) where K : EfBusiness<K>, IBusinessBase, new()
        {
            Total = query.Count();
            List<K> result;
            if (isAsc)
            {
                result = query
                       .OrderBy(r => r.CreatedDate)
                       .Skip(PageSize * (PageIndex - 1))
                       .Take(PageSize)
                       .ToList();
            }
            else
            {
                result = query
                       .OrderBy(r => r.CreatedDate)
                        .Skip(PageSize * (PageIndex - 1))
                        .Take(PageSize)
                        .ToList();
            }

            Datas = Mapper.Map<List<T>>(result);
            GetShowIndex();
        }

        /// <summary>
        /// 分页查询对象列表
        /// </summary>
        /// <typeparam name="R">排序字段类型</typeparam>
        /// <typeparam name="K">查询对象</typeparam>
        /// <param name="query">查询表达式</param>
        /// <param name="whereFunc">条件表达式</param>
        /// <param name="orderByFunc">排序表达式</param>
        /// <param name="isAsc">是否是顺序</param>
        public void Load<R, K>(
                        IEnumerable<K> query,
                        Func<K, bool> whereFunc,
                        Func<K, R> orderByFunc,
                        bool isAsc) where K : EfBusiness<K>, IBusinessBase, new()
        {
            Total = query.Where(whereFunc).Count();
            List<K> result;
            if (isAsc)
            {
                result = query
                       .Where(whereFunc)
                       .OrderBy(orderByFunc)
                       .Skip(PageSize * (PageIndex - 1))
                       .Take(PageSize)
                       .ToList();
            }
            else
            {
                result = query
                        .Where(whereFunc)
                        .OrderByDescending(orderByFunc)
                        .Skip(PageSize * (PageIndex - 1))
                        .Take(PageSize)
                        .ToList();
            }

            Datas = Mapper.Map<List<T>>(result);
            GetShowIndex();
        }

        /// <summary>
        /// The get show index.
        /// </summary>
        public void GetShowIndex()
        {
            var temp0 = Total % PageSize;//总页数是否能够整出每页数
            var temp1 = temp0 == 0 ? Total / PageSize : Total / PageSize + 1; //总分页数;

            ShowIndex = new List<int>();
            for (var i = 0; i < temp1; i++)
            {
                if (i * PageSize < PageIndex && (i + 1) * PageSize >= PageIndex)
                {
                    for (var j = 1; j <= 5; j++)
                    {
                        if (temp1 >= i * PageSize + j)
                        {
                            ShowIndex.Add(i * PageSize + j);
                        }
                    }
                }
            }


            if (temp1 > 5 && PageIndex % 5 == 0)
            {
                for (int i = 0; i < ShowIndex.Count; i++)
                {
                    ShowIndex[i] = ShowIndex[i] + 1;
                }
            }
            else if (PageIndex >= 6 && PageIndex % 5 == 1)
            {
                if (ShowIndex.Count == 5)
                {
                    for (int i = 0; i < ShowIndex.Count; i++)
                    {
                        ShowIndex[i] = ShowIndex[i] - 1;
                    }
                }
                else
                {
                    ShowIndex.Add(PageIndex - 1);
                    ShowIndex.Sort();
                }
            }
        }
    }

}