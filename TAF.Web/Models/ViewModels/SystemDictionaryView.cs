namespace TAF.Web.Models
{
    using System;
    using System.ComponentModel;

    using TAF.Core;
    using TAF.Utility;

    /// <summary>
    /// 字典表
    /// </summary>
    public class SystemDictionaryView : IEntityBase
    {
        public Guid Id
        {
            get; set;
        }
        public string Key
        {
            get; set;
        }

        public string KeyName
        {
            get
            {
                return EnumExt.GetItems<DictionaryKey>().Find(r => r.Value == this.Key).Text;
            }
        }

        public string Value
        {
            get; set;
        }

        public string Value1
        {
            get; set;
        }

        public string Value2
        {
            get; set;
        }

        public string Value3
        {
            get; set;
        }

        public string Value4
        {
            get; set;
        }


        public string Value5
        {
            get; set;
        }

        public string Value6
        {
            get; set;
        }

        public string Value7
        {
            get; set;
        }

        public string Value8
        {
            get; set;
        }

        public string Value9
        {
            get; set;
        }
    }

    public enum DictionaryKey
    {
        [Description("商品分类")]
        ProductCategory,
        [Description("颜色")]
        Color,
    }

}