// --------------------------------------------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

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
        /// Initializes a new instance of the <see cref="BaseBusiness"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        protected BaseBusiness(Guid id)
        {
            CurrentValues = new Dictionary<string, string>();
            OriginalValues = new Dictionary<string, string>();
            Id = id;
            Status = 0;
            CreatedDate = DateTime.Now;
            ChangedDate = DateTime.Now;
            createdBy = ModifyBy;
            InitProperties();

            rules = new List<IValidationRule>();
            this.validateionHandler = Ioc.Create<IValidationHandler>();
            this.DbProvider = Ioc.Create<IDbProvider>();
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

        private Guid createdBy;
        /// <summary>
        /// Gets or sets the changed date.
        /// </summary>
        [Description("创建者")]
        public Guid CreatedBy
        {
            get
            {
                return this.createdBy;
            }
            set
            {
                SetProperty(ref this.createdBy, value);
            }
        }

        private Guid modifyBy;
        /// <summary>
        /// Gets or sets the changed date.
        /// </summary>
        [Description("更新者")]
        public Guid ModifyBy
        {
            get
            {
                return this.modifyBy;
            }
            set
            {
                SetProperty(ref this.modifyBy, value);
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
            OnSetProperty(ref storage, value, propertyName);
            this.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// 继承类复写改方法可以用于执行赋值后附加的操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        protected virtual void OnSetProperty<T>(ref T storage, T value, string propertyName)
        {
            storage = value;
            this.CurrentValues[propertyName] = value.ToStr();
            CheckPropertyChange();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region 存储属性变更状态

        /// <summary>
        /// 当前属性值列表
        /// </summary>
        [NotMapped]
        public Dictionary<string, string> CurrentValues
        {
            get; private set;
        }

        /// <summary>
        /// 初始属性值列表
        /// </summary>
        [NotMapped]
        public Dictionary<string, string> OriginalValues
        {
            get; private set;
        }

        /// <summary>
        /// 已改变属性值列表
        /// </summary>
        /// <remarks>键，原值，现值</remarks>
        [NotMapped]
        public List<Tuple<string, string, string>> ChangedValues
        {
            get
            {
                var valuesX = this.CurrentValues;
                var valuesY = this.OriginalValues;

                var changedValues = new List<Tuple<string, string, string>>();
                foreach (var property in valuesX)
                {
                    if (!valuesY.ContainsKey(property.Key))
                    {
                        changedValues.Add(new Tuple<string, string, string>(property.Key, null, property.Value));
                    }
                    else if (property.Value != valuesY[property.Key])
                    {
                        changedValues.Add(new Tuple<string, string, string>(property.Key, valuesY[property.Key], property.Value));
                    }
                }
                foreach (var property in valuesY)
                {
                    if (!valuesX.ContainsKey(property.Key))
                    {
                        changedValues.Add(new Tuple<string, string, string>(property.Key, property.Value, null));
                    }
                    else if (property.Value != valuesX[property.Key])
                    {
                        changedValues.Add(new Tuple<string, string, string>(property.Key, property.Value, valuesY[property.Key]));
                    }
                }
                return changedValues;
            }
        }

        private void InitProperties()
        {
            var descriptions = this.ToString();
            OriginalValues = new Dictionary<string, string>();
            var reg = new Regex(@"(.*?):'(.*?)',?");
            var matches = reg.Matches(descriptions);
            foreach (Match match in matches)
            {
                OriginalValues.Add(match.Groups[1].Value, match.Groups[2].Value);
            }
        }

        /// <summary>
        /// 检查对象属性值是否与初始值一致
        /// </summary>
        /// <returns></returns>
        protected void CheckPropertyChange()
        {

        }

        #endregion

        #endregion

        /// <summary>
        /// The add descriptions.
        /// </summary>
        protected override void AddDescriptions()
        {
            AddDescription(nameof(Id), Id.ToStr());
            AddDescription(nameof(Status), Status.ToStr());
            AddDescription(nameof(Note), Note.ToStr());
            AddDescription(nameof(CreatedDate), CreatedDate.ToStr());
            AddDescription(nameof(ChangedDate), ChangedDate.ToStr());
        }
    }

    public delegate void FileWatchEventHandler(object sender, EventArgs e);
}