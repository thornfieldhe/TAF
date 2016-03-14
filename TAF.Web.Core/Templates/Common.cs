using System;
using System.Data;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using CodeSmith.Engine;
using SchemaExplorer;
using SchemaMapper;
using TAF;
using TAF.Utility;



namespace SchemaMapper
{
    public static class Common
    {
 
        /// <summary>
        /// 属性是否为系统属性
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool IsSystemProperty(ColumnSchema column)
        {
            var systemProperties=new List<string>{"Id","Status","CreatedDate","ChangedDate","Version","Note"};
            return column.Name.In(systemProperties);            
        }
        
        
        /// <summary>
        /// 通过数据表获得类名
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetClassName(TableSchema table)
        {
            return table.Name.ToSingular();
        }
        
        /// <summary>
        /// 获取所有非系统属性
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<ColumnSchema> GetUnSystemProperties(TableSchema table){

            return table.Columns.Where(r=>!IsSystemProperty(r)).ToList();
            
        }      
  
        
        /// <summary>
        /// 移除字符串末尾Id
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string RemoveLaseId(string name){

            return name.ReplaceLast("Id","");
            
        }      
  
    }
}
