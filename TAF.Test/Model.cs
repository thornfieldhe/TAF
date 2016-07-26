// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Model.cs" company="">
//   
// </copyright>
// <summary>
//   The user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TAF.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    using TAF.Core;
    using TAF.Utility;
    using Validation;

    /// <summary>
    /// 最大长度验证规则
    /// </summary>
    public class ContainsHXHValidationRule : IValidationRule
    {
        /// <summary>
        /// The txt.
        /// </summary>
        private readonly string txt;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainsHXHValidationRule"/> class. 
        /// 初始化客户英文名验证规则
        /// </summary>
        /// <param name="text">
        /// 客户
        /// </param>
        public ContainsHXHValidationRule(string text)
        {
            this.txt = text;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        public ValidationResult Validate()
        {
            if (!txt.Contains("HXH"))
            {
                return new ValidationResult("姓名必须包含HXH");
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// 最小长度验证规则
    /// </summary>
    public class MinLengthValidationRule : IValidationRule
    {
        /// <summary>
        /// The txt.
        /// </summary>
        private readonly string txt;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinLengthValidationRule"/> class. 
        /// 初始化客户英文名验证规则
        /// </summary>
        /// <param name="text">
        /// 客户
        /// </param>
        public MinLengthValidationRule(string text)
        {
            this.txt = text;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        public ValidationResult Validate()
        {
            if (this.txt.IsNull() || (this.txt.Length < 2))
            {
                return new ValidationResult("姓名长度不能小于2");
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// The user.
    /// </summary>
    [Serializable]
    public class User2 : BaseBusiness<User2>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required(ErrorMessage = @"姓名不能为空")]
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        protected override void Validate(ValidationResultCollection results)
        {
            results.Add(new MinLengthValidationRule(this.Name).Validate());
            base.Validate(results);
        }

        /// <summary>
        /// The add descriptions.
        /// </summary>
        /// <param name="isDefault">
        /// The is Default.
        /// </param>
        protected override void AddDescriptions()
        {
            this.AddDescription(nameof(Name), this.Name);
        }
    }

    /// <summary>
    /// The user.
    /// </summary>
    [Serializable]
    public class User : IComparable<User>, IUser
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email
        {
            get; set;
        }

        /// <summary>
        /// The compare to.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CompareTo(User other)
        {
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get; set;
        }
    }

    /// <summary>
    /// The User interface.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name
        {
            get; set;
        }
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal = 1,

        /// <summary>
        /// 错误
        /// </summary>
        [OrderBy(5)]
        [Description("错误")]
        Error = 2,

        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        [OrderBy(6)]
        Warning = 3,

        /// <summary>
        /// 信息
        /// </summary>
        [Description("信息")]
        Information = 4,

        /// <summary>
        /// 调试
        /// </summary>
        [OrderBy(3)]
        [Description("调试")]
        Debug = 5
    }

    public class IcoTestModel1 : IModel
    {
        public string Name
        {
            get; set;
        }

        public IcoTestModel1()
        {
            this.Name = "Model1";
        }
    }

    public class IcoTestModel2 : IModel
    {
        public string Name
        {
            get; set;
        }

        public IcoTestModel2()
        {
            this.Name = "Model2";
        }
    }

    public interface IModel
    {
        string Name
        {
            get; set;
        }
    }

    public class TestInfo
    {
        public string Name
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }


    }

    public class DefaultDbProvder : IDbProvider
    {
        public List<K> Get<K>(Expression<Func<K, bool>> query, bool useCache = false) where K : BaseBusiness<K>, new()
        {
            throw new NotImplementedException();
        }

        public K Find<K>(Expression<Func<K, bool>> func, bool useCache = false) where K : BaseBusiness<K>, new()
        {
            throw new NotImplementedException();
        }

        public Pager<T> Pages<K, R, T>(Pager<T> pager, Func<K, bool> whereFunc, Func<K, R> orderByFunc, bool isAsc = true, bool useCache = false) where K : BaseBusiness<K>, new() where R : new() where T : new()
        {
            throw new NotImplementedException();
        }

        public Pager<K> Pages<K, R>(Pager<K> pager, Func<K, bool> whereFunc, Func<K, R> orderByFunc, bool isAsc = true, bool useCache = false) where K : BaseBusiness<K>, new()
        {
            throw new NotImplementedException();
        }

        public bool Exist<K>(Expression<Func<K, bool>> func) where K : BaseBusiness<K>, new()
        {
            throw new NotImplementedException();
        }

        public int Count<K>(Expression<Func<K, bool>> func) where K : BaseBusiness<K>, new()
        {
            throw new NotImplementedException();
        }

        public int Delete<K>(Guid id, bool allowCommit = true) where K : BaseBusiness<K>, new()
        {
            throw new NotImplementedException();
        }

        public int Delete<K>(Expression<Func<K, bool>> func) where K : BaseBusiness<K>, new()
        {
            throw new NotImplementedException();
        }

        public int Delete<K>(K item, bool allowCommit) where K : BaseBusiness<K>, new()
        {
            throw new NotImplementedException();
        }

        public int Update<K>(Expression<Func<K, bool>> func, Expression<Func<K, K>> update) where K : BaseBusiness<K>, new()
        {
            throw new NotImplementedException();
        }

        public int Add<K>(K item, bool commit) where K : BaseBusiness<K>, new()
        {
            throw new NotImplementedException();
        }

        public int AddRange<K>(IEnumerable<K> items, bool commit) where K : BaseBusiness<K>, new() { throw new NotImplementedException(); }

        public int Commit()
        {
            throw new NotImplementedException();
        }
    }
}