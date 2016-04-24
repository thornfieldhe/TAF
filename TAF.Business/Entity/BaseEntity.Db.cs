// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.Base.cs" company="">
//   
// </copyright>
// <summary>
//   业务类基类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using TAF.Core;
    using TAF.Entity;
    using TAF.Utility;

    using Validation;

    /// <summary>
    /// The base business.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract partial class BaseBusiness<T> : StatusDescription,
                                                    IEqualityComparer<T>,
                                                    IComparable<IBusinessBase>,
                                                    IValidationEntity
        where T : BaseBusiness<T>, IBusinessBase, new()
    {
        #region 基本状态
        protected bool _isNew = false;
        protected bool _isDirty = false;
        protected bool _isDelete = false;

        /// <summary>
        /// 新建数据，未录入数据库
        /// </summary>
        [NotMapped]
        public bool IsNew
        {
            get
            {
                return this._isNew;
            }
            protected set
            {
                this._isNew = value;
            }
        }

        /// <summary>
        /// 从数据库中读取出来标志删除数据
        /// </summary>
        [NotMapped]
        public bool IsDelete
        {
            get
            {
                return this._isDelete;
            }
            protected set
            {
                this._isDelete = value;
            }
        }

        /// <summary>
        /// 从数据库中读取出来已修改数据
        /// </summary>
        [NotMapped]
        public bool IsDirty
        {
            get
            {
                return this._isDirty;
            }
            protected set
            {
                this._isDirty = value;
            }
        }



        protected virtual void MarkNew()
        {
            this._isNew = true;
            this.MarkDirty();
        }

        public virtual void MarkOld()
        {
            this._isNew = false;
            this.MarkClean();
        }

        protected virtual void MarkClean()
        {
            this._isDirty = false;
        }

        protected virtual void MarkDirty()
        {
            this._isDirty = true;
        }

        /// <summary>
        /// 标记删除
        /// </summary>
        public virtual void MarkDelete()
        {
            this._isDelete = true;
            this.MarkDirty();
        }

        #endregion

        #region 克隆操作

        /// <summary>
        /// 创建浅表副本
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T GetShallowCopy()
        {
            return (T)MemberwiseClone();
        }

        /// <summary>
        /// 深度克隆
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Clone()
        {
            var graph = this.SerializeObjectToString();
            return graph.DeserializeStringToObject<T>();
        }

        #endregion

        #region IComparable<IBusinessBase> 成员

        /// <summary>
        /// The compare to.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CompareTo(IBusinessBase other)
        {
            return Id.CompareTo(other.Id);
        }

        #endregion

        #region 重载相等判断

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Equals(T x, T y)
        {
            return x.Id == y.Id;
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetHashCode(T obj)
        {
            return obj.Id.ToString().GetHashCode();
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return ToString() == obj.ToString();
        }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        //        /// <summary>
        //        /// The to string.
        //        /// </summary>
        //        /// <returns>
        //        /// The <see cref="string"/>.
        //        /// </returns>
        //        public override string ToString()
        //        {
        //            return Id.ToString();
        //        }

        /// <summary>
        /// The ==.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator ==(BaseBusiness<T> lhs, BaseBusiness<T> rhs)
        {
            if ((lhs as object) != null && (rhs as object) != null)
            {
                return lhs.Id == rhs.Id;
            }

            if ((lhs as object) == null && (rhs as object) == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// The !=.
        /// </summary>
        /// <param name="lhs">
        /// The lhs.
        /// </param>
        /// <param name="rhs">
        /// The rhs.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool operator !=(BaseBusiness<T> lhs, BaseBusiness<T> rhs)
        {
            if ((lhs as object) != null && (rhs as object) != null)
            {
                return !(lhs.Id == rhs.Id);
            }

            if ((lhs as object) == null && (rhs as object) == null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region 属性验证

        #region 字段

        /// <summary>
        /// 验证规则集合
        /// </summary>
        protected readonly List<IValidationRule> rules;

        /// <summary>
        /// 验证处理器
        /// </summary>
        protected IValidationHandler validateionHandler;

        #endregion

        #region SetValidationHandler(设置验证处理器)

        /// <summary>
        /// 设置验证处理器
        /// </summary>
        /// <param name="handler">
        /// 验证处理器
        /// </param>
        public void SetValidationHandler(IValidationHandler handler)
        {
            if (handler == null)
            {
                return;
            }

            this.validateionHandler = handler;
        }

        #endregion

        #region AddValidationRule(添加验证规则)

        /// <summary>
        /// 添加验证规则
        /// </summary>
        /// <param name="rule">
        /// 验证规则
        /// </param>
        public virtual void AddValidationRule(IValidationRule rule)
        {
            if (rule == null)
            {
                return;
            }

            rules.Add(rule);
        }

        #endregion

        #region Validate(验证)

        /// <summary>
        /// 验证
        /// </summary>
        public virtual void Validate()
        {
            var result = GetValidationResult();
            HandleValidationResult(result);
        }

        /// <summary>
        /// The is validated.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsValidated()
        {
            return GetValidationResult().IsValid;
        }

        /// <summary>
        /// 验证并添加到验证结果集合
        /// </summary>
        /// <param name="results">
        /// 验证结果集合
        /// </param>
        protected virtual void Validate(ValidationResultCollection results)
        {
        }

        /// <summary>
        /// 获取验证结果
        /// </summary>
        /// <returns>
        /// The <see cref="ValidationResultCollection"/>.
        /// </returns>
        private ValidationResultCollection GetValidationResult()
        {
            var result = Ioc.Create<IValidator>().Validate(this);
            Validate(result);
            foreach (var rule in rules)
            {
                result.Add(rule.Validate());
            }

            return result;
        }

        /// <summary>
        /// 处理验证结果
        /// </summary>
        /// <param name="results">
        /// The results.
        /// </param>
        private void HandleValidationResult(ValidationResultCollection results)
        {
            if (results.IsValid)
            {
                return;
            }

            this.validateionHandler.Handle(results);
        }

        #endregion

        #endregion
    }
}