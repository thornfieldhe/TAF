namespace TAF.Core
{
    using System;

    public interface IDbAction
    {
        int Create(Guid userId, bool commit);

        int Save(Guid userId, bool commit);

        int Delete(Guid userId, bool commit);
    }
}