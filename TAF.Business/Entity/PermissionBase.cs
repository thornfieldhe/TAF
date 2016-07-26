// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PermissionBase.cs" company="" author="何翔华">
//   
// </copyright>
// <summary>
//   PermissionBase
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TAF.Business.Entity
{
    using System;

    using CacheManager.Core;
    using TAF.Utility;

    /// <summary>
    /// 权限基类
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class PermissionBase<T> : SingletonBase<T> where T : new()
    {
        /// <summary>
        /// 从缓存中获取用户对该模块id的权限
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetPermissionFromCache(Guid modelId, Guid userId)
        {
            //todos 增加判断两种情况下如何读取缓存
            string cacheKey = string.Format("{0}_{1}_{2}_{3}", "permissions", GetKey(), modelId, userId);
            var permissionStr = Ioc.Create<ICacheManager<object>>().Get<string>(cacheKey);
            if (permissionStr != null)
            {
                return int.Parse(permissionStr);
            }

            var permission = GetPermissions(modelId, userId);
            Ioc.Create<ICacheManager<object>>().Add(cacheKey, permission.ToString());
            return permission;
        }

        protected abstract string GetKey();

        protected abstract int GetPermissions(Guid modelId, Guid userId);

        protected int GetBasePermissions<K>(Guid modelId, Guid userId) where K : BaseBusiness<K>, new()
        {
            var exist = BaseBusiness<K>.Exist(r => r.Id == modelId && r.CreatedBy == userId)
                || !BaseBusiness<K>.Exist(r => r.Id == modelId);
            return exist ? (int)(InventoryAccess.Write | InventoryAccess.Read) : (int)InventoryAccess.Forbid;
        }
    }
}