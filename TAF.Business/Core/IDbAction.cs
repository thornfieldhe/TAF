namespace TAF.Core
{
    public interface IDbAction
    {
        int Create(bool commit);

        int Save(bool commit);

        int Delete(bool commit);
    }
}