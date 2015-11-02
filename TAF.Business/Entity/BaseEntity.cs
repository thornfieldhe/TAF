// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseEntity.cs" company="">
//   
// </copyright>
// <summary>
//   The base entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Core;

    /// <summary>
    /// The base entity.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    [Serializable]
    public abstract partial class BaseBusiness<T> : IBusinessBase
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
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBusiness{T}"/> class.
        /// </summary>
        protected BaseBusiness() : this(Guid.NewGuid())
        {
        }

        #endregion

        #region 基本属性

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        public Guid Id
        {
            get; protected set;
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public int Status
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime CreatedDate
        {
            get; protected set;
        }

        /// <summary>
        /// Gets or sets the changed date.
        /// </summary>
        public DateTime ChangedDate
        {
            get; protected set;
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public byte[] Version
        {
            get; protected set;
        }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        public string Note
        {
            get; set;
        }

        #endregion

        /// <summary>
        /// The add descriptions.
        /// </summary>
        protected override void AddDescriptions()
        {
            AddDescription("Id:" + Id);
            AddDescription("Status:" + Status);
            AddDescription("Note:" + Note);
            AddDescription("CreatedDate:" + CreatedDate);
            AddDescription("ChangedDate:" + ChangedDate);
        }
    }
}