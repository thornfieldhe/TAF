﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="">
//   
// </copyright>
// <summary>
//   The base entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    using Core;

    using TAF.Utility;

    /// <summary>
    /// 业务系统基类
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    [Serializable]
    public abstract partial class BaseBusiness<T> : IBusinessBase, INotifyPropertyChanged
    {

        #region 构造函数

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBusiness{T}"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        protected BaseBusiness(Guid id)
        {
            Id = id;
            Status = 0;
            CreatedDate = DateTime.Now;
            ChangedDate = DateTime.Now;

            rules = new List<IValidationRule>();
            this.validateionHandler = Ioc.Create<IValidationHandler>();
            MarkNew();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBusiness{T}"/> class.
        /// </summary>
        protected BaseBusiness() : this(Guid.NewGuid())
        {
        }

        #endregion

        #region 基本属性

        private Guid id;
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        [Description("序号")]
        public Guid Id
        {
            get
            {
                return id;
            }
            protected set
            {
                SetProperty(ref this.id, value);
            }
        }

        private int status;
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [Description("状态")]
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                SetProperty(ref this.status, value);
            }
        }

        private DateTime createdDate;
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        [Description("创建时间")]
        public DateTime CreatedDate
        {
            get
            {
                return this.createdDate;
            }
            protected set
            {
                SetProperty(ref this.createdDate, value);
            }
        }

        private DateTime changedDate;
        /// <summary>
        /// Gets or sets the changed date.
        /// </summary>
        [Description("更新时间")]
        public DateTime ChangedDate
        {
            get
            {
                return this.changedDate;
            }
            protected set
            {
                SetProperty(ref this.changedDate, value);
            }
        }

        private byte[] version;
        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        [Description("版本戳")]
        public byte[] Version
        {
            get
            {
                return this.version;
            }
            protected set
            {
                SetProperty(ref this.version, value);
            }
        }

        private string note;
        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        [Description("备注")]
        public string Note
        {
            get
            {
                return this.note;
            }
            set
            {
                SetProperty(ref this.note, value);
            }
        }

        #endregion

        #region 注册属性改变事件

        protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return;
            OnSetProperty(ref storage, value);

            this.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// 继承类复写改方法可以用于执行赋值后附加的操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        protected virtual void OnSetProperty<T>(ref T storage, T value)
        {
            storage = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        /// <summary>
        /// The add descriptions.
        /// </summary>
        protected override void AddDescriptions()
        {
            AddDescription("Id:" + Id);
            AddDescription("Status:" + Status);
            AddDescription("Note:" + Note.ToStr());
            AddDescription("CreatedDate:" + CreatedDate);
            AddDescription("ChangedDate:" + ChangedDate);
        }
    }
}